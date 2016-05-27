using IHCProject.ContextoDisciplina;
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

namespace IHCProject.infoClass
{
    /// <summary>
    /// Interaction logic for CelulaCalendario.xaml
    /// </summary>
    public partial class CelulaCalendarioAluno : UserControl
    {
        private List<string> marcacoes;
        private int dia;
        private int year;
        private int month;
        private SqlCommand CMD;

        // só quero ter 1 referencia em memória
        private static SqlConnection CN = null;

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

        public CelulaCalendarioAluno()
        {
            InitializeComponent();
            marcacoes = new List<string>();
        }

        public CelulaCalendarioAluno(int dia, int row, int colum, int year, int month, SqlConnection cn) : this()
        {
            this.dia = dia;
            numDia.Text = dia.ToString();
            this.year = year;
            this.month = month;
            DateTime d = DateTime.Today;
            if (d.Day == dia && d.Month == month && d.Year == year)
                gB.Background = Brushes.Ivory;
            if (CN == null)
            {
                CN = cn;
            }

            Grid.SetRow(this, row);
            Grid.SetColumn(this, colum);
        }

        private void updateData()
        {
            list.Text = "";
            foreach (string e in marcacoes)
            {

                list.Text += e + " \n";
            }
        }
        public void setAvaliacoes(List<string> temp)
        {
            marcacoes = temp;
            updateData();
        }
    }

}
