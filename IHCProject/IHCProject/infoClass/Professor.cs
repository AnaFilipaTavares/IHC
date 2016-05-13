using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHCProject.infoClass
{
    public class Professor
    {
        private int idProf;
        private string nome;
        private string dataNascimento;
        private int idade;


      

        public Professor(int idProf, string nome)
        {
            this.idProf = idProf;
            this.nome = nome;
        }

        public Professor(int idProf, string nome, string dataNascimento, int idade) : this(idProf, nome)
        {
            this.dataNascimento = dataNascimento;
            this.idade = idade;
        }

        public int IdProf
        {
            get
            {
                return idProf;
            }

            set
            {
                idProf = value;
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

        public string DataNascimento
        {
            get
            {
                return dataNascimento;
            }

            set
            {
                dataNascimento = value;
            }
        }

        public int Idade
        {
            get
            {
                return idade;
            }

            set
            {
                idade = value;
            }
        }

        public override string ToString()
        {
            return "Nome: " + nome + " Número: " + idProf;
        }
    }
}
