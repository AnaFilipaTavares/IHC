using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHCProject.infoClass
{
    public class HorarioDisciplina
    {
        private string nomeDisciplina;
        private int anoDisciplina;
        private string turmaDisciplina;
        private Professor professorDisciplina;

        public HorarioDisciplina(string nomeDisciplina, int anoDisciplina, string turmaDisciplina, Professor professorDisciplina)
        {
            this.nomeDisciplina = nomeDisciplina;
            this.anoDisciplina = anoDisciplina;
            this.turmaDisciplina = turmaDisciplina;
            this.professorDisciplina = professorDisciplina;
        }

        // private string horaInicio;
        // private string horaFim;

        public string NomeDisciplina
        {
            get
            {
                return nomeDisciplina;
            }

            set
            {
                nomeDisciplina = value;
            }
        }

        public int AnoDisciplina
        {
            get
            {
                return anoDisciplina;
            }

            set
            {
                anoDisciplina = value;
            }
        }

        public string TurmaDisciplina
        {
            get
            {
                return turmaDisciplina;
            }

            set
            {
                turmaDisciplina = value;
            }
        }

        public Professor ProfessorDisciplina
        {
            get
            {
                return professorDisciplina;
            }

            set
            {
                professorDisciplina = value;
            }
        }

        public override string ToString()
        {
            return "Disciplina: "+nomeDisciplina+" "+ anoDisciplina+" "+ turmaDisciplina+" Professor: "+ professorDisciplina.Nome;
        } 
    }
}
