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
using System.Data;

namespace IHCProject.AlunoInterface
{
    /// <summary>
    /// Interaction logic for HistoricoAulasAluno.xaml
    /// </summary>
    public partial class HistoricoAulasAluno : Page
    {
        private Aluno aluno;
        private SqlConnection CN;
        private Disciplina d;
        private SqlCommand CMD;

        public HistoricoAulasAluno()
        {
            InitializeComponent();
        }

        public HistoricoAulasAluno(Aluno aluno, Disciplina d, SqlConnection CN) : this()
        {
            this.aluno = aluno;
            this.d = d;
            this.CN = CN;
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
                CMD.CommandText = "EXEC PROJETO.p_AulasAluno @aluno,@disciplina;";
                CMD.Parameters.AddWithValue("@aluno", aluno.IdAluno);
                CMD.Parameters.AddWithValue("@disciplina", d.CodigoDisciplina);
               // CMD.Parameters.AddWithValue("@ano", d.AnoDisciplina);

                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    listaAula.Items.Add(new Aula(int.Parse(RDR["disciplinaInfo"].ToString()), int.Parse(RDR["numero"].ToString()), RDR["nome"].ToString(), int.Parse(RDR["ano"].ToString()), RDR["data"].ToString().Split()[0], RDR["sumario"].ToString()));
                }
                RDR.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Aula_Click(object sender, RoutedEventArgs e)
        {
            DisciplinasAluno d = new DisciplinasAluno(aluno, CN);
            this.NavigationService.Navigate(d);
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Aula aula = (Aula)listaAula.SelectedItem;
            if (aula == null)
            {
                MessageBox.Show("Selecione uma opção");
            }
            else
            {
                this.NavigationService.Navigate(new InfoAula(aula));
            }
        }

        private void listaAula_MouseDoubleClick(object sender, RoutedEventArgs e)
        {
            if ((Aula)listaAula.SelectedItem != null) { 
                Aula aula = (Aula)listaAula.SelectedItem;
                InfoAula a = new InfoAula(aula);
                a.Show();
            }

        }


    }



}
    
