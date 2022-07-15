namespace TestVersta
{
        public class Order
        {
            public int Id { get; set; }
            public string SenderCity { get; set; } = "";

            public string SenderAdress { get; set; } = "";

            public string RecipientCity { get; set; } = "";
            public string AddressRecipient { get; set; } = "";

            public string ParcelWeight { get; set; } = "";
            public string Date { get; set; } = "";
        }
}
