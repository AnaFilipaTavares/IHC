using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IHCProject.infoClass
{
    public class SecretariaData
    {
        private int idAdmin;
        private string nome;
        private string dataNascimento;
        private int idade;

        public SecretariaData(int id, string nome, int idade, string dataNascimento)
        {
            idAdmin = id;
            this.nome = nome;
            this.idade = idade;
            this.dataNascimento = dataNascimento;
        }

        public int IdAdmin
        {
            get
            {
                return idAdmin;
            }

            set
            {
                idAdmin = value;
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
    }
}
