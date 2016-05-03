using System;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace IHCProject
{
    /// <summary>
    /// Interaction logic for MinhasDisciplinas.xaml
    /// </summary>
    public partial class DirecaoTurma : Page
    {
        private string idProf;
        private SqlConnection CN;
        private SqlCommand CMD;

        public DirecaoTurma()
        {
            InitializeComponent();
        }

        // Construtor para a classe Prof_Home
        public DirecaoTurma(string idProf, SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.idProf = idProf;
            this.CN = cn;
            loadData();
        }

        private void horarioClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("horaior");
        }

        private void Perfil_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Prof_Home(idProf, CN));
        }

        private void disciplinaClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MinhasDisciplinas(idProf, CN));

        }

        private void DirecaoTurma_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }

        private void loadData() {
            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "SELECT nome,idAluno FROM ((SELECT codigo FROM ESCOLA_SECUNDARIA.TURMA WHERE professor="+idProf+") AS t JOIN ESCOLA_SECUNDARIA.ALUNO ON t.codigo=ALUNO.turma) JOIN ESCOLA_SECUNDARIA.PESSOA ON ALUNO.ncc=PESSOA.ncc";
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {   
                    TurmaDoDT.Items.Add("Nome: " + RDR["nome"].ToString() + "Número: " + RDR["idAluno"].ToString());
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
