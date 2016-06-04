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
    /// Interaction logic for DisciplinaAddHorario.xaml
    /// </summary>
    public partial class DisciplinaAddProf : UserControl
    {
        private int id;
        private string nomeDisciplina;

        public DisciplinaAddProf()
        {
            InitializeComponent();
        }

        public DisciplinaAddProf(int id,string nome) : this() {
            this.Id = id;
            nomeDisciplina = nome;
            tbnomeDisciplina.Text = nome;
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

        public string NomeDisciplina
        {
            get
            {
                return nomeDisciplina;
            }

            set
            {
                nomeDisciplina = value;
            }
        }
    }
}
