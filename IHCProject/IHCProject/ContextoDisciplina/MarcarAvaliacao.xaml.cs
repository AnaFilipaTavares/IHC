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
    public partial class MarcarAValiacao : Page
    {
        private Professor professor;
        private HorarioDisciplina hDisciplina;
        private SqlConnection CN;
        private SqlCommand CMD;

        public MarcarAValiacao()
        {
            InitializeComponent();

            cboMonth.ItemsSource = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            cboYear.ItemsSource = new List<int> { DateTime.Today.Year, DateTime.Today.Year-1  };
            /*cboYear.SelectedItem = DateTime.Today.Year;
            cboMonth.SelectedIndex = DateTime.Today.Month-1;
            Console.WriteLine(cboMonth.SelectedIndex +" " + (DateTime.Today.Month - 1));
            MessageBox.Show(cboYear.SelectedItem.ToString());
            Console.WriteLine(cboYear.SelectedItem.ToString());*/

        }

        // Construtor para a classe Prof_Home
        public MarcarAValiacao(Professor prof,HorarioDisciplina hoDisciplina,SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.professor = prof;
            this.hDisciplina = hoDisciplina;
            this.CN = cn;
            
            loadData();
        }

        private void drawCells(int ano,int mes) {
           
            int startDayofWeek = int.Parse(new DateTime(ano, mes, 1).DayOfWeek.ToString("D"));
            int diasDomes= DateTime.DaysInMonth(ano,mes);
            bool exist = false;
            int row = 0;
            int colum = (startDayofWeek - 1);
            for (int i = 0; i < diasDomes; i++)
            {

                if (colum >= 0 && colum <= 4) {
                    Container.Children.Add(new CelulaCalendario(i + 1, row, colum,ano,mes));
                    exist = true;
                }

                if (colum == 5) {
                    if (exist)
                        row++;
                    colum = -2;
                }

                colum++;
               
            }

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

            // query para obter os alunos do inscritos
            label.Content = hDisciplina.Disciplina.Nome + " "+hDisciplina.Disciplina.AnoDisciplina +" - Marcar Avaliação";

            //simula
            cboYear.SelectedIndex = 0;

            cboMonth.SelectedIndex = (DateTime.Today.Month - 1);
            drawCells(int.Parse(cboYear.SelectedItem.ToString()), cboMonth.SelectedIndex+1);

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
            this.NavigationService.Navigate(new CriarAula(professor, hDisciplina, CN));
        }

        private void Historico_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HistoricoAulas(professor, hDisciplina, CN));
        }
        private void MarcarTeste_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            
                this.NavigationService.Navigate(new MinhasDisciplinas(professor, CN));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cboMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Container.Children.Clear();
            
            if (cboMonth.SelectedIndex != -1)
                drawCells(int.Parse(cboYear.SelectedItem.ToString()), cboMonth.SelectedIndex + 1);
        }

        private void cboYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Container.Children.Clear();
            
            if (cboMonth.SelectedIndex!=-1)
                drawCells(int.Parse(cboYear.SelectedItem.ToString()), cboMonth.SelectedIndex + 1);
        }
    }
    
}
