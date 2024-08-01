using WineCard3.MyDB.Enities;

namespace WineCard3.MyDB.Services
{
    public class CardServices : BaseServices<Card>
    {
        private readonly DataContext _cont;
        public CardServices(DataContext cont) : base(cont)
        {
        }
    }
}
