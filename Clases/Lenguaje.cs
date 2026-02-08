using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Clases
{
    public class Lenguaje
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; }
        public List<Snippet?> Snippets { get; set; } = new();

        public Lenguaje()
        {

        }
    }
}
