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
        private SqlCommand CMD;
        private string commandText;
        private SqlConnection CN;
        private string data;

        public FaltasAluno()
        {
            InitializeComponent();
        }

        public FaltasAluno(string data, string commandText)
        {
            this.data = data;
            this.commandText = commandText;
            loadData();
        }

        private void loadData()
        {
            try
            {
                if (CN.State == System.Data.ConnectionState.Closed) CN.Open();
                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = commandText;
                SqlDataReader RDR = CMD.ExecuteReader();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
    }
}
