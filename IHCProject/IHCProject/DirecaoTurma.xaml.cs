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
    public partial class DirecaoTurma : Page
    {
        private string idProf;
        private SqlConnection CN;
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


    }
}
