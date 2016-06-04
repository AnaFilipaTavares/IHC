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

namespace IHCProject.Secretaria
{
    /// <summary>
    /// Interaction logic for MinhasDisciplinas.xaml
    /// </summary>
    public partial class Inicio : Page
    {


        private SqlConnection CN;


        public Inicio()
        {
            InitializeComponent();
        }

        public Inicio(SqlConnection cN):this()
        {
          
            CN = cN;

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Inserções i = new Inserções(CN);
            this.NavigationService.Navigate(i);
        }

        private void Gestao_Click(object sender, RoutedEventArgs e)
        {
            GestInico i = new GestInico(CN);
            this.NavigationService.Navigate(i);
        }
    }

}
