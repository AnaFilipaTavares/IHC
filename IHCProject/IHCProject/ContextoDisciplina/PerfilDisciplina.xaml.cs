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

namespace IHCProject.ContextoDisciplina
{
    /// <summary>
    /// Interaction logic for MinhasDisciplinas.xaml
    /// </summary>
    public partial class PerfilDisciplina : Page
    {
        private Professor professor;
        private HorarioDisciplina hDisciplina;
        private SqlConnection CN;
        private SqlCommand CMD;

        public PerfilDisciplina()
        {
            InitializeComponent();
        }

        // Construtor para a classe Prof_Home
        public PerfilDisciplina(Professor prof,HorarioDisciplina hoDisciplina,SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.professor = prof;
            this.hDisciplina = hoDisciplina;
            this.CN = cn;
            loadData();
        }

        private void horarioClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Horario(professor, CN));
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
            
            // query para obter os alunos do DT
            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "SELECT nome,idAluno FROM (SELECT * FROM ESCOLA_SECUNDARIA.FREQUENTA WHERE horario=@idHorario) AS T JOIN  ESCOLA_SECUNDARIA.ALUNO ON T.aluno=idAluno JOIN ESCOLA_SECUNDARIA.PESSOA ON PESSOA.ncc=ALUNO.ncc";
                CMD.Parameters.AddWithValue("@idHorario", hDisciplina.IdHorario);
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    ListaAluno.Items.Add(new Aluno(int.Parse(RDR["idAluno"].ToString()), RDR["nome"].ToString()));
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
            openPopupWindow((Aluno)ListaAluno.SelectedValue);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Aluno temp = (Aluno)ListaAluno.SelectedItem;
            if (temp == null)
            {
                MessageBox.Show("Selecione uma opção");
            }
            else {
                openPopupWindow(temp);
            }
        }

        private void openPopupWindow(Aluno selected) {
            InfoALuno popupWind = new InfoALuno((selected), CN);
            popupWind.Top = (SystemParameters.FullPrimaryScreenHeight - popupWind.Height) / 2;
            popupWind.Left = (SystemParameters.FullPrimaryScreenWidth - popupWind.Width) / 2;
            popupWind.ShowDialog();
        }

        private void AulaSubstituicao_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AulaSubstituicao(professor, CN));
        }
    }
    
}
