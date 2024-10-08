using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Schedum
{
    public int Id { get; set; }

    public DateOnly DataInizio { get; set; }

    public DateOnly? DataFine { get; set; }

    public string? Note { get; set; }

    public string FkUtente { get; set; } = null!;

    public int FkCliente { get; set; }

    public virtual ICollection<EsercizioSchedum> EsercizioScheda { get; set; } = new List<EsercizioSchedum>();

    public virtual Cliente FkClienteNavigation { get; set; } = null!;

    public virtual AspNetUser FkUtenteNavigation { get; set; } = null!;
}
