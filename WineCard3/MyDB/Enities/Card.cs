namespace WineCard3.MyDB.Enities
{
    public class Card : BaseModel
    {
        public string? CardName { get; set; }
        public string? CardDscp { get; set; }
        //public List<Wine> Wines { get; set; }

        public ICollection<Wine> Wines { get; set; } = new List<Wine>();
    }
}
