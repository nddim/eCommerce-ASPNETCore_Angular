using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data.Models
{
    public class Administrator:KorisnickiRacun
    {
        //public int RolaID { get; set; }
        //[ForeignKey("RolaID")]
        //public AdminRola Rola { get; set; }
        public DateTime? ZadnjiLogin { get; set; }

    }
}
