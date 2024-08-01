using WineCard3.MyDB.Enities;

namespace WineCard3.MyDB.Services
{
    public static class MySer
    {
        public static readonly DataContext context = new();
        public static readonly IBaseService<Style> styleServices = new StyleServices(context);
        public static readonly IBaseService<Origin> originServices = new OriginServices(context);
        public static readonly IBaseService<Wine> wineServices = new WineServices(context);
        public static readonly IBaseService<Card> cardServices = new CardServices(context);
        public static readonly CsvServices csvServices = new();
    }
}
