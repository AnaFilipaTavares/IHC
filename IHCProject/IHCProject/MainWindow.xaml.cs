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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //tamanho relativo centrado em 8/10 da tela
            this.Top= (SystemParameters.FullPrimaryScreenHeight - ((SystemParameters.FullPrimaryScreenHeight)*8/10)) / 2;
            this.Left= (SystemParameters.FullPrimaryScreenWidth - ((SystemParameters.FullPrimaryScreenWidth)*6/10)) / 2;
            this.Height = (SystemParameters.FullPrimaryScreenHeight) * 8 / 10;
            this.Width= (SystemParameters.FullPrimaryScreenWidth) * 6 / 10;
            ShowsNavigationUI = false;
        }
    }
}
