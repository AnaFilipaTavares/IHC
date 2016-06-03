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

namespace IHCProject.Secretaria
{
    /// <summary>
    /// Interaction logic for DisciplinaAno.xaml
    /// </summary>
    public partial class DisciplinaAno : UserControl
    {
        private String disciplina;
        private int codigo;

        public int Codigo
        {
            get
            {
                return codigo;
            }

            set
            {
                codigo = value;
            }
        }

        public DisciplinaAno()
        {
            InitializeComponent();
        }

        public DisciplinaAno(String disciplina, bool read) : this()
        {
            InitializeComponent();
            this.disciplina = disciplina;
            nomeDisciplina.IsReadOnly = read;
            checkBox.IsChecked = false;
            Codigo = -1;
            loadData();
        }

        public DisciplinaAno(String disciplina,int codigo, bool read) : this()
        {
            InitializeComponent();
            this.disciplina = disciplina;
            this.Codigo = codigo;
            nomeDisciplina.IsReadOnly = read;
            checkBox.IsChecked = false;
            loadData();
        }

        private void loadData()
        {
            comboBox.ItemsSource = new List<int> { 1, 2, 3 };
            comboBox.SelectedIndex = 0;
            nomeDisciplina.Text = disciplina;  
        }

        private void nomeDisciplina_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (nomeDisciplina.IsReadOnly == false)
            {
                if (nomeDisciplina.Text.Length != 0)
                {
                    checkBox.IsChecked = true;

                }
                else
                    checkBox.IsChecked = false;


                DependencyObject parent = VisualTreeHelper.GetParent(this);
                while (!(parent is ListView))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
                ListView v = ((ListView)parent);
                if (!((DisciplinaAno)v.Items.GetItemAt(v.Items.Count - 1)).nomeDisciplina.Text.Equals(""))
                    v.Items.Add(new DisciplinaAno("", false));

                if (((DisciplinaAno)v.Items.GetItemAt(v.Items.Count - 1)).nomeDisciplina.Text.Equals("") && ((DisciplinaAno)v.Items.GetItemAt(v.Items.Count - 2)).nomeDisciplina.Text.Equals(""))
                    v.Items.RemoveAt(v.Items.Count - 1);
            }
        }
    }
}
