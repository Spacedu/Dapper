using ProjetoDapper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDapper.Data
{
    public class DepartamentoRepository : IBaseRepository<Departamento>
    {
        public Departamento Buscar(int id)
        {
            throw new NotImplementedException();
        }
        public List<Departamento> PegarTodos()
        {
            throw new NotImplementedException();
        }
        public Departamento Cadastrar(Departamento entidade)
        {
            throw new NotImplementedException();
        }
        public Departamento Atualizar(Departamento entidade)
        {
            throw new NotImplementedException();
        }
        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }
    }
}
