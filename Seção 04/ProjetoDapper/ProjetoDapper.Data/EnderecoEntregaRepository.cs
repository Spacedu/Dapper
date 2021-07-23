using Dapper.Contrib.Extensions;
using ProjetoDapper.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDapper.Data
{
    public class EnderecoEntregaRepository : IBaseRepository<EnderecoEntrega>
    {
        private IDbConnection _db;
        public EnderecoEntregaRepository(string connectionString)
        {
            this._db = new SqlConnection(connectionString);
        }
        public EnderecoEntrega Buscar(int id)
        {
            return this._db.Get<EnderecoEntrega>(id);
        }
        public List<EnderecoEntrega> PegarTodos()
        {
            return this._db.GetAll<EnderecoEntrega>().ToList();
        }
        public EnderecoEntrega Cadastrar(EnderecoEntrega entidade)
        {
            entidade.Id = Convert.ToInt32(this._db.Insert<EnderecoEntrega>(entidade));
            return entidade;
        }
        public EnderecoEntrega Atualizar(EnderecoEntrega entidade)
        {
            this._db.Update<EnderecoEntrega>(entidade);
            return entidade;
        }
        public void Excluir(int id)
        {
            var contato = Buscar(id);
            this._db.Delete<EnderecoEntrega>(contato);
        }
    }
}
