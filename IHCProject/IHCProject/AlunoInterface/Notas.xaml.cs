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
    /// Interaction logic for Notas.xaml
    /// </summary>
    public partial class Notas : Page
    {

        private Aluno aluno;
        private SqlConnection CN;
        private SqlCommand CMD;


        public Notas()
        {
            InitializeComponent();
        }

        public Notas(Aluno aluno, SqlConnection cn) : this()
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
            List<Nota> items = new List<Nota>();
            try
            {
                if (CN.State == System.Data.ConnectionState.Closed) CN.Open();
                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "SELECT designação,ano,nota FROM ESCOLA_SECUNDARIA.NOTAS JOIN ESCOLA_SECUNDARIA.DISCIPLINA ON codigo = disciplina WHERE aluno = " + aluno.IdAluno + ";";
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    items.Add(new Nota() { disciplina = RDR["designação"].ToString() + " " + RDR["ano"].ToString(), nota = (int)RDR["nota"]});
                }

                listView.ItemsSource = items;
                RDR.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}

public class Nota
{
    public string disciplina { get; set; }

    public int nota { get; set; }
}
