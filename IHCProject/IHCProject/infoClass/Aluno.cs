using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHCProject.infoClass
{
    public class Aluno
    {
        private string dataNascimento;
        private int idade;
        private int idAluno;
        private string nome;
        

        public Aluno(int id,string nome) {
            idAluno = id;
            this.nome = nome;
        }

        public Aluno(int id, string nome, int idade, string dataNascimento) : this(id, nome)
        {
            this.idade = idade;
            this.dataNascimento = dataNascimento;
        }

        public int IdAluno
        {
            get
            {
                return idAluno;
            }

            set
            {
                idAluno = value;
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

        public override string ToString()
        {
            return "Nome: " + nome + " Número: " + idAluno;
        }
    }
}
