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
    /// Interaction logic for Inserções.xaml
    /// </summary>
    public partial class Inserções : Page
    {
        private SqlConnection CN;

        public Inserções()
        {
            InitializeComponent();
        }

        public Inserções(SqlConnection CN) :this()
        {
            this.CN = CN;
        }

        private void Curso_Click(object sender, RoutedEventArgs e)
        {
            InsertCurso c = new InsertCurso(CN);
            this.NavigationService.Navigate(c);
        }
    }
}
