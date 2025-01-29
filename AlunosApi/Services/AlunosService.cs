using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlunosApi.Context;
using AlunosApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AlunosApi.Services
{
    public class AlunosService : IAlunoService
    {
        private readonly AppDbContext _context;
        public AlunosService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Aluno> GetAlunoById(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            return aluno;
        }

        public async Task<IEnumerable<Aluno>> GetAlunos()
        {
            try
            {
                return await _context.Alunos.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro ao obter a lista de alunos: {ex.Message}");

                throw;

            }
        }

        public async Task<IEnumerable<Aluno>> GetAlunosByName(string name)
        {
            IEnumerable<Aluno> alunos;
            if (!string.IsNullOrWhiteSpace(name))
            {
                alunos = await _context.Alunos.Where(n => n.Name.Contains(name)).ToListAsync();
            }
            else
            {
                alunos = await GetAlunos();
            }
            return alunos;
        }
        public async Task CreateAluno(Aluno aluno)
        {
            try
            {
                if (aluno == null)
                    throw new ArgumentNullException(nameof(aluno), "Aluno não pode ser nulo.");

                await _context.Alunos.AddAsync(aluno);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro ao criar aluno: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAluno(Aluno aluno)
        {
            try
            {
                if (aluno == null)
                    throw new ArgumentNullException(nameof(aluno), "Aluno não pode ser nulo.");

                _context.Alunos.Remove(aluno);  // Remove o aluno do contexto
                await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro ao deletar aluno: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateAluno(Aluno aluno)
        {
            try
            {
                if (aluno == null)
                    throw new ArgumentNullException(nameof(aluno), "Aluno não pode ser nulo.");

                // Verifica se o aluno existe no banco antes de atualizar
                var alunoExistente = await _context.Alunos.FindAsync(aluno.Id);
                if (alunoExistente == null)
                    throw new KeyNotFoundException($"Aluno com ID {aluno.Id} não encontrado.");

                _context.Alunos.Update(aluno);  // Marca o aluno como alterado
                //_context.Entry(aluno).State = EntityState.Modified; 
                await _context.SaveChangesAsync(); // Salva as alterações no banco de dados
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Erro ao atualizar aluno: {ex.Message}");
                throw;
            }
        }
    }
}
