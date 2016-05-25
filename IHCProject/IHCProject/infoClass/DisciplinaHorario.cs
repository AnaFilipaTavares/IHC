using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHCProject.infoClass
{
    public class DisciplinaHorario
    {
        private string sala = null;
        private string disciplina = null;
        private string diaSemana = null;
        private string horaInicio = null;
        private int rowIndex = -1;
        private int columIndex = -1;

        public string Sala
        {
            get
            {
                return sala;
            }

            set
            {
                sala = value;
            }
        }

        public string Disciplina
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

        public int RowIndex
        {
            get
            {
                return rowIndex;
            }

            set
            {
                rowIndex = value;
            }
        }

        public int ColumIndex
        {
            get
            {
                return columIndex;
            }

            set
            {
                columIndex = value;
            }
        }

        public DisciplinaHorario(string sala, string diaSemana, string disciplina, string horaInicio)
        {
            this.sala = sala;
            this.diaSemana = diaSemana;
            this.disciplina = disciplina;
            this.horaInicio = horaInicio;
            calColum();
            calRow();
        }

        private void calColum() {
            switch (diaSemana)
            {
                case "Segunda":
                    columIndex = 0;
                    break;
                case "Terça":
                    columIndex = 1;
                    break;
                case "Quarta":
                    columIndex = 2;
                    break;
                case "Quinta":
                    columIndex = 3;
                    break;
                case "Sexta":
                    columIndex = 4;
                    break;
                default:
                    columIndex = -1;
                    break;
            }
        }

        private void calRow()
        {
            switch (horaInicio)
            {
                case "09:00:00":
                    rowIndex = 0;
                    break;
                case "09:50:00":
                    rowIndex = 1;
                    break;
                case "11:00:00":
                    rowIndex = 2;
                    break;
                case "11:50:00":
                    rowIndex = 3;
                    break;
                case "12:45:00":
                    rowIndex = 4;
                    break;
                default:
                    rowIndex = -1;
                    break;
            }
        }
    }
}
