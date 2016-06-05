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
        public PerfilDisciplina(Professor prof, HorarioDisciplina hoDisciplina, SqlConnection cn) : this()
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
            this.NavigationService.Navigate(new DirecaoTurma(professor, CN));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }

        private void loadData()
        {
            label.Content = hDisciplina.Disciplina.Nome + " " + hDisciplina.Disciplina.AnoDisciplina + " - Início";
            // query para obter os alunos do inscritos
            nomeDiciplina.Content = "Diciplina: " + hDisciplina.Disciplina.Nome + " " + hDisciplina.Disciplina.AnoDisciplina;
            turma.Content = "Turma: " + hDisciplina.Disciplina.AnoDisciplina + " " + hDisciplina.TurmaDisciplina;
            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "EXEC PROJETO.p_perfilDisciplina @idHorario";
                CMD.Parameters.AddWithValue("@idHorario", hDisciplina.IdHorario);
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    ListaAluno.Items.Add(new Aluno(int.Parse(RDR["aluno"].ToString()), RDR["nome"].ToString()));
                }
                RDR.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void AulaSubstituicao_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AulaSubstituicao(professor, CN));
        }

        private void Inicio_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CriarAula_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CriarAula(professor, hDisciplina, CN));
        }

        private void Historico_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HistoricoAulas(professor, hDisciplina, CN));
        }
        private void MarcarTeste_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MarcarAValiacao(professor, hDisciplina, CN));
        }
        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MinhasDisciplinas(professor, CN));
        }
    }

}
