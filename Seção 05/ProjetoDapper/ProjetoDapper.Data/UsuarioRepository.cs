using ProjetoDapper.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Z.Dapper.Plus;

namespace ProjetoDapper.Data
{
    public class UsuarioRepository : IBaseRepository<Usuario>, IUsuarioRepository
    {
        private IDbConnection _db;
        public UsuarioRepository(string connectionString)
        {
            this._db = new SqlConnection(connectionString);
        }
        public Usuario Buscar(int id)
        {
            /*
             * Tem como utilizar 3 opções:
             * 
             * this._db.Query<Usuario>("SELECT * FROM [dbo].[Usuarios] WHERE Id = @Id", new { Id = id }).SingleOrDefault();
             * this._db.QuerySingleOrDefault<Usuario>("SELECT * FROM [dbo].[Usuarios] WHERE Id = @Id", new { Id = id });
             * this._db.QueryFirstOrDefault<Usuario>("SELECT * FROM [dbo].[Usuarios] WHERE Id = @Id", new { Id = id });
             */
            return this._db.QuerySingleOrDefault<Usuario>("SELECT * FROM [dbo].[Usuarios] WHERE Id = @Id", new { Id = id });
        }
        public Usuario BuscaCompleta(int id)
        {
            string sql = "SELECT * FROM [dbo].[Usuarios] WHERE Id = @Id;" +
                "SELECT * FROM [dbo].[Contatos] WHERE UsuarioId = @Id;" +
                "SELECT * FROM [dbo].[EnderecosEntrega] WHERE UsuarioId = @Id;";

            using (var multiplosResultados = this._db.QueryMultiple(sql, new { Id = id }))
            {
                var usuario = multiplosResultados.Read<Usuario>().SingleOrDefault();
                var contato = multiplosResultados.Read<Contato>().SingleOrDefault();
                var enderecos = multiplosResultados.Read<EnderecoEntrega>().ToList();

                if(usuario != null)
                {
                    if(contato != null)
                    {
                        usuario.Contato = contato;
                    }
                    if(enderecos != null)
                    {
                        usuario.EnderecosEntrega = enderecos;
                    }
                }
                return usuario;
            }
        }
        public Usuario BuscaCompletaComJoin(int id)
        {
            string sql = "SELECT * FROM [dbo].[Usuarios] AS U LEFT JOIN [dbo].[Contatos] AS C ON U.Id = C.UsuarioId LEFT JOIN [dbo].[EnderecosEntrega] AS E ON U.Id = E.UsuarioId WHERE U.Id = @Id";

            var usuarioDic = new Dictionary<int, Usuario>();

            var usuario = this._db.Query<Usuario, Contato, EnderecoEntrega, Usuario>(sql, (usuario, contato, endereco) => {
                
                if( usuarioDic.Count == 0)
                {
                    usuarioDic.Add(usuario.Id, usuario);
                    usuario.EnderecosEntrega = new List<EnderecoEntrega>();
                }

                var usuarioDoDic = usuarioDic.GetValueOrDefault(usuario.Id);

                usuarioDoDic.Contato = contato;
                
                usuarioDoDic.EnderecosEntrega.Add(endereco);
                
                return usuario;
            }, new { Id = id });



            return usuarioDic[usuarioDic.Keys.First()];
        }


        public Usuario BuscaComJoinDepartamentos(int id)
        {
            string sql = "SELECT U.Id, U.Nome, U.Email, U.Sexo, U.RG, U.CPF, U.NomeMae, U.SituacaoCadastro, U.DataCadastro, D.Id DepartamentoId, D.Nome FROM [dbo].[Usuarios] AS U INNER JOIN [dbo].[UsuariosDepartamentos] AS UD ON U.Id = UD.UsuarioId INNER JOIN [dbo].[Departamentos] AS D ON D.Id = UD.DepartamentoId WHERE U.Id = @Id";

            var usuarioDic = new Dictionary<int, Usuario>();

            this._db.Query<Usuario, Departamento, Usuario>(sql, (usuario, departamento) =>
            {
                if( !usuarioDic.TryGetValue(usuario.Id, out var usuarioAtual))
                {
                    usuarioAtual = usuario;
                    usuarioDic.Add(usuarioAtual.Id, usuarioAtual);
                }

                usuarioAtual.Departamentos = usuarioAtual.Departamentos ?? new List<Departamento>();
                usuarioAtual.Departamentos.Add(departamento);

                return usuarioAtual;
            },
            new { Id = id },
            splitOn: "DepartamentoId");

            return usuarioDic[usuarioDic.Keys.First()];
        }

        public List<Usuario> PegarTodos()
        {
            return this._db.Query<Usuario>("SELECT * FROM [dbo].[Usuarios]").ToList();
        }
        public Usuario Cadastrar(Usuario entidade)
        {
            string sql = "INSERT INTO [dbo].[Usuarios] (Nome, Email, Sexo, RG, CPF, NomeMae, SituacaoCadastro, DataCadastro) VALUES (@Nome, @Email, @Sexo, @RG, @CPF, @NomeMae, @SituacaoCadastro, @DataCadastro); SELECT CAST(SCOPE_IDENTITY() AS INT);";

            entidade.Id = this._db.Query<int>(sql, entidade).Single();
            
            return entidade;
        }
        public Usuario Atualizar(Usuario entidade)
        {
            this._db.Execute("UPDATE [dbo].[Usuarios] SET Nome = @Nome, Email = @Email, Sexo = @Sexo, RG = @RG, CPF = @CPF, NomeMae = @NomeMae, SituacaoCadastro = @SituacaoCadastro, DataCadastro = @DataCadastro WHERE Id = @Id", entidade);

            return entidade;
        }

        public void Excluir(int id)
        {
            this._db.Execute("DELETE FROM [dbo].[Usuarios] WHERE Id = @Id", new { id });
        }

        public Usuario BuscaUsuarioComDapperEProcedure(int id)
        {
            return this._db.Query<Usuario>("GetUsuarioNome", new { Id = id }, commandType: CommandType.StoredProcedure).SingleOrDefault();
        }

        public void CadastrarUsuarioEmLote(List<Usuario> usuarios)
        {
            DapperPlusManager.Entity<Usuario>().Table("Usuarios");

            this._db.BulkInsert(usuarios);
        }

        public List<Usuario> PegarApenasOsUsuariosComIds(params int[] ids)
        {
            string sql = "SELECT * FROM [dbo].[Usuarios] WHERE Id IN @Ids";
            return this._db.Query<Usuario>(sql, new { Ids = ids }).ToList();
        }
    }
}
