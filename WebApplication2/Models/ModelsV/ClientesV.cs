using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.ModelsV
{
    public class ClientesV
    {
        // Potresti anche voler aggiungere altre proprietà per la validazione, come ad esempio:
        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il nome non può superare i 50 caratteri.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio.")]
        [StringLength(50, ErrorMessage = "Il cognome non può superare i 50 caratteri.")]
        public string Cognome { get; set; }

        [Required(ErrorMessage = "L'email è obbligatoria.")]
        [EmailAddress(ErrorMessage = "L'email non è valida.")]
        public string Email { get; set; }

        [DisplayName("HaveAccess")]
        public bool? HaveAccess { get; set; }

        [DisplayName("Numero di Telefono"), StringLength(15, ErrorMessage = "Il numero di telefono non può superare i 15 caratteri.")]
        public string? NumeroTelefono { get; set; }

        public string FkUtente { get; set; }
    }
}
