using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Pagamento
{
    public int Id { get; set; }

    public int? FkCliente { get; set; }

    public string? FkUtente { get; set; }

    public DateOnly? Data { get; set; }

    public decimal? ImportoPagamento { get; set; }

    public bool? StatoPagamento { get; set; }

    public virtual Cliente? FkClienteNavigation { get; set; }

    public virtual AspNetUser? FkUtenteNavigation { get; set; }
}
