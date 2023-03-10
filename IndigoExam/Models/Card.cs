using System.ComponentModel.DataAnnotations;

namespace IndigoExam.Models
{
    public class Card
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

    }
}
