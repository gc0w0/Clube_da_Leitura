using Clube_da_Leitura.Compartilhado;
using Gestao_de_Equipamentos.Compartilhado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clube_da_Leitura.ModuloRevista
{
    public class RepositorioRevista : RepositorioBase<Revista>, IRepositorioRevista
    {
        public List<Revista> SelecionarPorFiltro(string letra)
        {
            throw new NotImplementedException();
        }

        public List<Revista> SelecionarPorFiltro2(Predicate<Revista> condicao)
        {
            throw new NotImplementedException();
        }
    }
}
