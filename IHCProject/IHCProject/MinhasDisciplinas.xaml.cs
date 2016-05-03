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

namespace IHCProject
{
    /// <summary>
    /// Interaction logic for MinhasDisciplinas.xaml
    /// </summary>
    public partial class MinhasDisciplinas : Page
    {
        private string idProf;
        private SqlConnection CN;
        public MinhasDisciplinas()
        {
            InitializeComponent();
        }

        // Construtor para a classe Prof_Home
        public MinhasDisciplinas(string idProf, SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.idProf = idProf;
            this.CN = cn;
        }

        private void horarioClick(object sender, RoutedEventArgs e)
        {

        }

        private void disciplinaClick(object sender, RoutedEventArgs e)
        {

        }

        private void aceder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Perfil_Click(object sender, RoutedEventArgs e)
        {
            Prof_Home p = new Prof_Home(idProf, CN);
            this.NavigationService.Navigate(p);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }
    }
}
