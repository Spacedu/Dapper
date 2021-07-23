using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDapper.Domain
{
    public class Departamento
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        //public ICollection<UsuarioDepartamento> UsuarioDepartamentos { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
