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
    public partial class JustificarFalta : Window
    {
        private Aluno aluno;
        private Aula aulaFalta;

        private SqlConnection CN;
        private SqlCommand CMD;


        public JustificarFalta()
        {
            InitializeComponent();
        }

        // Construtor para a classe Prof_Home
        public JustificarFalta(Aluno al,Aula aulaFalta,SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            aluno = al;
            this.aulaFalta = aulaFalta;

            CN = cn;
            loadData();
        }

        private void loadData() {
            falta.Content = "Falta: "+aulaFalta.Disciplina+ " "+aulaFalta.Data;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (Justificacao.Text.Equals("") || Justificacao.Text.Length>=199)
                MessageBox.Show("Justificação inválida (tamanho maximo de carateres 200)");
            else {
                try
                {
                    if (CN.State == ConnectionState.Closed) CN.Open();

                    CMD = new SqlCommand();
                    CMD.Connection = CN;
                    CMD.CommandText = "INSERT INTO ESCOLA_SECUNDARIA.FALTAS_JUSTIFICADAS_ALUNO VALUES (@idAluno,@idhorario,@nAula,@descricao);";
                    CMD.Parameters.AddWithValue("@idAluno", aluno.IdAluno);
                    CMD.Parameters.AddWithValue("@idhorario", aulaFalta.Horario);
                    CMD.Parameters.AddWithValue("@nAula", aulaFalta.NumeroAula);
                    CMD.Parameters.AddWithValue("@descricao", Justificacao.Text);
                    int temp = CMD.ExecuteNonQuery();
                    if (temp != 0)
                        Console.WriteLine("Falta justificada com Sucesso!");
                    else
                        Console.WriteLine("Erro na justificação da falta");
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }

                this.Close();
            }
         
        }
    }
}

