
using System.ComponentModel.DataAnnotations;

namespace Clases
{
    public class Snippet
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "El titulo es obligatorio")]
        public string Titulo { get; set; }
        public int LenguajeId { get; set; }
        [Required(ErrorMessage = "El código es obligatorio")]
        public string Codigo { get; set; }
    }

}
