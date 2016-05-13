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
using IHCProject.infoClass;
using System.Data;

namespace IHCProject
{
    /// <summary>
    /// Interaction logic for MinhasDisciplinas.xaml
    /// </summary>
    public partial class Horario : Page
    {
        private Professor prof;
        private SqlConnection CN;
        private SqlCommand CMD;
        public Horario()
        {
            InitializeComponent();
        }

        // Construtor para a classe Prof_Home
        public Horario(Professor prof, SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.prof = prof;
            this.CN = cn;
            loadData();
        }

        private void loadData()
        {
            
        }

        private void horarioClick(object sender, RoutedEventArgs e)
        {

        }

        private void disciplinaClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MinhasDisciplinas(prof, CN));
        }

        private void Perfil_Click(object sender, RoutedEventArgs e)
        {
            Prof_Home p = new Prof_Home(prof, CN);
            this.NavigationService.Navigate(p);
        }

        private void DirecaoTurma_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DirecaoTurma(prof, CN));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }

        private void AulaSubstituicao_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AulaSubstituicao(prof, CN));
        }
    }
}
