using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using IHCProject.infoClass;

namespace IHCProject.AlunoInterface
{
    /// <summary>
    /// Interaction logic for AlunoHome.xaml
    /// </summary>
    public partial class AlunoHome : Page
    {
        private Aluno aluno;
        private SqlConnection CN;
        private SqlCommand CMD;

        public AlunoHome()
        {
            InitializeComponent();
        }

        // Construtor para a classe 
        public AlunoHome(Aluno aluno, SqlConnection cn) : this()
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

        private void Avaliacao_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Avaliaçoes");
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
            perfil.Background = Brushes.LightSeaGreen;
            try
            {
                if (CN.State == System.Data.ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "SELECT nomeEncarregado,nomeAluno,dAluno,nome,nomeCurso,ano,designação FROM ESCOLA_SECUNDARIA.PESSOA JOIN"
                                  + "(SELECT * FROM ESCOLA_SECUNDARIA.PROFESSOR JOIN"
                                  + "(SELECT nomeEncarregado, nomeAluno, turma, dAluno, nome as nomeCurso, ano, designação, professor FROM ESCOLA_SECUNDARIA.TURMA JOIN"
                                  + "(SELECT * FROM ESCOLA_SECUNDARIA.PLANO_CURSO JOIN"
                                  + "(SELECT nome as nomeEncarregado, dataNascimento, nomeAluno, turma, dAluno, curso FROM ESCOLA_SECUNDARIA.PESSOA JOIN"
                                  + "(SELECT nome as nomeAluno, turma, dataNascimento as dAluno, curso, encarregado FROM ESCOLA_SECUNDARIA.ALUNO JOIN ESCOLA_SECUNDARIA.PESSOA ON ALUNO.ncc = PESSOA.ncc WHERE idAluno = " + aluno.IdAluno + ")"
                                  + "AS T ON encarregado = ncc) AS T2 ON curso = codigo) AS T3 ON ESCOLA_SECUNDARIA.TURMA.codigo = turma) AS T4 ON professor = idProf)"
                                  + "AS T5 ON T5.ncc = PESSOA.ncc;";


                SqlDataReader RDR = CMD.ExecuteReader();
                if (RDR.Read())
                {
                    nomeAluno.Content = RDR["nomeAluno"].ToString();
                    dataAluno.Content = RDR["dAluno"].ToString().Split()[0];
                    encarregadoAluno.Content = RDR["nomeEncarregado"].ToString();
                    cursoAluno.Content = RDR["nomeCurso"].ToString();
                    turmaAluno.Content = RDR["ano"].ToString() + "º" + RDR["designação"].ToString();
                    diretorAluno.Content = RDR["nome"].ToString();
                }
                RDR.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            PlanoCurso p = new PlanoCurso(aluno, CN);
            this.NavigationService.Navigate(p);
        }

        private void notas_Click(object sender, RoutedEventArgs e)
        {
            Notas n = new Notas(aluno, CN);
            this.NavigationService.Navigate(n);
        }
    }
}
