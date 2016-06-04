using IHCProject.infoClass;
using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class GerarHorario : Page
    {
        private SqlConnection CN;
        private SqlCommand CMD;

        public GerarHorario()
        {
            InitializeComponent();
        }

        public GerarHorario(SqlConnection cN): this()
        {
            this.CN = cN;
            loadData();
        }

        private void loadData()
        {
           
        }
        private void Gest_DisciplinaClick(object sender, RoutedEventArgs e)
        {

        }


        private void Gest_horarioClick(object sender, RoutedEventArgs e)
        {
            GerarHorario a = new GerarHorario(CN);
            this.NavigationService.Navigate(a);
        }

        private void Gest_turmaClick(object sender, RoutedEventArgs e)
        {
            GerarTurmas a = new GerarTurmas(CN);
            this.NavigationService.Navigate(a);
        }

        private void Perfil_Click(object sender, RoutedEventArgs e)
        {
            Inicio x = new Inicio(CN);
            this.NavigationService.Navigate(x);
        }
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            Inserções x = new Inserções(CN);
            this.NavigationService.Navigate(x);
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
