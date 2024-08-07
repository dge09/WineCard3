using WineCard3.MyDB.Enities;

namespace WineCard3.MyDB.DTOs
{
    public class CardWineListDTO
    {
        public Wine DtoWine { get; set; }
        public bool IsPartOfCard { get; set; }

        public CardWineListDTO(Wine w)
        {
            DtoWine = w;
        }
    }
}
