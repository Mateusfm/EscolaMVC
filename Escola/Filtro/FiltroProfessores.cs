using Escola.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Escola.Filtro
{
    public class FiltroProfessores
    {
        public IList<Professor> FiltraProfessoresCujaMediaIdadeDosAlunos(int de, int ate)
        {
            using (var db = new EscolaEntities())
            {
                var professores = db.Professor;
                IList<Professor> profQueAtendeCondicao = new List<Professor>();

                foreach (var item in professores)
                {
                    var alunosDoProfessor = db.Aluno.Where(x => x.ProfessorId == item.ProfessorId);

                    var mediaDeIdadeDosAlunos = RetornaMediaIdadesDos(alunosDoProfessor.ToList());

                    if ((mediaDeIdadeDosAlunos >= de) && (mediaDeIdadeDosAlunos <= ate))
                        profQueAtendeCondicao.Add(item);
                }

                return profQueAtendeCondicao;
            }
        }

        private double RetornaMediaIdadesDos(IList<Aluno> alunos)
        {
            IList<int> idades = new List<int>();

            foreach (var item in alunos)
                idades.Add(CalculaIdade(item.DataNascimento));

            return idades.Any() ? idades.Average() : 0;
        }

        private int CalculaIdade(DateTime dataNascimento)
        {
            var idade = DateTime.Now.Year - dataNascimento.Year;

            if ((DateTime.Now.Month < dataNascimento.Month) || (DateTime.Now.Month == dataNascimento.Month && DateTime.Now.Day < dataNascimento.Day))
                idade--;

            return idade;
        }
    }
}