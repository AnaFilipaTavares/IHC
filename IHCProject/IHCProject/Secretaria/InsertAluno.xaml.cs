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
        private static string key = "WASERDTFVGYHJCKC";
        private static string pass = "123456";

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

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }

        private void loadData()
        {
            try
            {
                if (cN.State == System.Data.ConnectionState.Closed) cN.Open();

                CMD = new SqlCommand();
                CMD.Connection = cN;
                CMD.CommandText = "EXEC PROJETO.p_planosdecurso";
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    curso.Items.Add(new Curso(int.Parse(RDR["codigo"].ToString()), RDR["nome"].ToString()));

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
            bool status = false;
            if (nome.Text.Length == 0 || ncc.Text.Length == 0 || idade.Text.Length == 0 || data.Text.Length == 0 || morada.Text.Length == 0 || sexo.Text.Length == 0 || ee.Text.Length == 0 || ano.Text.Length == 0 || curso.SelectedItem==null)
                status = true;

            if (status == true) {
                MessageBox.Show("Todos os campos são de preenchimento obrigatório");
                return;
            }

            try
            {
                if (cN.State == System.Data.ConnectionState.Closed) cN.Open();

                CMD = new SqlCommand();
                CMD.Connection = cN;
                CMD.CommandText = "EXEC PROJETO.p_createAluno @ncc,@nome,@idade,@data,@morada,@sexo,@ee,@curso,@ano,@pass,@key;";
                CMD.Parameters.AddWithValue("@ncc", ncc.Text);
                CMD.Parameters.AddWithValue("@nome", nome.Text);
                CMD.Parameters.AddWithValue("@idade", int.Parse(idade.Text));
                CMD.Parameters.AddWithValue("@data", data.Text);
                CMD.Parameters.AddWithValue("@morada", morada.Text);
                CMD.Parameters.AddWithValue("@sexo", sexo.Text);
                CMD.Parameters.AddWithValue("@ee", ee.Text);
                Curso c = (Curso)curso.SelectedItem;
                CMD.Parameters.AddWithValue("@curso", c.Id );
                CMD.Parameters.AddWithValue("@ano", int.Parse(ano.Text));
                CMD.Parameters.AddWithValue("@pass", pass);
                CMD.Parameters.AddWithValue("@key", key);
                CMD.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na criação do aluno. Verifique se introduziu os dados corretos");
                Console.WriteLine(ex.Message);
                return;
            }

            MessageBox.Show("Aluno Inserido com sucesso");
            clearFields();
          

        }

        private void Gestao_Click(object sender, RoutedEventArgs e)
        {
            GestInico x = new GestInico(cN);
            this.NavigationService.Navigate(x);
        }

        private void clearFields() {
            nome.Clear();
            ncc.Clear();
            idade.Clear();
            morada.Clear();
            curso.SelectedIndex = -1;
            sexo.Clear();
            ano.Clear();
            data.Clear();
            ee.Clear();
        }
    }
}
