namespace Store.APIs.DTOs
{
    public class TransactionToReturnDTO
    {

        public string GoodID { get; set; }
        public string TransactionID { get; set; }
        public string TransactionDate { get; set; }

        public int Amount { get; set; }
        public string Direction { get; set; }
        public string Comment { get; set; }
    }
}
