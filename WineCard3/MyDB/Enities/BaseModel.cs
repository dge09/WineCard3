using System.ComponentModel.DataAnnotations;

namespace WineCard3.MyDB.Enities
{
    public class BaseModel
    {
        [Key]
        public int ID { get; set; }
    }
}
