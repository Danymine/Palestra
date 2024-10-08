using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Muscolo
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<EsercizioMuscolo> EsercizioMuscolos { get; set; } = new List<EsercizioMuscolo>();
}
