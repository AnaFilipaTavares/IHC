using System.Data.SqlClient;

using System;
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
    /// Interaction logic for Prof_Home.xaml
    /// </summary>
    public partial class Prof_Home : Page
    {
        private string idProf;
        private SqlConnection CN;
        public Prof_Home()
        {
            InitializeComponent();
        }

        // Construtor para a classe Prof_Home
        public Prof_Home(string idProf, SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.idProf = idProf;
            this.CN = cn;
        }

        private void horarioClick(object sender, RoutedEventArgs e)
        {
           MessageBox.Show("horaior");
        }

        private void disciplinaClick(object sender, RoutedEventArgs e)
        {
            DisciplinaPopUp d = new DisciplinaPopUp();
            d.ShowDialog();
        }

    }
}
