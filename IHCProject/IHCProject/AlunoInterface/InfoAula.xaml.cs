using IHCProject.infoClass;
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

namespace IHCProject.AlunoInterface
{
    /// <summary>
    /// Interaction logic for InfoAula.xaml
    /// </summary>
    public partial class InfoAula : Window
    {
        private Aula a;
        public InfoAula(Aula a)
        {
            InitializeComponent();
            this.a = a;
            loadData();
        }

        private void loadData()
        {
            label.Content = a.NAulaBinding;
            label2.Content = a.Disciplina;
            label4.Content = a.dataBinding;
            textBlock.Text = a.Sumário;
        }

        
    }
}
