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
using System.Windows.Shapes;


using System.Data;
using IHCProject.infoClass;

namespace IHCProject.ContextoDisciplina
{
    /// <summary>
    /// Interaction logic for MinhasDisciplinas.xaml
    /// </summary>
    public partial class MarcarAvaliacaoPop : Window
    {

        private HorarioDisciplina hDisciplina;
        private DateTime date;
        private SqlConnection CN;
        private SqlCommand CMD;
        //private MarcarAValiacao referenciaPai;

        public MarcarAvaliacaoPop()
        {
            InitializeComponent();
        }

        // Construtor para a classe Prof_Home
        public MarcarAvaliacaoPop(HorarioDisciplina hDisciplina,DateTime date,SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            
            this.hDisciplina = hDisciplina;
            this.CN = cn;
            this.date = date;
            loadData();
        }

      
        private void loadData() {
            disciplina.Content = "Disciplina: " + hDisciplina.Disciplina + " "+hDisciplina.TurmaDisciplina;
            data.Content = "Data: " + date.ToString("dd-MM-yyyy");


        }
        
        private void marcar_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Tem a certeza que pretende marcar uma avaliação de "+((ComboBoxItem)comboBox.SelectedValue).Content+ " para o dia "+ date.ToString("dd-MM-yyyy") + " \n", "Marcar avaliação", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                insertAvaliacao(hDisciplina,date);

                this.Close();
            }
            else {
                Console.WriteLine("Não marcou avaliação");
            }
        }

        private void insertAvaliacao(HorarioDisciplina hd,DateTime dT) {
            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();
                int inseriu = 0;
                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "INSERT INTO ESCOLA_SECUNDARIA.MARCA_AVALIAÇÕES VALUES (@idHorario,@date,@tipo);";
                CMD.Parameters.AddWithValue("@idHorario", hDisciplina.IdHorario);
                CMD.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
                CMD.Parameters.AddWithValue("@tipo", ((ComboBoxItem)comboBox.SelectedValue).Content.ToString().Split()[0]);

                inseriu = CMD.ExecuteNonQuery();

                Console.WriteLine("Número de linhas inseridas: "+inseriu);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: Já existe uma avaliação desta disciplina para esse dia");
                Console.WriteLine(ex.Message);
            }
        }
    }
    
}
