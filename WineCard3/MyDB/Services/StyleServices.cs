using WineCard3.MyDB.Enities;

namespace WineCard3.MyDB.Services
{
    public class StyleServices : BaseServices<Style>
    {
        private readonly DataContext _cont;
        public StyleServices(DataContext cont) : base(cont)
        {
        }
    }
}
