using IHCProject.infoClass;
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

namespace IHCProject.AlunoInterface
{
    /// <summary>
    /// Interaction logic for PlanoCurso.xaml
    /// </summary>
    public partial class PlanoCurso : Page
    {

        private Aluno aluno;
        private SqlConnection CN;
        private SqlCommand CMD;

        public PlanoCurso()
        {
            InitializeComponent();
        }

        public PlanoCurso(Aluno aluno, SqlConnection cn) : this()
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
            MessageBox.Show("horario");
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
            try
            {
                if (CN.State == System.Data.ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "SELECT designação,ano FROM ESCOLA_SECUNDARIA.DISCIPLINA JOIN (SELECT disciplina, ano, idAluno FROM(SELECT * FROM ESCOLA_SECUNDARIA.ALUNO  WHERE idAluno = 5) AS T JOIN ESCOLA_SECUNDARIA.COMPOSIÇÃO_CURSO ON T.curso = COMPOSIÇÃO_CURSO.curso) AS T2 ON disciplina = codigo ORDER BY 2,1;";
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    textBlock.Text += RDR["designação"].ToString() + " " + RDR["ano"].ToString() + "\n";
                
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
            Notas n = new Notas(aluno, CN);
            this.NavigationService.Navigate(n);
        }
    }



}

