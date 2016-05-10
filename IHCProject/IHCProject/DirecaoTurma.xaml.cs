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
using IHCProject.infoClass;

namespace IHCProject
{
    /// <summary>
    /// Interaction logic for MinhasDisciplinas.xaml
    /// </summary>
    public partial class DirecaoTurma : Page
    {
        private Professor professor;
        private SqlConnection CN;
        private SqlCommand CMD;

        public DirecaoTurma()
        {
            InitializeComponent();
        }

        // Construtor para a classe Prof_Home
        public DirecaoTurma(Professor prof, SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.professor = prof;
            this.CN = cn;
            loadData();
        }

        private void horarioClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("horaior");
        }

        private void Perfil_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Prof_Home(professor, CN));
        }

        private void disciplinaClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MinhasDisciplinas(professor, CN));

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
                CMD.CommandText = "SELECT nome,idAluno FROM ((SELECT codigo FROM ESCOLA_SECUNDARIA.TURMA WHERE professor="+ professor.IdProf+ ") AS t JOIN ESCOLA_SECUNDARIA.ALUNO ON t.codigo=ALUNO.turma) JOIN ESCOLA_SECUNDARIA.PESSOA ON ALUNO.ncc=PESSOA.ncc";
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {   
                    TurmaDoDT.Items.Add(new Aluno(int.Parse(RDR["idAluno"].ToString()), RDR["nome"].ToString()));
                }
                RDR.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void TurmaDoDT_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InfoALuno popupWind = new InfoALuno(((Aluno)TurmaDoDT.SelectedValue),CN);
            popupWind.Top= (SystemParameters.FullPrimaryScreenHeight - popupWind.Height) / 2;
            popupWind.Left = (SystemParameters.FullPrimaryScreenWidth - popupWind.Width) / 2;
            popupWind.ShowDialog();
        }
    }
    
}
