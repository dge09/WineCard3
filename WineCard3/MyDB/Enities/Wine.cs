using System.ComponentModel.DataAnnotations;

namespace WineCard3.MyDB.Enities
{
    public class Wine : BaseModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string? WineName { get; set; }

        [Required]
        public int StyleID { get; set; }
        public Style? Style { get; set; }
        
        public int SweetnessLevel { get; set; }
        
        public int Year { get; set; }
        
        public float Rating { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public int OriginID { get; set; }
        public Origin? Origin { get; set; }

        public ICollection<Card> Cards { get; set; } = new List<Card>();
    }
}
