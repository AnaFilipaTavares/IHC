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

namespace IHCProject.Secretaria
{
    /// <summary>
    /// Interaction logic for InsertEE.xaml
    /// </summary>
    public partial class InsertEE : Page
    {
        private SqlConnection cN;
        private SqlCommand CMD;
        public InsertEE()
        {
            InitializeComponent();
        }

        public InsertEE(SqlConnection cN): this()
        {
            this.cN = cN;
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

        private void Aluno_Click(object sender, RoutedEventArgs e)
        {
            InsertAluno a = new InsertAluno(cN);
            this.NavigationService.Navigate(a);
        }

        private void EE_Click(object sender, RoutedEventArgs e)
        {
            InsertEE x = new InsertEE(cN);
            this.NavigationService.Navigate(x);
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            bool status = false;
            if (nome.Text.Length == 0 || ncc.Text.Length == 0 || idade.Text.Length == 0 || data.Text.Length == 0 || morada.Text.Length == 0 || sexo.Text.Length == 0)
                status = true;

            if (status == true)
            {
                MessageBox.Show("Todos os campos são de preenchimento obrigatório");
                return;
            }

            try
            {
                if (cN.State == System.Data.ConnectionState.Closed) cN.Open();

                CMD = new SqlCommand();
                CMD.Connection = cN;
                CMD.CommandText = "EXEC PROJETO.p_insertEncarregado @ncc,@nome,@idade,@data,@morada,@sexo;";
                CMD.Parameters.AddWithValue("@ncc", ncc.Text);
                CMD.Parameters.AddWithValue("@nome", nome.Text);
                CMD.Parameters.AddWithValue("@idade", int.Parse(idade.Text));
                CMD.Parameters.AddWithValue("@data", data.Text);
                CMD.Parameters.AddWithValue("@morada", morada.Text);
                CMD.Parameters.AddWithValue("@sexo", sexo.Text);
                CMD.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na criação do encarregado. Verifique se introduziu os dados corretos");
                Console.WriteLine(ex.Message);
                return;
            }

            MessageBox.Show("Encarregado de Educação Inserido com sucesso");
            clearFields();
        }
    
        private void Gestao_Click(object sender, RoutedEventArgs e)
        {
            GestInico x = new GestInico(cN);
            this.NavigationService.Navigate(x);
        }

        private void inicio_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Inserções(cN));
        }

        private void clearFields()
        {
            nome.Clear();
            ncc.Clear();
            idade.Clear();
            morada.Clear();
            sexo.Clear();
            data.Clear();
        }
    }
}
