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
    /// Interaction logic for DisciplinaDisplayHorario.xaml
    /// </summary>
    public partial class DisciplinaDisplayHorario : UserControl
    {

        private string nome;
        private int id;


        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public DisciplinaDisplayHorario()
        {
            InitializeComponent();
        }

        public DisciplinaDisplayHorario(int id,string nome, List<GerarHorario.Sala> list) : this()
        {
            this.id = id;
            this.nome = nome;

            cbSala.ItemsSource = list;
            cbSala.SelectedIndex = 0;
            nomeDisciplina.Text = nome;
        }

    }
}
