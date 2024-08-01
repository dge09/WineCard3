using WineCard3.MyDB.Enities;

namespace WineCard3.MyDB.Services
{
    public class WineServices : BaseServices<Wine>
    {
        private readonly DataContext _cont;
        public WineServices(DataContext cont) : base(cont)
        {
        }
    }
}
