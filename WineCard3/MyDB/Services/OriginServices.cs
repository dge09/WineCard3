using WineCard3.MyDB.Enities;

namespace WineCard3.MyDB.Services
{
    public class OriginServices : BaseServices<Origin>
    {
        private readonly DataContext _cont;
        public OriginServices(DataContext cont) : base(cont)
        {
        }
    }
}
