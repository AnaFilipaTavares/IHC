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

namespace IHCProject.AlunoInterface
{
    /// <summary>
    /// Interaction logic for Notas.xaml
    /// </summary>
    public partial class HorarioAluno : Page
    {

        private Aluno aluno;
        private SqlConnection CN;
        private SqlCommand CMD;


        public HorarioAluno()
        {
            InitializeComponent();
        }

        public HorarioAluno(Aluno aluno, SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.aluno = aluno;
            this.CN = cn;
            loadData();
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

        private void Aula_Click(object sender, RoutedEventArgs e)
        {
            DisciplinasAluno d = new DisciplinasAluno(aluno, CN);
            this.NavigationService.Navigate(d);
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

        private void loadData()
        {
            horario.Background = Brushes.LightSeaGreen;
            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "EXEC ESCOLA_SECUNDARIA.SP_HorarioAluno @aluno;";
                CMD.Parameters.AddWithValue("@aluno", aluno.IdAluno);
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {

                    Container.Children.Add(new DisplayDisciplinaHorario(new DisciplinaHorario(RDR["sala"].ToString(), RDR["diaSemana"].ToString(), RDR["designação"].ToString(), RDR["horaInicio"].ToString().Split()[0])));
                    //listBox.Items.Add(new HorarioDisciplina(int.Parse(RDR["id"].ToString()), new Disciplina(int.Parse(RDR["disciplina"].ToString()), int.Parse(RDR["ano"].ToString()), RDR["designação"].ToString()), RDR["letra"].ToString(), prof));
                    if (RDR["duração"].ToString().Equals("90"))
                    {
                        DisciplinaHorario di = new DisciplinaHorario(RDR["sala"].ToString(), RDR["diaSemana"].ToString(), RDR["designação"].ToString(), RDR["horaInicio"].ToString().Split()[0]);
                        di.RowIndex++;
                        Container.Children.Add(new DisplayDisciplinaHorario(di));
                    }
                }
                RDR.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

    }
}

