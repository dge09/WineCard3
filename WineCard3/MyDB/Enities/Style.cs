using System.ComponentModel.DataAnnotations;

namespace WineCard3.MyDB.Enities
{
    public class Style : BaseModel
    {
        [Required]
        public string? StyleDscp { get; set; }
    }
}
