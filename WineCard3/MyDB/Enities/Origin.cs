using System.ComponentModel.DataAnnotations;

namespace WineCard3.MyDB.Enities
{
    public class Origin : BaseModel
    {
        [Required]
        public string? OriginName { get; set; }
    }
}
