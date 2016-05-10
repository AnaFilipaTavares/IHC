using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IHCProject.infoClass;

namespace IHCProject.AlunoInterface
{
    /// <summary>
    /// Interaction logic for AlunoHome.xaml
    /// </summary>
    public partial class AlunoHome : Page
    {
        private Aluno aluno;
        private SqlConnection CN;
        private SqlCommand CMD;

        public AlunoHome()
        {
            InitializeComponent();
        }

        // Construtor para a classe 
        public AlunoHome(Aluno aluno, SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.aluno = aluno;
            this.CN = cn;
            loadData();
        }

        private void Perfil_Click(object sender, RoutedEventArgs e)
        {
            AlunoHome p = new AlunoHome(aluno, CN);
            this.NavigationService.Navigate(p);
        }

        private void horarioClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("horario");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }

        private void Avaliacao_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Avaliaçoes");
        }

        private void Falta_Click(object sender, RoutedEventArgs e)
        {

        }






        private void loadData()
        {


            try
            {
                if (CN.State == System.Data.ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "SELECT * FROM (SELECT * FROM ESCOLA_SECUNDARIA.ALUNO  WHERE idAluno=" + aluno.IdAluno + ") as T JOIN ESCOLA_SECUNDARIA.PESSOA ON T.ncc=PESSOA.ncc";
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
            }

        }
    }
}
