using System.Data.SqlClient;

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace IHCProject
{
    /// <summary>
    /// Interaction logic for Prof_Home.xaml
    /// </summary>
    public partial class Prof_Home : Page
    {
        private string idProf;
        private SqlConnection CN;
        private SqlCommand CMD;
        public Prof_Home()
        {
            InitializeComponent();
        }

        // Construtor para a classe Prof_Home
        public Prof_Home(string idProf, SqlConnection cn) : this()
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

        private void disciplinaClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MinhasDisciplinas(idProf, CN));
            
        }

        private void DirecaoTurma_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DirecaoTurma(idProf,CN));
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
                CMD.CommandText = "SELECT * FROM (SELECT * FROM ESCOLA_SECUNDARIA.PROFESSOR  WHERE idProf="+idProf+") as T JOIN ESCOLA_SECUNDARIA.PESSOA ON T.ncc=PESSOA.ncc";
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
