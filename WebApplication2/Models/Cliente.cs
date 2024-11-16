using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public partial class Cliente
{
    public int Id { get; set; }

    public string? Nome { get; set; }

    public string? Cognome { get; set; }

    public string? Email { get; set; }

    public bool? HaveAccess { get; set; }

    public string? NumeroTelefono { get; set; }

    public string? FkUtente { get; set; }

    public virtual AspNetUser? FkUtenteNavigation { get; set; }

    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();

    public virtual ICollection<Schedum> Scheda { get; set; } = new List<Schedum>();
}
