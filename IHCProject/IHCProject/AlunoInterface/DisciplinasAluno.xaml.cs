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
using IHCProject.infoClass;
using System.Data;

namespace IHCProject.AlunoInterface
{
    /// <summary>
    /// Interaction logic for DisciplinasAluno.xaml
    /// </summary>
    public partial class DisciplinasAluno : Page
    {

        private Aluno aluno;
        private SqlConnection CN;
        private SqlCommand CMD;

        public DisciplinasAluno()
        {
            InitializeComponent();
        }

        public DisciplinasAluno(Aluno aluno, SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.aluno = aluno;
            this.CN = cn;
            loadData();
        }

        private void loadData()
        {
            aula.Background = Brushes.LightSeaGreen;
            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();
                   
                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "SELECT * FROM PROJETO.UDF_ListaDisciplinasAluno (@aluno) ORDER BY 3,2";
                CMD.Parameters.AddWithValue("@aluno", aluno.IdAluno);
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {

                    listBox.Items.Add(new Disciplina(int.Parse(RDR["id"].ToString()), int.Parse(RDR["ano"].ToString()), RDR["nome"].ToString()));
                }
                RDR.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
           
        }

        private void aceder_Click(object sender, RoutedEventArgs e)
        {
            Disciplina temp = (Disciplina)listBox.SelectedItem;
            if (temp == null)
            {
                MessageBox.Show("Selecione uma opção");
            }
            else
            {
                this.NavigationService.Navigate(new HistoricoAulasAluno(aluno, temp, CN));
            }
        }

        private void Perfil_Click(object sender, RoutedEventArgs e)
        {
            AlunoHome p = new AlunoHome(aluno, CN);
            this.NavigationService.Navigate(p);
        }

        private void Horario_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HorarioAluno(aluno, CN));
        }

        private void Avaliacao_Click(object sender, RoutedEventArgs e)
        {
            ConsultarAvaliação p = new ConsultarAvaliação(aluno, CN);
            this.NavigationService.Navigate(p);
        }

        private void Notas_Click(object sender, RoutedEventArgs e)
        {
            Notas p = new Notas(aluno, CN);
            this.NavigationService.Navigate(p);
        }

        private void Falta_Click(object sender, RoutedEventArgs e)
        {
            Faltas p = new Faltas(aluno, CN);
            this.NavigationService.Navigate(p);
        }

        private void Plano_Click(object sender, RoutedEventArgs e)
        {
            PlanoCurso p = new PlanoCurso(aluno, CN);
            this.NavigationService.Navigate(p);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }

        private void Aula_Click(object sender, RoutedEventArgs e)
        {
            DisciplinasAluno d = new DisciplinasAluno(aluno, CN);
            this.NavigationService.Navigate(d);
        }

        private void listbox_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if ((Disciplina)listBox.SelectedItem != null) { 
                Disciplina temp = (Disciplina)listBox.SelectedItem;
                this.NavigationService.Navigate(new HistoricoAulasAluno(aluno, temp, CN));
            }
        }

    }


}
