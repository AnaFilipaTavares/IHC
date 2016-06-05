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
    public partial class CriarAula : Page
    {
        private Professor professor;
        private HorarioDisciplina hDisciplina;
        private SqlConnection CN;
        private SqlCommand CMD;
        private List<DiaSemana> diasDisciplina;

        public CriarAula()
        {
            InitializeComponent();
            data.SelectedDate = DateTime.Today;
           
        }

        // Construtor para a classe Prof_Home
        public CriarAula(Professor prof,HorarioDisciplina hoDisciplina,SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.professor = prof;
            this.hDisciplina = hoDisciplina;
            this.CN = cn;
            loadData();
        }

        private void horarioClick(object sender, RoutedEventArgs e)
        {
            if (warningExit())
                this.NavigationService.Navigate(new Horario(professor, CN));
        }

        private void Perfil_Click(object sender, RoutedEventArgs e)
        {
            if (warningExit())
                this.NavigationService.Navigate(new Prof_Home(professor, CN));
        }

        private void disciplinaClick(object sender, RoutedEventArgs e)
        {
            if (warningExit())
                this.NavigationService.Navigate(new MinhasDisciplinas(professor, CN));

        }

        private void DirecaoTurma_Click(object sender, RoutedEventArgs e)
        {
            if (warningExit())
                this.NavigationService.Navigate(new DirecaoTurma(professor, CN));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            if (warningExit())
                this.NavigationService.Navigate(new Login());
        }

        private void loadData() {

            // query para obter os alunos do inscritos
            label.Content = hDisciplina.Disciplina.Nome + " "+hDisciplina.Disciplina.AnoDisciplina +" - Criar Aula";
            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "EXEC PROJETO.p_perfilDisciplina @idHorario";
                CMD.Parameters.AddWithValue("@idHorario", hDisciplina.IdHorario);
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    ListaAluno.Items.Add(new CelulaAlunoFalta(new Aluno(int.Parse(RDR["aluno"].ToString()), RDR["nome"].ToString())));
                }
                RDR.Close();

                //obter a aula
                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "SELECT PROJETO.ultimaAula(@idHorario)";
                CMD.Parameters.AddWithValue("@idHorario", hDisciplina.IdHorario);
                int ret = (int)CMD.ExecuteScalar();
                nAula1.Text = (ret + 1)+"";

               

                

            }

            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                MessageBox.Show("Ocorreu um erro, repita a operação");
            }
            //obter dias da semana
            diasDisciplina = new List<DiaSemana>();
            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "Select * FROM PROJETO.getDayofWeeks(@idHorario)";
                CMD.Parameters.AddWithValue("@idHorario", hDisciplina.IdHorario);
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    diasDisciplina.Add(new DiaSemana(int.Parse(RDR["id"].ToString()),RDR["diaSemana"].ToString()));
                }
                RDR.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                MessageBox.Show("Ocorreu um erro, repita a operação");
            }

        }


        private void AulaSubstituicao_Click(object sender, RoutedEventArgs e)
        {
            if (warningExit())
                this.NavigationService.Navigate(new AulaSubstituicao(professor, CN));
        }

        private void Inicio_Click(object sender, RoutedEventArgs e)
        {
            if (warningExit())
                this.NavigationService.Navigate(new PerfilDisciplina(professor,hDisciplina, CN));
        }

        private void CriarAula_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Historico_Click(object sender, RoutedEventArgs e)
        {
            if (warningExit())
                this.NavigationService.Navigate(new HistoricoAulas(professor, hDisciplina, CN));
        }
        private void MarcarTeste_Click(object sender, RoutedEventArgs e)
        {
            if (warningExit())
                this.NavigationService.Navigate(new MarcarAValiacao(professor, hDisciplina, CN));
        }
        private void Voltar_Click(object sender, RoutedEventArgs e)
        {
            if (warningExit())
                this.NavigationService.Navigate(new MinhasDisciplinas(professor, CN));
        }

        private void aulaDuplaCheckBox_Click(object sender, RoutedEventArgs e)
        {
            updateAula2Int();
        }

        private void updateAula2Int()
        {
            if (aulaDuplaCheckBox.IsChecked == true)
            {
                nAula2.IsEnabled = true;
                label_e.IsEnabled = true;
                if (!nAula1.Text.Equals(""))
                {
                    nAula2.Text = (int.Parse(nAula1.Text) + 1) + "";
                }
            }
            else
            {
                nAula2.IsEnabled = false;
                label_e.IsEnabled = false;
            }
           
        }


        private void nAula1_TextChanged(object sender, TextChangedEventArgs e)
        {
            updateAula2Int();
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Despois de guardar só poderá alterar a informação desta aula no histórico de aulas.\n\nTem a certeza que pretende continuar?", "Guardar Aula", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                return;
            }

            //Verificar o matching do dia da semana

            DiaSemana dataAdicionar=null;
            foreach (DiaSemana d in diasDisciplina) {
                if (d.Dia != DayOfWeek.Sunday && d.Dia == data.SelectedDate.Value.DayOfWeek)
                {

                    dataAdicionar = d;
                }
            }

            if (dataAdicionar == null) {
                MessageBox.Show("Dia de semana inválido: " + data.SelectedDate.Value.DayOfWeek+"\nCorriga a data para um dia de semana em que a disciplina pode ser lecionada");
                return;
            }

            //criar aula
            int criarAula = 0;

            if (!nAula1.Text.Equals("") && sumarioBox.Text.Length <= 199)
            {
                try
                {
                    if (CN.State == ConnectionState.Closed) CN.Open();

                    CMD = new SqlCommand();
                    CMD.Connection = CN;
                    CMD.CommandText = "EXEC PROJETO.p_insertAula @idHorario,@id,@numeroAula,@sumario,@data";
                    CMD.Parameters.AddWithValue("@idHorario", hDisciplina.IdHorario);
                    CMD.Parameters.AddWithValue("@id", dataAdicionar.Id);
                    CMD.Parameters.AddWithValue("@numeroAula", nAula1.Text);
                    CMD.Parameters.AddWithValue("@sumario", sumarioBox.Text);
                    CMD.Parameters.AddWithValue("@data", data.SelectedDate.Value.ToString("yyyy-MM-dd"));
                    criarAula = CMD.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                if (aulaDuplaCheckBox.IsChecked == true)
                {
                    try
                    {
                        if (CN.State == ConnectionState.Closed) CN.Open();

                        CMD = new SqlCommand();
                        CMD.Connection = CN;
                        CMD.CommandText = "EXEC PROJETO.p_insertAula @idHorario,@id,@numeroAula,@sumario,@data";
                        CMD.Parameters.AddWithValue("@idHorario", hDisciplina.IdHorario);
                        CMD.Parameters.AddWithValue("@id", dataAdicionar.Id);
                        CMD.Parameters.AddWithValue("@numeroAula", nAula2.Text);
                        CMD.Parameters.AddWithValue("@sumario", sumarioBox.Text);
                        CMD.Parameters.AddWithValue("@data", data.SelectedDate.Value.ToString("yyyy-MM-dd"));
                        criarAula = CMD.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Parametros inválidos");
            }

            Console.WriteLine("valor criarAula: " + criarAula);
            if (criarAula != 0)
            {
                //MARCAR FALTAS se não existirem erros
               Console.WriteLine("Aula Criada");
               foreach (CelulaAlunoFalta elementos in ListaAluno.Items)
                {
                    if (elementos.getMarcarfalta() == true)
                    {
                        int erros = 0;
                         try
                         {
                             if (CN.State == ConnectionState.Closed) CN.Open();

                             CMD = new SqlCommand();
                             CMD.Connection = CN;
                             CMD.CommandText = "EXEC PROJETO.p_insertFaltas @idAluno,@idHorario,@id,@numeroAula,@tipo";
                             CMD.Parameters.AddWithValue("@idAluno", elementos.getNumero());
                             CMD.Parameters.AddWithValue("@idHorario", hDisciplina.IdHorario);
                             CMD.Parameters.AddWithValue("@id", dataAdicionar.Id);
                             CMD.Parameters.AddWithValue("@numeroAula", nAula1.Text);
                             CMD.Parameters.AddWithValue("@tipo", elementos.getTipofalta()[0]);
                             erros = CMD.ExecuteNonQuery();

                         }
                         catch (Exception ex)
                         {
                             MessageBox.Show(ex.Message);
                         }
                        Console.WriteLine(elementos + " "+elementos.getTipofalta());
                        if (erros != 0)
                        {
                            Console.WriteLine("Falta adicionada");
                        }
                        else {
                            Console.WriteLine("Erro na falta " + elementos);
                        }
                    }
                }

            }
            else {
                MessageBox.Show("Erro na criação da aula, não foram marcadas as faltas");
                Console.WriteLine("Erro na criação da aula");
                resetFaltas_Click(sender, e);
            }

            this.NavigationService.Navigate(new PerfilDisciplina(professor, hDisciplina, CN));

        }

        

        private void resetFaltas_Click(object sender, RoutedEventArgs e)
        {
            foreach (CelulaAlunoFalta elementos in ListaAluno.Items)
            {
                elementos.checkMarcarFalta.IsChecked = false;
                elementos.cbTipoFalta.SelectedIndex = 0;
            }
        }
        /*
            retorna true se for para sair
            
        */
        private bool warningExit()
        {
            if (MessageBox.Show("Se saires desta página todas as alterações seram perdidas.\n\nDesejas Continuar?", "Guardar Aula", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                return true;
            }
            return false;
        }
    }

    public class DiaSemana{
        private int id;
        private DayOfWeek dia=0;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public DayOfWeek Dia
        {
            get
            {
                return dia;
            }

            set
            {
                dia = value;
            }
        }

        public DiaSemana(int id, string data) {
            this.Id = id;
            parsingDia(data);
        }

        private void parsingDia(string data) {
            switch (data)
            {
                case "Segunda":
                    Dia = DayOfWeek.Monday;
                    break;
                case "Terça":
                    Dia = DayOfWeek.Tuesday;
                    break;
                case "Quarta":
                    Dia = DayOfWeek.Wednesday;
                    break;
                case "Quinta":
                    Dia = DayOfWeek.Thursday;
                    break;
                case "Sexta":
                    Dia = DayOfWeek.Friday;
                    break;
            }

        }
    }
    
}
