using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Categorium
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<CategoriaEsercizio> CategoriaEsercizios { get; set; } = new List<CategoriaEsercizio>();
}
