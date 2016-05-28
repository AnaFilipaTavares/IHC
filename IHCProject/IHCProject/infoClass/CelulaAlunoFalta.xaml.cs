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
    /// Interaction logic for CelulaAlunoFalta.xaml
    /// </summary>
    public partial class CelulaAlunoFalta : UserControl
    {
        private Aluno aluno;
        private bool marcaFalta;
        public CelulaAlunoFalta()
        {
            InitializeComponent();
        }

        public CelulaAlunoFalta(Aluno al): this()
        {
            aluno = al;
            marcaFalta = false;
            loadData();
        }

        private void loadData()
        {
            tbName.Text ="Nome: " +aluno.Nome;
            tbNumero.Text = "Número: " + aluno.IdAluno;
        }

        private void checkMarcarFalta_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox a = (CheckBox)sender;
            marcaFalta = true;
        }

        private void checkMarcarFalta_Unchecked(object sender, RoutedEventArgs e)
        {
            marcaFalta = false;
        }

        public bool getMarcarfalta()
        {
            return marcaFalta;
        }

        public string getNome() {
            return aluno.Nome;
        }

        public int getNumero() {
            return aluno.IdAluno;
        }

        public string getTipofalta() {
            return ((ListBoxItem)cbTipoFalta.SelectedItem).Content.ToString();
        }

        public override string ToString()
        {
            return aluno.Nome + " " + aluno.IdAluno;
        }
    }
}
