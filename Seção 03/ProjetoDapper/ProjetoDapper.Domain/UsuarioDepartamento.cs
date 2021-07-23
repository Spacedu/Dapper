using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDapper.Domain
{
    public class UsuarioDepartamento
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int DepartamentoId { get; set; }

        public Usuario Usuario { get; set; }

        public Departamento Departamento { get; set; }
    }
}
