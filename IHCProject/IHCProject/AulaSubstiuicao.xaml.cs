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
    public partial class AulaSubstituicao : Page
    {
        private Professor professor;
        private SqlConnection CN;
        private SqlCommand CMD;

        public AulaSubstituicao()
        {
            InitializeComponent();
        }

        // Construtor para a classe Prof_Home
        public AulaSubstituicao(Professor prof, SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.professor = prof;
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
        private void AulaSubstituicao_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DirecaoTurma_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DirecaoTurma(professor, CN));
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
                CMD.CommandText = "SELECT PESSOA.nome,PROFESSOR.idProf, HORARIO_DISCIPLINA.id,DISCIPLINA.designação,HORARIO_DISCIPLINA.ano,TURMA.designação FROM ESCOLA_SECUNDARIA.HORARIO_DISCIPLINA JOIN ESCOLA_SECUNDARIA.PROFESSOR ON professor=idProf JOIN ESCOLA_SECUNDARIA.PESSOA ON PESSOA.ncc=PROFESSOR.ncc JOIN ESCOLA_SECUNDARIA.DISCIPLINA ON disciplina=codigo JOIN ESCOLA_SECUNDARIA.TURMA ON turma=TURMA.codigo;";
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {   
                    //TurmaDoDT.Items.Add(new Aluno(int.Parse(RDR["idAluno"].ToString()), RDR["nome"].ToString()));
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
          //  openPopupWindow((Aluno)TurmaDoDT.SelectedValue);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
           /* Aluno temp = (Aluno)TurmaDoDT.SelectedItem;
            if (temp == null)
            {
                MessageBox.Show("Selecione uma opção");
            }
            else {
                openPopupWindow(temp);
            }*/
        }

        private void openPopupWindow(Aluno selected) {
            InfoALuno popupWind = new InfoALuno((selected), CN);
            popupWind.Top = (SystemParameters.FullPrimaryScreenHeight - popupWind.Height) / 2;
            popupWind.Left = (SystemParameters.FullPrimaryScreenWidth - popupWind.Width) / 2;
            popupWind.ShowDialog();
        }

       
    }
    
}
