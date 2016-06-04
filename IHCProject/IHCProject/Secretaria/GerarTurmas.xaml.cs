using IHCProject.infoClass;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for InsertProf.xaml
    /// </summary>
    public partial class GerarTurmas : Page
    {
        private SqlConnection CN;
        private SqlCommand CMD;

        public GerarTurmas()
        {
            InitializeComponent();
        }

        public GerarTurmas(SqlConnection cN): this()
        {
            this.CN = cN;
            loadData();
        }

        private void loadData()
        {
            cblistProfessores.Items.Clear();
            cblistTurmas.Items.Clear();
            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "EXEC PROJETO.p_professorSemDireçãoTurma";
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    cblistProfessores.Items.Add(new Professor(int.Parse(RDR["idProf"].ToString()), RDR["nome"].ToString()));
                }
                RDR.Close();

                cblistProfessores.SelectedIndex = 0;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // preencher a listBOX

            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "EXEC PROJETO.p_turmasSemDiretorTurma";
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    cblistTurmas.Items.Add(new Turma(int.Parse(RDR["codigo"].ToString()), RDR["ano"].ToString(),RDR["designação"].ToString()));
                }
                RDR.Close();



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            cblistTurmas.SelectedIndex = 0;
            cblistProfessores.SelectedIndex = 0;
        }
        private void Gest_DisciplinaClick(object sender, RoutedEventArgs e)
        {
            GerarDisciplinas a = new GerarDisciplinas(CN);
            this.NavigationService.Navigate(a);
        }


        private void Gest_horarioClick(object sender, RoutedEventArgs e)
        {
            GerarHorario a = new GerarHorario(CN);
            this.NavigationService.Navigate(a);
        }

        private void Gest_turmaClick(object sender, RoutedEventArgs e)
        {

        }

        private void Perfil_Click(object sender, RoutedEventArgs e)
        {
            Inicio x = new Inicio(CN);
            this.NavigationService.Navigate(x);
        }
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            Inserções x = new Inserções(CN);
            this.NavigationService.Navigate(x);
        }
        private void Gest_inicio(object sender, RoutedEventArgs e)
        {
            GestInico x = new GestInico(CN);
            this.NavigationService.Navigate(x);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (cblistProfessores.SelectedItem != null && cblistTurmas.SelectedItem != null) {
                try
                {
                    if (CN.State == ConnectionState.Closed) CN.Open();

                    CMD = new SqlCommand();
                    CMD.Connection = CN;
                    CMD.CommandText = "EXEC PROJETO.insertDiretorTurma @idProf,@idTurma;";
                    CMD.Parameters.AddWithValue("@idTurma", ((Turma)cblistTurmas.SelectedItem).IdTurma);
                    CMD.Parameters.AddWithValue("@idProf", ((Professor)cblistProfessores.SelectedItem).IdProf);

                    int temp = CMD.ExecuteNonQuery();
                    if (temp != 0)
                        Console.WriteLine("Insert com sucesso!");
                    else
                        Console.WriteLine("Não inseriu");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Não foi possivel fazer a seguinte associação");
                }
                loadData();
            }
          

        }

   
    }
}
