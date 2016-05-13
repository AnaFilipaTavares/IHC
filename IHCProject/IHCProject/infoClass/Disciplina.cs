using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHCProject.infoClass
{
    public class Disciplina
    {
        private int codigoDisciplina;// primary key
        private int anoDisciplina;// primary key
        private string nome;

        public int CodigoDisciplina
        {
            get
            {
                return codigoDisciplina;
            }

            set
            {
                codigoDisciplina = value;
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

        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }

        public Disciplina(int codigoDisciplina, int anoDisciplina,string nome)
        {
            this.codigoDisciplina = codigoDisciplina;
            this.anoDisciplina = anoDisciplina;
            this.nome = nome;
        }

        public override string ToString()
        {
            return nome +" "+ anoDisciplina;
        }
    }
}
