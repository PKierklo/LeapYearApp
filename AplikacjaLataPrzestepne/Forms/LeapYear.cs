using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeapYearApp.Forms
{
    
    public class LeapYear
    {
        [Key]
        public int Id { get; set; }
        public int Rok { get; set; }
        public string? Imie { get; set; }
        public DateTime Data { get; set; }
        public string? user_id { get; set; }
        public string? login { get; set; }
        public string czy_przestepny { get; set; }
    }
}
