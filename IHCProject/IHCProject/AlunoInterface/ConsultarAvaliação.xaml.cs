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
using System.Data;
using IHCProject.infoClass;

namespace IHCProject.AlunoInterface
{
    /// <summary>
    /// Interaction logic for MinhasDisciplinas.xaml
    /// </summary>
    public partial class ConsultarAvaliação : Page
    {
        private Aluno aluno;
        private SqlConnection CN;
        private SqlCommand CMD;

        public ConsultarAvaliação()
        {
            InitializeComponent();

            cboMonth.ItemsSource = new List<string> { "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro" };

            cboYear.ItemsSource = new List<int> { DateTime.Today.Year, DateTime.Today.Year - 1 };
            /*cboYear.SelectedItem = DateTime.Today.Year;
            cboMonth.SelectedIndex = DateTime.Today.Month-1;
            Console.WriteLine(cboMonth.SelectedIndex +" " + (DateTime.Today.Month - 1));
            MessageBox.Show(cboYear.SelectedItem.ToString());
            Console.WriteLine(cboYear.SelectedItem.ToString());*/

        }
        // Construtor para a classe Prof_Home
        public ConsultarAvaliação(Aluno aluno,SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.aluno = aluno;
            //this.hDisciplina = hoDisciplina
            this.CN = cn;

            loadData();
        }


        private void drawCells(int ano, int mes)
        {

            ISet<Marcacao> todasMarcacoes = queryMarcacoes(mes);

            int startDayofWeek = int.Parse(new DateTime(ano, mes, 1).DayOfWeek.ToString("D"));
            int diasDomes = DateTime.DaysInMonth(ano, mes);
            bool exist = false;
            int row = 0;
            int colum = (startDayofWeek - 1);

            List<string> temp = new List<string>();
            for (int i = 0; i < diasDomes; i++)
            {

                if (colum >= 0 && colum <= 4)
                {
                    CelulaCalendarioAluno cell = new CelulaCalendarioAluno(i + 1, row, colum, ano, mes,CN);

                    temp.Clear();
                    foreach (Marcacao e in todasMarcacoes)
                    {
                        if ((i + 1) == e.data.Day)
                        {
                            // Console.WriteLine("adicionei "+e.disciplina+" dia "+e.data.Day);
                            temp.Add(e.disciplina);
                        }
                    }
                    cell.setAvaliacoes(temp);

                    Container.Children.Add(cell);
                    exist = true;
                }

                if (colum == 5)
                {
                    if (exist)
                        row++;
                    colum = -2;
                }

                colum++;

            }

        }
        private ISet<Marcacao> queryMarcacoes(int mes)
        {
            ISet<Marcacao> setReturn = new HashSet<Marcacao>();

            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                String format = "____-" + String.Format("{0:00}", mes) + "-__";
                CMD.CommandText = "EXEC ESCOLA_SECUNDARIA.SP_AvaliaçõesAluno @aluno,@format;";
                CMD.Parameters.AddWithValue("@aluno", aluno.IdAluno);
                CMD.Parameters.AddWithValue("@format", format);
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    setReturn.Add(new Marcacao(RDR["designação"].ToString() + " " + RDR["tipoAvaliacao"].ToString()[0], Convert.ToDateTime(RDR["data"].ToString())));
                }
                RDR.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }



            return setReturn;
        }

        private void loadData()
        {
            //simula
            Calendario.Background = Brushes.LightSeaGreen;
            cboYear.SelectedIndex = 0;

            cboMonth.SelectedIndex = (DateTime.Today.Month - 1);
            drawCells(int.Parse(cboYear.SelectedItem.ToString()), cboMonth.SelectedIndex + 1);

        }
        private void cboMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Container.Children.Clear();

            if (cboMonth.SelectedIndex != -1)
                drawCells(int.Parse(cboYear.SelectedItem.ToString()), cboMonth.SelectedIndex + 1);
        }

        private void cboYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Container.Children.Clear();

            if (cboMonth.SelectedIndex != -1)
                drawCells(int.Parse(cboYear.SelectedItem.ToString()), cboMonth.SelectedIndex + 1);
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

        //class para guardar dados
        private class Marcacao
        {

            public string disciplina;
            public DateTime data;

            public Marcacao(string dis, DateTime date)
            {
                disciplina = dis;
                data = date;
            }

            public override bool Equals(object obj)
            {
                return disciplina.Equals(((Marcacao)obj).disciplina) && data.Equals(((Marcacao)obj).data);
            }

        }
    }

}
