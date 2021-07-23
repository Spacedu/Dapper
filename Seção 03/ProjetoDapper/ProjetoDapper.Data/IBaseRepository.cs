using ProjetoDapper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDapper.Data
{
    public interface IBaseRepository<T>
    {
        //CRUD
        T Buscar(int id);
        List<T> PegarTodos();
        T Cadastrar(T entidade);
        T Atualizar(T entidade);
        void Excluir(int id);
    }
}
