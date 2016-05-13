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
            if (Justificacao.Text.Equals(""))
                MessageBox.Show("Insira uma justificação");
            else
                this.Close();
          //  janelaPai.loadData();
           
            /*   try
       {
           if (CN.State == ConnectionState.Closed) CN.Open();

           CMD = new SqlCommand();
           CMD.Connection = CN;
           CMD.CommandText = "SELECT  AULA.horario,AULA.numero,designação,HORARIO_DISCIPLINA.ano,data FROM (ESCOLA_SECUNDARIA.HORARIO_DISCIPLINA JOIN ESCOLA_SECUNDARIA.DISCIPLINA ON HORARIO_DISCIPLINA.disciplina=DISCIPLINA.codigo) JOIN ESCOLA_SECUNDARIA.AULA ON HORARIO_DISCIPLINA.id=AULA.horario JOIN (SELECT FALTAS_ALUNO.aluno,FALTAS_ALUNO.horario,FALTAS_ALUNO.aula FROM ESCOLA_SECUNDARIA.FALTAS_ALUNO LEFT OUTER JOIN  ESCOLA_SECUNDARIA.FALTAS_JUSTIFICADAS_ALUNO ON FALTAS_ALUNO.aula=FALTAS_JUSTIFICADAS_ALUNO.aula AND FALTAS_ALUNO.horario= FALTAS_JUSTIFICADAS_ALUNO.horario WHERE FALTAS_ALUNO.aluno=@idAluno AND FALTAS_JUSTIFICADAS_ALUNO.aluno IS NULL) AS T ON T.horario=AULA.horario AND T.aula=AULA.numero;";
           CMD.Parameters.AddWithValue("@idAluno", aluno.IdAluno);
           SqlDataReader RDR = CMD.ExecuteReader();
           while (RDR.Read())
           {
               listBox.Items.Add(new Aula(int.Parse(RDR["horario"].ToString()), int.Parse(RDR["numero"].ToString()), RDR["designação"].ToString(), int.Parse(RDR["ano"].ToString()), RDR["data"].ToString().Split()[0]));
           }
           RDR.Close();
       }
       catch (Exception ex)
       {

           MessageBox.Show(ex.Message);
       }*/
        }
    }
}
