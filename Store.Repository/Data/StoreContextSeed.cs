using Store.Core.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Data
{
    public static class StoreContextSeed
    {
        //public async static Task SeedAsync(StoreContext _dbcontext)
        //{
        //    if(_dbcontext.StoresInfo.Count() == 0)
        //    {
        //        var storesData = File.ReadAllText("../Store.Repository/Data/DataSeed/stores.csv");
        //        var stores = JsonSerializer.Deserialize<List<StoreInfo>>(storesData);

        //        if(stores?.Count() > 0)
        //        {
        //            foreach(var store in stores)
        //            {
        //                _dbcontext.Set<StoreInfo>().Add(store);
        //            }
        //            await _dbcontext.SaveChangesAsync();
        //        }
        //    }

        //    if (_dbcontext.Goods.Count() == 0)
        //    {
        //        var goodsData = File.ReadAllText("../Store.Repository/Data/DataSeed/goods.csv");
        //        var goods = JsonSerializer.Deserialize<List<Good>>(goodsData);

        //        if (goods?.Count() > 0)
        //        {
        //            foreach (var good in goods)
        //            {
        //                _dbcontext.Set<Good>().Add(good);
        //            }
        //            await _dbcontext.SaveChangesAsync();
        //        }
        //    }

        //    if (_dbcontext.Transactions.Count() == 0)
        //    {
        //        var transactionsData = File.ReadAllText("../Store.Repository/Data/DataSeed/transactions.csv");
        //        var transactions = JsonSerializer.Deserialize<List<Transaction>>(transactionsData);

        //        if (transactions?.Count() > 0)
        //        {
        //            foreach (var transaction in transactions)
        //            {
        //                _dbcontext.Set<Transaction>().Add(transaction);
        //            }
        //            await _dbcontext.SaveChangesAsync();
        //        }
        //    }
        //}



        private static List<StoreInfo> ParseCsvFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var stores = new List<StoreInfo>();

            // Skip the header line
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var values = line.Split(';');

                var store = new StoreInfo
                {
                    Name = values[0].Trim(),
                    StoreFileDate = DateTime.ParseExact(values[1].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture)
                };

                stores.Add(store);
            }

            return stores;
        }
        public static List<Good> ParseGoodsCsvFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var goods = new List<Good>();

            // Skip the header line
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var values = line.Split(';');

                var good = new Good
                {
                    StoreName = "Azdo",
                    GoodID = (values[0].Trim()),
                    GoodInitialBalance = int.Parse(values[1].Trim())
                };

                goods.Add(good);
            }

            return goods;
        }
        public static List<Transaction> ParseTransactionsCsvFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var transactions = new List<Transaction>();

            // Skip the header line
            for (int i = 1; i < lines.Length; i++)
            {
                var line = lines[i];
                var values = line.Split(';');

                var transaction = new Transaction
                {
                    GoodID = (values[0].Trim()),
                    TransactionID = (values[1].Trim()),
                    TransactionDate = DateTime.ParseExact(values[2].Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Amount = int.Parse(values[3].Trim()),
                    Direction = values[4].Trim(),
                    Comment = values.Length > 5 ? values[5].Trim() : null
                };

                transactions.Add(transaction);
            }
            Console.WriteLine(transactions);
            return transactions;
        }

        public async static Task SeedAsync(StoreContext _dbcontext)
        {
            if (_dbcontext.StoresInfo.Count() == 0)
            {
                var filePath = "../Store.Repository/Data/DataSeed/stores.csv";
                var stores = ParseCsvFile(filePath);

                if (stores.Count > 0)
                {
                    foreach (var store in stores)
                    {
                        _dbcontext.Set<StoreInfo>().Add(store);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }
            if (_dbcontext.Goods.Count() == 0)
            {
                var filePath = "../Store.Repository/Data/DataSeed/goods.csv";
                var goods = ParseGoodsCsvFile(filePath);

                if (goods.Count > 0)
                {
                    foreach (var good in goods)
                    {
                        _dbcontext.Set<Good>().Add(good);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }
            if (_dbcontext.Transactions.Count() == 0)
            {
                var filePath = "../Store.Repository/Data/DataSeed/transactions.csv";
                var transactions = ParseTransactionsCsvFile(filePath);

                if (transactions.Count > 0)
                {
                    foreach (var transaction in transactions)
                    {
                        _dbcontext.Set<Transaction>().Add(transaction);
                    }
                    await _dbcontext.SaveChangesAsync();
                }
            }

        }
    }
}
