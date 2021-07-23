using ProjetoDapper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDapper.Data
{
    public class ContatoRepository : IBaseRepository<Contato>
    {
        public Contato Buscar(int id)
        {
            throw new NotImplementedException();
        }
        public List<Contato> PegarTodos()
        {
            throw new NotImplementedException();
        }
        public Contato Cadastrar(Contato entidade)
        {
            throw new NotImplementedException();
        }
        public Contato Atualizar(Contato entidade)
        {
            throw new NotImplementedException();
        }
        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

    }
}
