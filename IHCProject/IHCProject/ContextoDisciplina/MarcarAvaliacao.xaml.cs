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

namespace IHCProject.ContextoDisciplina
{
    /// <summary>
    /// Interaction logic for MinhasDisciplinas.xaml
    /// </summary>
    public partial class MarcarAValiacao : Page
    {
        private Professor professor;
        private HorarioDisciplina hDisciplina;
        private SqlConnection CN;
        private SqlCommand CMD;

        public Professor Professor
        {
            get
            {
                return professor;
            }

            set
            {
                professor = value;
            }
        }

        public HorarioDisciplina HDisciplina
        {
            get
            {
                return hDisciplina;
            }

            set
            {
                hDisciplina = value;
            }
        }

        public SqlConnection CN1
        {
            get
            {
                return CN;
            }

            set
            {
                CN = value;
            }
        }

        public MarcarAValiacao()
        {
            InitializeComponent();

            cboMonth.ItemsSource = new List<string> { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            cboYear.ItemsSource = new List<int> { DateTime.Today.Year, DateTime.Today.Year-1  };
            /*cboYear.SelectedItem = DateTime.Today.Year;
            cboMonth.SelectedIndex = DateTime.Today.Month-1;
            Console.WriteLine(cboMonth.SelectedIndex +" " + (DateTime.Today.Month - 1));
            MessageBox.Show(cboYear.SelectedItem.ToString());
            Console.WriteLine(cboYear.SelectedItem.ToString());*/

        }

        // Construtor para a classe Prof_Home
        public MarcarAValiacao(Professor prof,HorarioDisciplina hoDisciplina,SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.professor = prof;
            this.hDisciplina = hoDisciplina;
            this.CN = cn;
            
            loadData();
        }
       
        
       private void drawCells(int ano,int mes) {

            ISet<Marcacao> todasMarcacoes = queryMarcacoes();

            int startDayofWeek = int.Parse(new DateTime(ano, mes, 1).DayOfWeek.ToString("D"));
            int diasDomes= DateTime.DaysInMonth(ano,mes);
            bool exist = false;
            int row = 0;
            int colum = (startDayofWeek - 1);

            List<string> temp = new List<string>();
            for (int i = 0; i < diasDomes; i++)
            {

                if (colum >= 0 && colum <= 4) {
                    CelulaCalendario cell = new CelulaCalendario(i + 1, row, colum, ano, mes, hDisciplina, CN);

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

                if (colum == 5) {
                    if (exist)
                        row++;
                    colum = -2;
                }

                colum++;
               
            }

        }

        private ISet<Marcacao> queryMarcacoes()
        {
            ISet<Marcacao> setReturn = new HashSet<Marcacao>();

            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "SELECT designação,tipoAvaliacao,data FROM ESCOLA_SECUNDARIA.DISCIPLINA JOIN (ESCOLA_SECUNDARIA.HORARIO_DISCIPLINA JOIN (ESCOLA_SECUNDARIA.MARCA_AVALIAÇÕES JOIN (select horario from ESCOLA_SECUNDARIA.FREQUENTA where aluno IN( SELECT aluno FROM ESCOLA_SECUNDARIA.UDF_ListaAlunosDisciplina (@idHorario)) group by horario) AS T ON T.horario=MARCA_AVALIAÇÕES.horario) ON MARCA_AVALIAÇÕES.horario=id) ON disciplina = codigo";
                CMD.Parameters.AddWithValue("@idHorario", hDisciplina.IdHorario);
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    setReturn.Add(new Marcacao(RDR["designação"].ToString()+" "+ RDR["tipoAvaliacao"].ToString()[0], Convert.ToDateTime(RDR["data"].ToString())));
                }
                RDR.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        

            return setReturn;
        }

        private void horarioClick(object sender, RoutedEventArgs e)
        {
            
                this.NavigationService.Navigate(new Horario(professor, CN));
        }

        private void Perfil_Click(object sender, RoutedEventArgs e)
        {
            
                this.NavigationService.Navigate(new Prof_Home(professor, CN));
        }

        private void disciplinaClick(object sender, RoutedEventArgs e)
        {
            
                this.NavigationService.Navigate(new MinhasDisciplinas(professor, CN));

        }

        private void DirecaoTurma_Click(object sender, RoutedEventArgs e)
        {
            
                this.NavigationService.Navigate(new DirecaoTurma(professor, CN));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            
                this.NavigationService.Navigate(new Login());
        }

        private void loadData() {

            // query para obter os alunos do inscritos
            label.Content = hDisciplina.Disciplina.Nome + " "+hDisciplina.Disciplina.AnoDisciplina +" - Marcar Avaliação";

            //simula
            cboYear.SelectedIndex = 0;

            cboMonth.SelectedIndex = (DateTime.Today.Month - 1);
            drawCells(int.Parse(cboYear.SelectedItem.ToString()), cboMonth.SelectedIndex+1);

        }

        private void AulaSubstituicao_Click(object sender, RoutedEventArgs e)
        {
            
                this.NavigationService.Navigate(new AulaSubstituicao(professor, CN));
        }

        private void Inicio_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new PerfilDisciplina(professor,hDisciplina, CN));
        }

        private void CriarAula_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CriarAula(professor, hDisciplina, CN));
        }

        private void Historico_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HistoricoAulas(professor, hDisciplina, CN));
        }
        private void MarcarTeste_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            
                this.NavigationService.Navigate(new MinhasDisciplinas(professor, CN));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

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
            
            if (cboMonth.SelectedIndex!=-1)
                drawCells(int.Parse(cboYear.SelectedItem.ToString()), cboMonth.SelectedIndex + 1);
        }

        //class para guardar dados
        private class Marcacao {

            public string disciplina;
            public DateTime data;

            public Marcacao(string dis, DateTime date) {
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
