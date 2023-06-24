using System;
using System.Collections.Generic;

namespace Books;

public partial class Author
{
    public int Id { get; set; }

    public string? Surname { get; set; }

    public string? Nameauthor { get; set; }

    public string? Patronimic { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
