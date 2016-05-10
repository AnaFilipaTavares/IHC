using System.Data.SqlClient;
using IHCProject.infoClass;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


using System.Data;
namespace IHCProject
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class InfoALuno : Window
    {
        private Aluno aluno;
        private SqlConnection CN;
        private SqlCommand CMD;
        public InfoALuno()
        {
            InitializeComponent();
        }

        // Construtor para a classe Prof_Home
        public InfoALuno(Aluno al, SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            aluno = al;
            CN = cn;
            loadData();
        }

        private void loadData() {
            nomeAluno.Content = "Nome: "+aluno.Nome;
            numeroAluno.Content = "Número: "+aluno.IdAluno;
           
            //query para ler as faltas injustificadas
            
            /* try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "SELECT * FROM (SELECT * FROM ESCOLA_SECUNDARIA.PROFESSOR  WHERE idProf=" + idProf + ") as T JOIN ESCOLA_SECUNDARIA.PESSOA ON T.ncc=PESSOA.ncc";
                SqlDataReader RDR = CMD.ExecuteReader();
                if (RDR.Read())
                {
                    nomeProf.Content = RDR["nome"].ToString();
                    dataProf.Content = RDR["dataNascimento"].ToString().Split()[0];
                }
                RDR.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }*/
        }


    }
}
