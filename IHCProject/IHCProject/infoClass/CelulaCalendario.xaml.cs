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
    public partial class CelulaCalendario : UserControl
    {
        private List<string> marcacoes;
        private int dia;
        private int year;
        private int month;
        private SqlCommand CMD;

        // só quero ter 1 referencia em memória
        private HorarioDisciplina hDisciplina;
        private static SqlConnection CN=null;

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

        public CelulaCalendario(int dia,int row,int colum,int year,int month,HorarioDisciplina hDis,SqlConnection cn) : this()
        {
            this.dia = dia;
            numDia.Text = dia.ToString();



            
            this.year = year;
            this.month = month;
            DateTime d = DateTime.Today;
            if (d.Day == dia && d.Month == month && d.Year == year)
                gB.Background = Brushes.Ivory;


            //associação
            
                hDisciplina = hDis;
            if (CN == null)
            {
                CN = cn;
            }

            Grid.SetRow(this, row);
            Grid.SetColumn(this, colum);
        }



        private void updateData() {
            list.Text = "";
            foreach (string e in marcacoes)
            {
               
                list.Text += e + " \n";
            }
        }
        private void GroupBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DateTime.Compare( DateTime.Today, new DateTime(year, month, dia))>0) {
                MessageBox.Show("Não tem permições para marcar avaliações em data inferior á data atual");
                return;
            }


            MarcarAvaliacaoPop popupWind = new MarcarAvaliacaoPop(hDisciplina, new DateTime(year, month, dia), CN);
            Console.WriteLine(new DateTime(year, month, dia));
            popupWind.Top = (SystemParameters.FullPrimaryScreenHeight - popupWind.Height) / 2;
            popupWind.Left = (SystemParameters.FullPrimaryScreenWidth - popupWind.Width) / 2;
            popupWind.ShowDialog();
            
            DependencyObject parent = VisualTreeHelper.GetParent(this);
            while (!(parent is MarcarAValiacao))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            ((MarcarAValiacao)parent).drawCells(year,month);


        }

        public void setAvaliacoes(List<string> temp)
        {
            marcacoes = temp;
            updateData();
        }
    }

}
