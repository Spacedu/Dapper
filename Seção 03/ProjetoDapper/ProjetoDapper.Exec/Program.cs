using ProjetoDapper.Data;
using ProjetoDapper.Domain;
using System;

namespace ProjetoDapper.Exec
{
    class Program
    {
        static string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DbProjetoDapper;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        static void Main(string[] args)
        {
            /*
             * PegarTodosUsuariosComDapper();
             * BuscarUsuarioComDapper();
             * CadastrarUsuarioComDapper();
             * AtualizarUsuarioComDapper();
             * ExcluirUsuarioComDapper();
             * BuscaCompletaUsuarioComDapper();
             * BuscaCompletaUsuarioComDapperJoin();
             */
            BuscaUsuarioComDapperJoinDepartamentos();
            Console.WriteLine("Finalizado!");
        }
        static void BuscaUsuarioComDapperJoinDepartamentos()
        {
            /*Many-to-Many*/
            IUsuarioRepository repository = new UsuarioRepository(conString);
            var usuario = repository.BuscaComJoinDepartamentos(2);

            Console.WriteLine($"Nome: {usuario.Nome}");
            foreach (var departamento in usuario.Departamentos)
            {
                Console.WriteLine($" - {departamento.Nome} ({departamento.Id})");
            }
        }
        static void BuscaCompletaUsuarioComDapperJoin()
        {
            IUsuarioRepository repository = new UsuarioRepository(conString);
            var usuario = repository.BuscaCompletaComJoin(2);
            Console.WriteLine($"Dados do usuário: {usuario.Nome} - {usuario.Contato.Celular} - {usuario.EnderecosEntrega.Count}");
            foreach(var endereco in usuario.EnderecosEntrega)
            {
                Console.WriteLine($"Nome End.: {endereco.NomeEndereco} ({endereco.CEP})");
            }
        }
        static void BuscaCompletaUsuarioComDapper()
        {
            IUsuarioRepository repository = new UsuarioRepository(conString);
            var usuario = repository.BuscaCompleta(2);

            Console.WriteLine($"Telefone do usuário: {usuario.Contato.Celular}");
            Console.WriteLine($"QTD. Endereços cadastrados: {usuario.EnderecosEntrega.Count}");

        }
        static void ExcluirUsuarioComDapper()
        {
            IBaseRepository<Usuario> repository = new UsuarioRepository(conString);
            repository.Excluir(3);

            Console.WriteLine("Usuário foi excluído com sucesso!");
        }
        static void AtualizarUsuarioComDapper()
        {
            Usuario usuario = new Usuario() { Id = 3, Nome = "Jessica Rodrigues", Email = "jessica.rodrigues@gmail.com", RG = "2.255.652", CPF = "123.555.654-14", Sexo = "F", NomeMae = "Maria Rodrigues", SituacaoCadastro = "A", DataCadastro = DateTime.Now };
            
            IBaseRepository<Usuario> repository = new UsuarioRepository(conString);
            repository.Atualizar(usuario);

            Console.WriteLine("Usuário atualizado com sucesso!");
        }
        static void CadastrarUsuarioComDapper()
        {
            Usuario usuario = new Usuario() { Nome = "Jessica Ribeiro", Email = "jessica.ribeiro@gmail.com", RG = "2.255.652", CPF = "123.555.654-14", Sexo = "F", NomeMae = "Maria Ribeiro", SituacaoCadastro = "A", DataCadastro = DateTime.Now };

            IBaseRepository<Usuario> repository = new UsuarioRepository(conString);
            repository.Cadastrar(usuario);

            Console.WriteLine($"Usuário inserido com sucesso: (ID: {usuario.Id})");
        }

        static void BuscarUsuarioComDapper()
        {
            IBaseRepository<Usuario> repository = new UsuarioRepository(conString);

            var usuario = repository.Buscar(1);
            if(usuario != null)
            {
                Console.WriteLine($"Usuário localizado: {usuario.Nome}");
            }
            else
            {
                Console.WriteLine("Usuário não localizado.");
            }
        }
        static void PegarTodosUsuariosComDapper()
        {
            IBaseRepository<Usuario> repository = new UsuarioRepository(conString);

            Console.WriteLine($"Quantidade de usuários no banco: {repository.PegarTodos().Count}");
        }
    }
}
