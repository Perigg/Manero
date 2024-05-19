namespace Manero.Client.Models
{
    public class CardInteractive
    {
        public int CardId { get; set; }
        public int CardOwnerId { get; set; }

        public string CardOwnerName { get; set; }

        public DateOnly CardExpDate { get; set; }

        public string CardNumber { get; set; }

        public string CardPin { get; set; }
        public bool ShowOptions { get; set; }
        public int RefreshState { get; set; }
    }
}
