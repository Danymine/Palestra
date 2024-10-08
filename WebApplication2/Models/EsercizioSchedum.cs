using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class EsercizioSchedum
{
    public int Id { get; set; }

    public int? FkScheda { get; set; }

    public int? FkEsercizio { get; set; }

    public int? NumeroSerie { get; set; }

    public string? NumeroRipetizioni { get; set; }

    public string? Note { get; set; }

    public virtual Esercizio? FkEsercizioNavigation { get; set; }

    public virtual Schedum? FkSchedaNavigation { get; set; }
}
