using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using AlunosApi.Models;

namespace AlunosApi.Services
{
    public interface IAlunoService
    {
        Task<IEnumerable<Aluno>> GetAlunos();
        Task<Aluno> GetAlunoById(int id);
        Task<IEnumerable<Aluno>> GetAlunosByName(string name);
        Task CreateAluno(Aluno aluno);
        Task DeleteAluno(Aluno aluno);
        Task UpdateAluno(Aluno aluno);
    }
}
