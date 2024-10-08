namespace WebApplication2.Models.ModelsV
{
    public class SchedumV
    {
        public required string NomeCognome { get; set; }

        public required string DataInizio { get; set; }

        public required string DataFine { get; set; }

        public string? Note { get; set; }
    }
}
