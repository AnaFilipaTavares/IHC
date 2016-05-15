using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHCProject.infoClass
{
    public class Aula
    {
        private int horario;
        private int nAula;
        private string disciplina;
        private int anoDisciplina;
        private string data;
        private string sumário;


        public Aula(int horario, int nAula)
        {
            this.horario = horario;
            this.nAula = nAula;
        }

        public Aula(int horario, int nAula, string disciplina, int anoDisciplina, string data) : this(horario, nAula)
        {
            this.disciplina = disciplina;
            this.anoDisciplina = anoDisciplina;
            this.data = data;
        }

        public Aula(int horario, int nAula, string disciplina, int anoDisciplina, string data, string sumário)
        {
            this.horario = horario;
            this.nAula = nAula;
            this.disciplina = disciplina;
            this.anoDisciplina = anoDisciplina;
            this.data = data;
            this.sumário = sumário;
        }

        public int NumeroAula
        {
            get
            {
                return nAula;
            }

            set
            {
                nAula = value;
            }
        }

        public int Horario
        {
            get
            {
                return horario;
            }

            set
            {
                horario = value;
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

        public string Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        public string Sumário
        {
            get
            {
                return sumário;
            }

            set
            {
                sumário = value;
            }
        }

        public override string ToString()
        {
            return "Disciplina: " + disciplina + " " + anoDisciplina + " data: " + data;
        }

        public string NAulaBinding {
            get
            { 
            return "Aula nº " + nAula;
            }
        }

        public string dataBinding
        {
            get
            {
                return "Data " + data.Split()[0];
            }
        }

    }
}
