using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class AspNetUser : IdentityUser
{
    public string? Cognome { get; set; }

    public string? Nome { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Pagamento> Pagamentos { get; set; } = new List<Pagamento>();

    public virtual ICollection<Schedum> Scheda { get; set; } = new List<Schedum>();

}
