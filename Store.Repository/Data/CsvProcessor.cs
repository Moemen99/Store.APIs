
using Microsoft.Extensions.Logging;
using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Data
{
    public static class CsvProcessor
    {
        
        //private static bool _isProcessed = false;
        //private static readonly object _lock = new object();
        public static async Task ProcessCsvAsync(ILoggerFactory loggerFactory)
        {
            
            //lock (_lock)
            //{
            //    if (_isProcessed)
            //    {
            //        return;
            //    }
            //    _isProcessed = true;
            //}

            var storeDataLines = await File.ReadAllLinesAsync("../Store.Repository/Data/DataProcess/Store Data.csv");

            #region StoreProcessing
            // Process store information
            var storeInfo = ReadStoreInfo(storeDataLines.ToList());
            // Create CSV outputs
            var storeCsvOutput = CreateStoreCsvOutput(storeInfo);
            // Write CSV outputs to separate files
            await File.WriteAllTextAsync("../Store.Repository/Data/DataSeed/stores.csv", storeCsvOutput);
            #endregion


            #region GoodProcessing
            // Process good information
            var goods = ReadGoods(storeDataLines.Skip(2).Take(6).ToList());
            // Create Csv output
            var goodCsvOutput = CreateGoodCsvOutput(goods);
            // Write CSV outputs to separate files
            await File.WriteAllTextAsync("../Store.Repository/Data/DataSeed/goods.csv", goodCsvOutput);
            #endregion


            #region TransactionProcessing
            // Process transaction information
            var transactionsLines = storeDataLines.Skip(11).ToList();
            var (transactions, errorLog) = ReadTransactions(transactionsLines, loggerFactory);
            // Create Csv output
            var transactionsCsvOutput = CreateTransactionsCsvOutput(transactions);
            // Write CSV outputs to separate files
            await File.WriteAllTextAsync("../Store.Repository/Data/DataSeed/transactions.csv", transactionsCsvOutput);

            // Write error log to a text file
            await File.WriteAllLinesAsync("../Store.Repository/Data/SegregatedData/errors.log.txt", errorLog);

            #endregion

            #region Segregate Transactions
            // Write transactions to separate CSV files based on GoodID 
            await CreateSeperateTransactionCsvFilesAsync(transactions);

            #endregion

        }

        private static StoreInfo ReadStoreInfo(List<string> lines) => new StoreInfo
        {
            Name = lines[0].Split(";")[0].Trim(),

            StoreFileDate = DateTime.ParseExact(lines[1].Split(";")[0].Trim(), "d/m/yyyy", CultureInfo.InvariantCulture)
        };

        private static string CreateStoreCsvOutput(StoreInfo storeInfo)
        {
            var csv = new List<string>()
            {
                "Store Name;File Date",
                $"{storeInfo.Name};{storeInfo.StoreFileDate:dd/MM/yyyy}"
            };
            return string.Join("\n", csv);
        }


        private static List<Good> ReadGoods(List<string> lines) => lines.Select(line => { var values = line.Split(";");
            return new Good { GoodID = (values[1].Trim().Split(" ")[1]) , 
                              GoodInitialBalance = int.Parse(values[0])};
        }).ToList();
        private static string CreateGoodCsvOutput(List<Good> goods)
        {
            var csv = new List<string>()
            {
                "Good ID;Good Initial Balance"
            };
            csv.AddRange(goods.Select(G => $"{G.GoodID};{G.GoodInitialBalance}"));

            return string.Join("\n", csv);
        }


       private static (List<Transaction>, List<string>) ReadTransactions(List<string> lines, ILoggerFactory loggerFactory)
        {   
            var logger = loggerFactory.CreateLogger<Transaction>();
            var transactions = new List<Transaction>();
            var errorLog = new List<string>();

            // Assuming the headers are at line index 11, start processing real records from index 12
            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var transactionValues = line.Split(';');

                // Check for empty or null values in the required fields (Good ID, Transaction ID, Transaction Date, Amount, Direction)
                if (string.IsNullOrWhiteSpace(transactionValues[0]) || 
                    string.IsNullOrWhiteSpace(transactionValues[1]) || 
                    string.IsNullOrWhiteSpace(transactionValues[2]) || 
                    string.IsNullOrWhiteSpace(transactionValues[3]) || 
                    string.IsNullOrWhiteSpace(transactionValues[4]))
                {
                    string errorMsg = $"Error: Null or empty value found in non-comment field while reading csv file, Line Number: {i + 12}, Good ID: {transactionValues.ElementAtOrDefault(0) ?? "N/A"}, Transaction ID: {transactionValues.ElementAtOrDefault(1) ?? "N/A"}";
                    errorLog.Add(errorMsg);
                    logger.LogError("Null or empty value found in non-comment field while reading csv file, Line Number: {LineNumber}, Good ID: {GoodId}, Transaction ID: {TransactionId}",
                    i + 12,
                    transactionValues.ElementAtOrDefault(0) ?? "N/A",
                    transactionValues.ElementAtOrDefault(1) ?? "N/A");
                    continue;
                }

                try
                {
                    var transaction = new Transaction()
                    {
                        GoodID = transactionValues[0],
                        TransactionID = transactionValues[1],
                        TransactionDate = DateTime.ParseExact(transactionValues[2], "dd/MM/yyyy", CultureInfo.InvariantCulture),
                        Amount = int.Parse(transactionValues[3]),
                        Direction = transactionValues[4],
                        Comment = transactionValues[5]
                    };
                    transactions.Add(transaction);
                }
                catch (Exception ex)
                {
                    string errorMsg = $"Error parsing transaction at line {i + 12}, Good ID: {transactionValues[0]}, Transaction ID: {transactionValues[1]}: {ex.Message}";
                    errorLog.Add(errorMsg);
                    logger.LogError(ex, "Error parsing transaction at line {LineNumber}, Good ID: {GoodId}, Transaction ID: {TransactionId}",
                    i + 12,
                   transactionValues[0],
                   transactionValues[1]);
                }
            }

            return (transactions, errorLog);
        }



        private static string CreateTransactionsCsvOutput(List<Transaction> transactions)
        {
            var csv = new List<string>()
            {
                $"Good ID;Transaction ID;Transaction Date;Amount;Direction;Comments"
            };

            csv.AddRange(transactions.Select(T => $"{T.GoodID};{T.TransactionID};{T.TransactionDate:dd/MM/yyyy};{T.Amount};{T.Direction};{T.Comment}"));

            return string.Join("\n",csv );
        }

        public static async Task CreateSeperateTransactionCsvFilesAsync(List<Transaction> transactions)
        {
            // Group transactions by GoodID
            var groupedTransactions = transactions.GroupBy(t => t.GoodID);

            foreach (var group in groupedTransactions)
            {
                var goodId = group.Key;
                var csv = new List<string>()
            {
                "Good ID;Transaction ID;Transaction Date;Amount;Direction;Comments"
            };

                csv.AddRange(group.Select(T => $"{T.GoodID};{T.TransactionID};{T.TransactionDate:dd/MM/yyyy};{T.Amount};{T.Direction};{T.Comment}"));

                var csvContent = csv;
                var filePath = Path.Combine("../Store.Repository/Data/SegregatedData/", $"Transactions_GoodID_{goodId}.csv");

                await File.WriteAllLinesAsync(filePath, csvContent);
            }
        }

    }
}
