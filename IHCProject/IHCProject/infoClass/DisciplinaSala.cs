using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHCProject.infoClass
{
    public class DisciplinaSala
    {
        private String disciplina;
        private String sala;

        public DisciplinaSala(string disciplina, string sala)
        {
            this.Disciplina = disciplina;
            this.Sala = sala;
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
    }
}
