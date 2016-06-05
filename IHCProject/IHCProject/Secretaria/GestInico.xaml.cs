using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace IHCProject.Secretaria
{
    /// <summary>
    /// Interaction logic for InsertProf.xaml
    /// </summary>
    public partial class GestInico : Page
    {
        private SqlConnection cN;

        public GestInico()
        {
            InitializeComponent();
        }

        public GestInico(SqlConnection cN): this()
        {
            this.cN = cN;
        }

        private void Gest_DisciplinaClick(object sender, RoutedEventArgs e)
        {
            GerarDisciplinas a = new GerarDisciplinas(cN);
            this.NavigationService.Navigate(a);
        }

        private void Gest_horarioClick(object sender, RoutedEventArgs e)
        {
            GerarHorario a = new GerarHorario(cN);
            this.NavigationService.Navigate(a);
        }

        private void Gest_turmaClick(object sender, RoutedEventArgs e)
        {
            GerarTurmas a = new GerarTurmas(cN);
            this.NavigationService.Navigate(a);
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }


        private void inserçao_Click(object sender, RoutedEventArgs e)
        {
            Inserções x = new Inserções(cN);
            this.NavigationService.Navigate(x);
        }
    }
}
