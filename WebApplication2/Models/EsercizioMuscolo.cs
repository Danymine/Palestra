using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class EsercizioMuscolo
{
    public int Id { get; set; }

    public int FkEsercizio { get; set; }

    public int FkMuscolo { get; set; }

    public virtual Esercizio FkEsercizioNavigation { get; set; } = null!;

    public virtual Muscolo FkMuscoloNavigation { get; set; } = null!;
}
