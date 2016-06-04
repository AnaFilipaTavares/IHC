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
using IHCProject.ContextoDisciplina;

namespace IHCProject
{
    /// <summary>
    /// Interaction logic for MinhasDisciplinas.xaml
    /// </summary>
    public partial class AulaSubstituicao : Page
    {
        private Professor prof;
        private SqlConnection CN;
        private SqlCommand CMD;

        public AulaSubstituicao()
        {
            InitializeComponent();
        }

        // Construtor para a classe Prof_Home
        public AulaSubstituicao(Professor prof, SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.prof = prof;
            this.CN = cn;
            loadData();
        }

        private void horarioClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Horario(prof, CN));
        }

        private void Perfil_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Prof_Home(prof, CN));
        }

        private void disciplinaClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MinhasDisciplinas(prof, CN));

        }
        private void AulaSubstituicao_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DirecaoTurma_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DirecaoTurma(prof, CN));
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }

        private void loadData() {
          
            
           try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "EXEC PROJETO.p_listaSubstProf @idProf @disciplina @professor";
                CMD.Parameters.AddWithValue("@idProf", prof.IdProf);
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    listBoxDisciplinas.Items.Add(new HorarioDisciplina(int.Parse(RDR["id"].ToString()),new Disciplina(int.Parse(RDR["codigo"].ToString()), int.Parse(RDR["ano"].ToString()), RDR["nomeDisciplina"].ToString()), RDR["designação"].ToString(),new Professor(int.Parse(RDR["idProf"].ToString()), RDR["nome"].ToString())));
                    
                }
                RDR.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void listBoxDisciplinas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((HorarioDisciplina)listBoxDisciplinas.SelectedItem != null)
                if (MessageBox.Show("Tem a certeza que pretende criar uma aula de substituição\n" + ((HorarioDisciplina)listBoxDisciplinas.SelectedValue).ToString(), "criar aula", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Console.WriteLine("cria aula substituicao");
                    this.NavigationService.Navigate(new CriarAulaSubstituicao(prof,(HorarioDisciplina)listBoxDisciplinas.SelectedItem, CN));
                }
                else {
                    Console.WriteLine("Não cria aula substituicao");
                }

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            HorarioDisciplina temp = (HorarioDisciplina)listBoxDisciplinas.SelectedItem;
            if (temp == null)
            {
                MessageBox.Show("Selecione uma opção");
            }
            else {
                if (MessageBox.Show("Tem a certeza que pretende criar uma aula de substituição\n" + ((HorarioDisciplina)listBoxDisciplinas.SelectedValue).ToString(), "criar aula", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Console.WriteLine("cria aula substituicao");
                    this.NavigationService.Navigate(new CriarAulaSubstituicao(prof, (HorarioDisciplina)listBoxDisciplinas.SelectedItem, CN));
                }
                else {
                    Console.WriteLine("Não cria aula substituicao");
                }
            }
            
        }

        private void openPopupWindow(HorarioDisciplina selected) {
           /* InfoALuno popupWind = new InfoALuno((selected), CN);
            popupWind.Top = (SystemParameters.FullPrimaryScreenHeight - popupWind.Height) / 2;
            popupWind.Left = (SystemParameters.FullPrimaryScreenWidth - popupWind.Width) / 2;
            popupWind.ShowDialog();*/
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            listBoxDisciplinas.Items.Clear();

            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "EXEC PROJETO.p_listaSubstProf @idProf @disciplina @professor";

                if (!disciplina_search.Text.Equals("")) { 
                    CMD.CommandText = "EXEC PROJETO.p_listaSubstProf @idProf @disciplina";
                    CMD.Parameters.AddWithValue("@disciplina", disciplina_search.Text);
                }
                if (!professor_search.Text.Equals(""))
                {
                    CMD.CommandText = "EXEC PROJETO.p_listaSubstProf @idProf @disciplina";
                    CMD.Parameters.AddWithValue("@professor", professor_search.Text);
                }

                CMD.Parameters.AddWithValue("@idProf", prof.IdProf);
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    listBoxDisciplinas.Items.Add(new HorarioDisciplina(int.Parse(RDR["id"].ToString()), new Disciplina(int.Parse(RDR["codigo"].ToString()), int.Parse(RDR["ano"].ToString()), RDR["nomeDisciplina"].ToString()), RDR["designação"].ToString(), new Professor(int.Parse(RDR["idProf"].ToString()), RDR["nome"].ToString())));
                    //TurmaDoDT.Items.Add(new Aluno(int.Parse(RDR["idAluno"].ToString()), RDR["nome"].ToString()));
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
