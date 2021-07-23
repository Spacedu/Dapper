using ProjetoDapper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SqlClient;

namespace ProjetoDapper.Data
{
    public class ContatoRepository : IBaseRepository<Contato>
    {
        private IDbConnection _db;
        public ContatoRepository(string connectionString)
        {
            this._db = new SqlConnection(connectionString);
        }
        public Contato Buscar(int id)
        {
            return this._db.Get<Contato>(id);
        }
        public List<Contato> PegarTodos()
        {
            return this._db.GetAll<Contato>().ToList();
        }
        public Contato Cadastrar(Contato entidade)
        {
            entidade.Id = Convert.ToInt32( this._db.Insert<Contato>(entidade) );
            return entidade;
        }
        public Contato Atualizar(Contato entidade)
        {
            this._db.Update<Contato>(entidade);
            return entidade;
        }
        public void Excluir(int id)
        {
            var contato = Buscar(id);
            this._db.Delete<Contato>(contato);
        }

    }
}
