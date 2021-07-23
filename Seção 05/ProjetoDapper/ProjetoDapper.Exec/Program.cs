using ProjetoDapper.Data;
using ProjetoDapper.Domain;
using System;
using System.Collections.Generic;

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
             * BuscaUsuarioComDapperJoinDepartamentos();
             * ContatoComDapperContrib();
             * BuscaUsuarioComDapperEProcedure();
             * CadastrarUsuarioEmLoteComDapperPlus();
             */
            PegarUsuariosPorArrayDeIds();


            Console.WriteLine("Finalizado!");
        }

        static void PegarUsuariosPorArrayDeIds()
        {
            IUsuarioRepository repository = new UsuarioRepository(conString);
            var usuarios = repository.PegarApenasOsUsuariosComIds(1003, 2002, 2003);
            foreach(var usuario in usuarios)
            {
                Console.WriteLine($" - {usuario.Nome}");
            }
        }
        static void CadastrarUsuarioEmLoteComDapperPlus()
        {
            Usuario usuario01 = new Usuario() { Nome = "Marcos Ribeiro", Email = "jessica.ribeiro@gmail.com", RG = "2.255.652", CPF = "123.555.654-14", Sexo = "F", NomeMae = "Maria Ribeiro", SituacaoCadastro = "A", DataCadastro = DateTime.Now };
            Usuario usuario02 = new Usuario() { Nome = "Tiago Ribeiro", Email = "jessica.ribeiro@gmail.com", RG = "2.255.652", CPF = "123.555.654-14", Sexo = "F", NomeMae = "Maria Ribeiro", SituacaoCadastro = "A", DataCadastro = DateTime.Now };
            Usuario usuario03 = new Usuario() { Nome = "Marcelo Ribeiro", Email = "jessica.ribeiro@gmail.com", RG = "2.255.652", CPF = "123.555.654-14", Sexo = "F", NomeMae = "Maria Ribeiro", SituacaoCadastro = "A", DataCadastro = DateTime.Now };
            Usuario usuario04 = new Usuario() { Nome = "Aline Ribeiro", Email = "jessica.ribeiro@gmail.com", RG = "2.255.652", CPF = "123.555.654-14", Sexo = "F", NomeMae = "Maria Ribeiro", SituacaoCadastro = "A", DataCadastro = DateTime.Now };

            var usuarios = new List<Usuario>() { usuario01, usuario02, usuario03, usuario04 };

            IUsuarioRepository repository = new UsuarioRepository(conString);
            repository.CadastrarUsuarioEmLote(usuarios);

        }
        static void BuscaUsuarioComDapperEProcedure()
        {
            IUsuarioRepository repository = new UsuarioRepository(conString);
            var usuario = repository.BuscaUsuarioComDapperEProcedure(2);
        }
        static void ContatoComDapperContrib()
        {
            IBaseRepository<Contato> repository = new ContatoRepository(conString);
            //Buscar contato do João.
            var contatoJoao = repository.Buscar(1);
            Console.WriteLine($"Telefone do João: {contatoJoao.Telefone} - {contatoJoao.Celular}");

            //Cadastra um contato para a Jessica
            var contato = new Contato() { Telefone = "(61) 3333-3333", Celular = "(61) 91273-1829", UsuarioId = 1003 };
            repository.Cadastrar(contato);
            Console.WriteLine($"Usuário inserido com sucesso! (ID: {contato.Id})");

            contato.Telefone = "(61) 3891-8294";
            repository.Atualizar(contato);

            Console.WriteLine($"Usuário atualizado com sucesso!");

            repository.Excluir(contato.Id);


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
