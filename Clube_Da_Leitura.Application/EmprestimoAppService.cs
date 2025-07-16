using Clube_da_Leitura.Dominio.ModuloEmprestimo;
using Clube_da_Leitura.ModuloEmprestimo;
using Clube_da_Leitura.ModuloRevista;
using static Clube_da_Leitura.ModuloRevista.Revista;

namespace Clube_Da_Leitura.Application
{
    public class EmprestimoAppService 
    { 
        private IRepositorioEmprestimo repositorioEmprestimo { get; set; }
        private IRepositorioRevista repositorioRevista { get; set; }   
        public void RegistrarDevolucao(DateTime DataDevolucao, int id)
        {
            var emprestimo = repositorioEmprestimo.SelecionarPorId(id);
            emprestimo.RegistrarDevolucacao(DataDevolucao);
            repositorioRevista.EditarRegistro(emprestimo.Revista.Id, emprestimo.Revista);
            repositorioEmprestimo.EditarRegistro(id, emprestimo);
        }
        public EmprestimoAppService(IRepositorioEmprestimo repositorioEmprestimo, IRepositorioRevista repositorioRevista)
        {
            this.repositorioEmprestimo = repositorioEmprestimo;
            this.repositorioRevista = repositorioRevista;
        }
    }
}
