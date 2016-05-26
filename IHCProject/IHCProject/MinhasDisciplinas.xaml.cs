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
using IHCProject.ContextoDisciplina;
using System.Data;

namespace IHCProject
{
    /// <summary>
    /// Interaction logic for MinhasDisciplinas.xaml
    /// </summary>
    public partial class MinhasDisciplinas : Page
    {
        private Professor prof;
        private SqlConnection CN;
        private SqlCommand CMD;
        public MinhasDisciplinas()
        {
            InitializeComponent();
        }

        // Construtor para a classe Prof_Home
        public MinhasDisciplinas(Professor prof, SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.prof = prof;
            this.CN = cn;
            loadData();
        }

        private void loadData()
        {
            
            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();
               
                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "SELECT id, T.designação,T.ano,disciplina,TURMA.designação AS letra FROM ESCOLA_SECUNDARIA.TURMA JOIN (SELECT id, designação, ano, disciplina, turma FROM ESCOLA_SECUNDARIA.HORARIO_DISCIPLINA JOIN ESCOLA_SECUNDARIA.DISCIPLINA ON HORARIO_DISCIPLINA.disciplina= DISCIPLINA.codigo WHERE professor = @idProf) AS T ON T.turma = TURMA.codigo;";
                CMD.Parameters.AddWithValue("@idProf", prof.IdProf);
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    listBox.Items.Add(new HorarioDisciplina(int.Parse(RDR["id"].ToString()),new Disciplina(int.Parse(RDR["disciplina"].ToString()), int.Parse(RDR["ano"].ToString()), RDR["designação"].ToString()), RDR["letra"].ToString(), prof));
                }
                RDR.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void horarioClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Horario(prof, CN));
        }

        private void disciplinaClick(object sender, RoutedEventArgs e)
        {

        }


        private void Perfil_Click(object sender, RoutedEventArgs e)
        {
            Prof_Home p = new Prof_Home(prof, CN);
            this.NavigationService.Navigate(p);
        }

        private void DirecaoTurma_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DirecaoTurma(prof, CN));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }

        private void aceder_Click(object sender, RoutedEventArgs e)
        {
            HorarioDisciplina temp = (HorarioDisciplina)listBox.SelectedItem;
            if (temp == null)
            {
                MessageBox.Show("Selecione uma opção");
            }
            else {
                this.NavigationService.Navigate(new PerfilDisciplina(prof, temp, CN));
            }
        }

        private void AulaSubstituicao_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AulaSubstituicao(prof, CN));
        }

        private void listbox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.NavigationService.Navigate(new PerfilDisciplina(prof, (HorarioDisciplina)listBox.SelectedItem, CN));
        }


    }
}

