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
    public partial class Faltas : Page
    {

        private Aluno aluno;
        private SqlConnection CN;
        private SqlCommand CMD;


        public Faltas()
        {
            InitializeComponent();
        }

        public Faltas(Aluno aluno, SqlConnection cn) : this()
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
            List<Falta> items = new List<Falta>();
            try
            {
                if (CN.State == System.Data.ConnectionState.Closed) CN.Open();
                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "SELECT designação,count(*) as nFaltas FROM(SELECT disciplina, data, sumario, descricao, designação FROM ESCOLA_SECUNDARIA.DISCIPLINA JOIN(SELECT * FROM ESCOLA_SECUNDARIA.HORARIO_DISCIPLINA JOIN(SELECT T.horario, data, sumario, descricao FROM  ESCOLA_SECUNDARIA.AULA JOIN(SELECT FALTAS_ALUNO.aluno, FALTAS_ALUNO.horario, FALTAS_ALUNO.aula, descricao, tipo FROM ESCOLA_SECUNDARIA.FALTAS_ALUNO LEFT OUTER JOIN ESCOLA_SECUNDARIA.FALTAS_JUSTIFICADAS_ALUNO ON FALTAS_JUSTIFICADAS_ALUNO.aluno = FALTAS_ALUNO.aluno AND FALTAS_JUSTIFICADAS_ALUNO.horario = FALTAS_ALUNO.horario AND FALTAS_JUSTIFICADAS_ALUNO.aula = FALTAS_ALUNO.aula WHERE FALTAS_ALUNO.aluno = " + aluno.IdAluno + ") AS T ON aula = numero AND T.horario = AULA.horario) AS T2 ON horario = id) AS T3 ON disciplina = codigo) AS T4 GROUP BY designação;";
                SqlDataReader RDR = CMD.ExecuteReader();

                while (RDR.Read())
                {
                    items.Add(new Falta() { disciplina = RDR["designação"].ToString(), numero = (int)RDR["nFaltas"], datas = new List<string>()});
                }
                RDR.Close();
                CMD.CommandText = "SELECT designação,data,sumario,descricao,tipo FROM ESCOLA_SECUNDARIA.DISCIPLINA JOIN (SELECT * FROM ESCOLA_SECUNDARIA.HORARIO_DISCIPLINA JOIN(SELECT T.horario,data,sumario,descricao,T.tipo FROM  ESCOLA_SECUNDARIA.AULA JOIN (SELECT FALTAS_ALUNO.aluno,FALTAS_ALUNO.horario,FALTAS_ALUNO.aula,descricao,FALTAS_ALUNO.tipo FROM ESCOLA_SECUNDARIA.FALTAS_ALUNO LEFT OUTER JOIN ESCOLA_SECUNDARIA.FALTAS_JUSTIFICADAS_ALUNO ON FALTAS_JUSTIFICADAS_ALUNO.aluno = FALTAS_ALUNO.aluno AND FALTAS_JUSTIFICADAS_ALUNO.horario = FALTAS_ALUNO.horario AND FALTAS_JUSTIFICADAS_ALUNO.aula = FALTAS_ALUNO.aula WHERE FALTAS_ALUNO.aluno = " + aluno.IdAluno + ") AS T ON aula = numero AND T.horario = AULA.horario) AS T2 ON horario = id) AS T3 ON disciplina=codigo;";
                SqlDataReader RDR2 = CMD.ExecuteReader();

                while (RDR2.Read())
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].disciplina.Equals(RDR2["designação"].ToString()))
                        {
                            items[i].datas.Add(RDR2["data"].ToString().Split(' ')[0]);
                        }
                    }
                }

                listView.ItemsSource = items;
                RDR2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ChangeDate(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            string data = comboBox.SelectedItem as string;
            FaltasAluno f = new FaltasAluno(data,CMD.CommandText);
        }
    }
}

public class Falta
{
    public string disciplina { get; set; }

    public int numero { get; set; }

    public List<String> datas { get; set; }
     
}
