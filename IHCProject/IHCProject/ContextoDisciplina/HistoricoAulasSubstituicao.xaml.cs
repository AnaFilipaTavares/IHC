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
    public partial class HistoricoAulasSubstituicao : Page
    {
        private Professor professor;

        private HorarioDisciplina hDisciplina;
        private SqlConnection CN;
        private SqlCommand CMD;

        public HistoricoAulasSubstituicao()
        {
            InitializeComponent();
        }

        // Construtor para a classe Prof_Home
        public HistoricoAulasSubstituicao(Professor prof,HorarioDisciplina hoDisciplina,SqlConnection cn) : this()
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

        private void loadData() {
            label.Content = hDisciplina.Disciplina.Nome + " " + hDisciplina.Disciplina.AnoDisciplina + " - Histórico Aula de Substituição";
            // query para obter os alunos do inscritos
            listaAula.Items.Clear();
            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN; 
                CMD.CommandText = "EXEC PROJETO.p_historioAulasSubstituicao @idHorario,@idProf";
                CMD.Parameters.AddWithValue("@idHorario", hDisciplina.IdHorario);
                CMD.Parameters.AddWithValue("@idProf", professor.IdProf);
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    listaAula.Items.Add(new Aula(int.Parse(RDR["disciplinaInfo"].ToString()), int.Parse(RDR["numero"].ToString()), int.Parse(RDR["id"].ToString()), hDisciplina.Disciplina.Nome,hDisciplina.Disciplina.AnoDisciplina, RDR["data"].ToString().Split()[0], RDR["sumario"].ToString() ));
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
            this.NavigationService.Navigate(new PerfilDisciplina(professor,hDisciplina, CN));
        }

        private void CriarAula_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CriarAulaSubstituicao(professor,hDisciplina, CN));
        }

        private void Historico_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AulaSubstituicao(professor, CN));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Aula temp = (Aula)listaAula.SelectedItem;
            if (temp == null)
            {
                MessageBox.Show("Selecione uma opção");
            }
            else {
                openPopupWindow(temp);
                loadData();
            }
        }

        private void openPopupWindow(Aula selected)
        {
            EditarAula popupWind = new EditarAula(professor, selected, CN);
            popupWind.Top = (SystemParameters.FullPrimaryScreenHeight - popupWind.Height) / 2;
            popupWind.Left = (SystemParameters.FullPrimaryScreenWidth - popupWind.Width) / 2;
            popupWind.ShowDialog();
        }

        private void listaAula_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((Aula)listaAula.SelectedItem != null) {
                openPopupWindow((Aula)listaAula.SelectedItem);
                loadData();
            }
        }
    }

}
