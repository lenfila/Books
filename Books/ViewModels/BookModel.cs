using System.ComponentModel.DataAnnotations;

namespace Books.ViewModels
{
    public class BookModel
    {
        
        [Required]
        [Display(Name = "Название")]
        public string? Title { get; set; }

        [Required]
        [Display(Name = "Жанр")]
        public string? Category { get; set; }

        [Required]
        [Display(Name = "Автор")]
        public string? Author { get; set; }
    }
}
