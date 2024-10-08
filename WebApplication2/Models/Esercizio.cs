using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Esercizio
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Descrizione { get; set; } = null!;

    public string Dimostrazione { get; set; } = null!;

    public virtual ICollection<CategoriaEsercizio> CategoriaEsercizios { get; set; } = new List<CategoriaEsercizio>();

    public virtual ICollection<EsercizioMuscolo> EsercizioMuscolos { get; set; } = new List<EsercizioMuscolo>();

    public virtual ICollection<EsercizioSchedum> EsercizioScheda { get; set; } = new List<EsercizioSchedum>();
}
