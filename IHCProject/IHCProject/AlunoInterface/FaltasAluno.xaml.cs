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
using System.Windows.Shapes;

namespace IHCProject
{
    /// <summary>
    /// Interaction logic for FaltasAluno.xaml
    /// </summary>
    public partial class FaltasAluno : Window
    {
        private int idAluno;
        private string disciplina;
        private Data data1;


        public FaltasAluno(int idAluno, string disciplina, Data data1)
        {
            InitializeComponent();
            this.Top = (SystemParameters.FullPrimaryScreenHeight - this.Height) / 2;
            this.Left = (SystemParameters.FullPrimaryScreenWidth - this.Width) / 2;
            this.idAluno = idAluno;
            this.disciplina = disciplina;
            this.data1 = data1;

            loadData();
        }

        private void loadData()
        {

          if(data1.Tipo == "P")
                label.Content = "Falta de Presença";

          else if (data1.Tipo == "D")
                label.Content = "Falta Disciplinar";
          else
                label.Content = "Falta";


            label2.Content = disciplina;

            label4.Content = data1.Date;

            textBlock.Text = data1.Sumario;

            if (data1.Descrição.Equals(""))
                justificação.Visibility = Visibility.Hidden;
            
            else
                textBlock1.Text = data1.Descrição;

        }
    }
}
