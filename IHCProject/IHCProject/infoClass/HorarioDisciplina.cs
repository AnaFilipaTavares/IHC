using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHCProject.infoClass
{
    public class HorarioDisciplina
    {
        private int idHorario;
        private Disciplina disciplina;
        private string turmaDisciplina;
        private Professor professorDisciplina;

        public int IdHorario
        {
            get
            {
                return idHorario;
            }

            set
            {
                idHorario = value;
            }
        }

        public Disciplina Disciplina
        {
            get
            {
                return disciplina;
            }

            set
            {
                disciplina = value;
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

        public string TurmaBinding
        {
            get
            {
                return Disciplina.AnoDisciplina + " "+ turmaDisciplina;
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

        public HorarioDisciplina(int idHorario, Disciplina disciplina, string turmaDisciplina, Professor professorDisciplina)
        {
            this.IdHorario = idHorario;
            this.Disciplina = disciplina;
            this.TurmaDisciplina = turmaDisciplina;
            this.ProfessorDisciplina = professorDisciplina;
        }


        // private string horaInicio;
        // private string horaFim;


        public override string ToString()
        {
            return "Disciplina: "+ disciplina +" "+ TurmaDisciplina+" Professor: "+ ProfessorDisciplina.Nome;
        } 
    }
}
