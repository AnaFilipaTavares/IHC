using System;

using System.Data.SqlClient;
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
using System.Data;
using IHCProject.infoClass;

namespace IHCProject.Secretaria
{
    /// <summary>
    /// Interaction logic for MinhasDisciplinas.xaml
    /// </summary>
    public partial class Inicio : Page
    {
        private SecretariaData secretaria;

        private SqlConnection CN;
        private SqlCommand CMD;

        public Inicio()
        {
            InitializeComponent();
        }

        public Inicio(SecretariaData secretaria, SqlConnection cN):this()
        {
            this.secretaria = secretaria;
            CN = cN;

        }
    }

}
