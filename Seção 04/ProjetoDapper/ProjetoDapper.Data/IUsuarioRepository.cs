using ProjetoDapper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoDapper.Data
{
    public interface IUsuarioRepository
    {
        Usuario BuscaCompleta(int id);
        Usuario BuscaCompletaComJoin(int id);
        Usuario BuscaComJoinDepartamentos(int id);
    }
}
