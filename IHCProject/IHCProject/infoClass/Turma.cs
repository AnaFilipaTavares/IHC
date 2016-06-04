using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHCProject.infoClass
{
    public class Turma
    {
        private int idTurma;
        private string ano;
        private string letra;

        public int IdTurma
        {
            get
            {
                return idTurma;
            }

            set
            {
                idTurma = value;
            }
        }

        public string Ano
        {
            get
            {
                return ano;
            }

            set
            {
                ano = value;
            }
        }

        public string Letra
        {
            get
            {
                return letra;
            }

            set
            {
                letra = value;
            }
        }

        public Turma(int id, string ano, string letra)
        {
            IdTurma = id;
            this.Ano = ano;
            this.Letra = letra;
        }

        public override string ToString()
        {
            return ano +" " + letra;
        }

    }
}
