using System.Data.SqlClient;
using IHCProject.infoClass;

using System;
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
namespace IHCProject
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class InfoALuno : Window
    {
        private Aluno aluno;
        private SqlConnection CN;
        private SqlCommand CMD;
        public InfoALuno()
        {
            InitializeComponent();
        }

        // Construtor para a classe Prof_Home
        public InfoALuno(Aluno al, SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            aluno = al;
            CN = cn;
            loadData();
        }

        private void loadData() {
            nomeAluno.Content = "Nome: "+aluno.Nome;
            numeroAluno.Content = "Número: "+aluno.IdAluno;
            Console.WriteLine("FIZ O LOAD");
            //query para ler as faltas injustificadas
            listBox.Items.Clear();
            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "EXEC PROJETO.p_faltasInjustificadasAluno @idAluno;";
                CMD.Parameters.AddWithValue("@idAluno", aluno.IdAluno);
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                     listBox.Items.Add(new Aula(int.Parse(RDR["disciplinaInfo"].ToString()), int.Parse(RDR["numeroAula"].ToString()), int.Parse(RDR["horario"].ToString()),RDR["disciplina"].ToString(), int.Parse(RDR["ano"].ToString()), RDR["data"].ToString().Split()[0], RDR["sumario"].ToString()));
                }
                RDR.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void Falta_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            openPopupWindow((Aula)listBox.SelectedValue);
            this.loadData();
        }

        private void openPopupWindow(Aula selected) {
            JustificarFalta popupWind = new JustificarFalta(aluno,selected, CN);
            popupWind.Top = (SystemParameters.FullPrimaryScreenHeight - popupWind.Height) / 2;
            popupWind.Left = (SystemParameters.FullPrimaryScreenWidth - popupWind.Width) / 2;
            popupWind.ShowDialog();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Aula temp = (Aula)listBox.SelectedItem;
            if (temp == null)
            {
                MessageBox.Show("Selecione uma opção");
            }
            else {
                openPopupWindow(temp);
                this.loadData();
            }
        }
    }
}
