using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class CategoriaEsercizio
{
    public int Id { get; set; }

    public int FkEsercizio { get; set; }

    public int FkCategoria { get; set; }

    public virtual Categorium FkCategoriaNavigation { get; set; } = null!;

    public virtual Esercizio FkEsercizioNavigation { get; set; } = null!;
}
