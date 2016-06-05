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

namespace IHCProject.Secretaria
{
    /// <summary>
    /// Interaction logic for MinhasDisciplinas.xaml
    /// </summary>
    public partial class HorarioPreview : Window
    {
        private Turma turma;
        private SqlConnection CN;
        private SqlCommand CMD;

        public HorarioPreview()
        {
            InitializeComponent();
            
        }

        // Construtor para a classe Prof_Home
        public HorarioPreview(Turma turma, SqlConnection CN) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.turma = turma;
            this.CN = CN;
            loadData();
        }

      
        
        private void loadData() {

          

            label.Content = "Horario Preview" + turma.Ano + " "+ turma.Letra;

            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "EXEC PROJETO.p_horarioTurma @idTurma;";
                CMD.Parameters.AddWithValue("@idTurma", turma.IdTurma);
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {

                    Container.Children.Add(new DisplayDisciplinaHorario(new DisciplinaHorario(RDR["sala"].ToString(), RDR["diaSemana"].ToString(), RDR["nome"].ToString(), RDR["horaInicio"].ToString().Split()[0])));
                    //listBox.Items.Add(new HorarioDisciplina(int.Parse(RDR["id"].ToString()), new Disciplina(int.Parse(RDR["disciplina"].ToString()), int.Parse(RDR["ano"].ToString()), RDR["designação"].ToString()), RDR["letra"].ToString(), prof));
                    if (RDR["duração"].ToString().Equals("100"))
                    {
                        DisciplinaHorario di = new DisciplinaHorario(RDR["sala"].ToString(), RDR["diaSemana"].ToString(), RDR["nome"].ToString(), RDR["horaInicio"].ToString().Split()[0]);
                        di.RowIndex++;
                        Container.Children.Add(new DisplayDisciplinaHorario(di));
                    }
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
