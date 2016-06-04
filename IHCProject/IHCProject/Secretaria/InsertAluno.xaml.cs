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
using IHCProject.infoClass;

namespace IHCProject.Secretaria
{
    /// <summary>
    /// Interaction logic for InsertAluno.xaml
    /// </summary>
    public partial class InsertAluno : Page
    {
        private SqlConnection cN;
        private SqlCommand CMD;

        public InsertAluno()
        {
            InitializeComponent();

        }
        public InsertAluno(SqlConnection cN) : this()
        {
            this.cN = cN;
            loadData();
        }

        private void Curso_Click(object sender, RoutedEventArgs e)
        {
            InsertCurso c = new InsertCurso(cN);
            this.NavigationService.Navigate(c);
        }

        private void Prof_Click(object sender, RoutedEventArgs e)
        {
            InsertProf p = new InsertProf(cN);
            this.NavigationService.Navigate(p);
        }

        private void EE_Click(object sender, RoutedEventArgs e)
        {
            InsertEE x = new InsertEE(cN);
            this.NavigationService.Navigate(x);
        }

        private void Aluno_Click(object sender, RoutedEventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            try
            {
                if (cN.State == System.Data.ConnectionState.Closed) cN.Open();

                CMD = new SqlCommand();
                CMD.Connection = cN;
                CMD.CommandText = "SELECT * from PROJETO.PLANO_CURSO;";
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    t9.Items.Add(new Curso(int.Parse(RDR["codigo"].ToString()), RDR["nome"].ToString()));

                }
                RDR.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na base de dados");
                Console.WriteLine(ex.Message);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Gestao_Click(object sender, RoutedEventArgs e)
        {
            GestInico x = new GestInico(cN);
            this.NavigationService.Navigate(x);
        }
    }
}
