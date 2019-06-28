using Escola.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Escola.Filtro
{
    public class FiltroAlunos
    {
        public IList<Aluno> FiltraAlunosMaioresDe(int anos)
        {
            using (var db = new EscolaEntities())
            {
                var data = DateTime.Now.AddYears(anos * -1);

                var alunos = db.Aluno
                        .Include("Professor")
                        .Where(x => (x.DataNascimento <= data));

                return alunos.ToList();
            }
        }
    }
}