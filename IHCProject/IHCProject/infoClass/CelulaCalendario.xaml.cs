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

namespace IHCProject.infoClass
{
    /// <summary>
    /// Interaction logic for CelulaCalendario.xaml
    /// </summary>
    public partial class CelulaCalendario : UserControl
    {
        private List<string> marcacoes;
        private int dia;
        private int year;
        private int month;

        public int Dia
        {
            get
            {
                return dia;
            }

            set
            {
                dia = value;
            }
        }

        public CelulaCalendario()
        {
            InitializeComponent();
            marcacoes = new List<string>();
        }

        public CelulaCalendario(int dia,int row,int colum,int year,int month) : this()
        {
            this.dia = dia;
            numDia.Text = dia.ToString();
            foreach (string e in marcacoes) {
                list.Text += e+" \n";
            }
            this.year = year;
            this.month = month;
            DateTime d = DateTime.Today;
            if (d.Day == dia && d.Month == month && d.Year == year)
                gB.Background = Brushes.Ivory;
                
            Grid.SetRow(this, row);
            Grid.SetColumn(this, colum);
        }

        private void GroupBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine(new DateTime(year,month,dia));
        }
    }
}
