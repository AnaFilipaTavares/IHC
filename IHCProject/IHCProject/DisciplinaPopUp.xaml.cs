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
using System.Windows.Shapes;

namespace IHCProject
{
    /// <summary>
    /// Interaction logic for DisciplinaPopUp.xaml
    /// </summary>
    public partial class DisciplinaPopUp : Window
    {
        public DisciplinaPopUp()
        {
            InitializeComponent();
        }

        private void aceder_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
