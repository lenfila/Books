using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Books;

public partial class Book
{
    [Required]
    [Display(Name = "Идентификатор")]
    public int Id { get; set; }

    [Required]
    [Display(Name = "Название")]
    public string? Title { get; set; }

    [Required]
    [Display(Name = "Жанр")]
    public string? Category { get; set; }
    [Required]
    [Display(Name = "Автор")]
    public int? AuthorId { get; set; }
    [Required]
    [Display(Name = "Автор")]
    public virtual Author? Author { get; set; }
}
