using IHCProject.infoClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace IHCProject.Secretaria
{
    /// <summary>
    /// Interaction logic for InsertProf.xaml
    /// </summary>
    public partial class GerarHorario : Page
    {
        private SqlConnection CN;
        private SqlCommand CMD;
        private List<Sala> listaSalas ;
        private List<PermutacoesHorario> posFreeHorario;
        public GerarHorario()
        {
            InitializeComponent();
            listaSalas = new List<Sala>();
        }

        public GerarHorario(SqlConnection cN): this()
        {
            this.CN = cN;
            loadData();
        }

        private void loadData()
        {

            
            //criar listaposlivresNohorario

            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "EXEC PROJETO.p_turmaSemHorario";
                SqlDataReader RDR = CMD.ExecuteReader();
                List<Turma> temp = new List<Turma>();
                while (RDR.Read())
                {
                    temp.Add(new Turma(int.Parse(RDR["codigo"].ToString()), RDR["ano"].ToString(), RDR["designação"].ToString()));

                }
                cblistTurma.ItemsSource = temp;
                RDR.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            cblistTurma.SelectedIndex = 0;
           

        }
        private void loadDisciplina()
        {

            List<string> diasSemana = new List<string>();
            diasSemana.Add("Segunda");
            diasSemana.Add("Terça");
            diasSemana.Add("Quarta");
            diasSemana.Add("Quinta");
            diasSemana.Add("Sexta");
            List<string> horasInico = new List<string>();
            horasInico.Add("09:00:00");
            horasInico.Add("11:00:00");
            horasInico.Add("14:40:00");
            posFreeHorario = new List<PermutacoesHorario>();
            foreach (string d in diasSemana)
                foreach (string h in horasInico)
                    posFreeHorario.Add(new PermutacoesHorario(d, h));


            if (cblistTurma.SelectedValue != null)
            {
                // carrega as salas existentes
                listaSalas.Clear();
                try
                {
                    if (CN.State == ConnectionState.Closed) CN.Open();

                    CMD = new SqlCommand();
                    CMD.Connection = CN;
                    CMD.CommandText = "EXEC PROJETO.p_Salas";
                    SqlDataReader RDR = CMD.ExecuteReader();
                   
                    while (RDR.Read())
                    {
                        listaSalas.Add(new Sala(int.Parse(RDR["id"].ToString()), RDR["designação"].ToString()));
                    }
                    RDR.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                // carregar as disciplina que precisas de ter horario acociado
                try
                {
                    if (CN.State == ConnectionState.Closed) CN.Open();

                    CMD = new SqlCommand();
                    CMD.Connection = CN;
                    CMD.CommandText = "EXEC PROJETO.p_disciplinasByTurma @turma";
                    CMD.Parameters.AddWithValue("@turma", ((Turma)cblistTurma.SelectedValue).IdTurma);
                    SqlDataReader RDR = CMD.ExecuteReader();
                    List<DisciplinaDisplayHorario> temp = new List<DisciplinaDisplayHorario>();
                    while (RDR.Read())
                    {

                        temp.Add(new DisciplinaDisplayHorario(int.Parse(RDR["id"].ToString()), RDR["nome"].ToString(), listaSalas));

                    }
                    listBoxDisciplinas.ItemsSource = temp;
                    RDR.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Gest_DisciplinaClick(object sender, RoutedEventArgs e)
        {
           
        }


        private void Gest_horarioClick(object sender, RoutedEventArgs e)
        {
            GerarHorario a = new GerarHorario(CN);
            this.NavigationService.Navigate(a);
        }

        private void Gest_turmaClick(object sender, RoutedEventArgs e)
        {
            GerarTurmas a = new GerarTurmas(CN);
            this.NavigationService.Navigate(a);
        }

        private void Perfil_Click(object sender, RoutedEventArgs e)
        {
            Inicio x = new Inicio(CN);
            this.NavigationService.Navigate(x);
        }
        private void Insert_Click(object sender, RoutedEventArgs e)
        {
            Inserções x = new Inserções(CN);
            this.NavigationService.Navigate(x);
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (cblistTurma.SelectedValue == null)
                return;
            int maximo=0;
            foreach (DisciplinaDisplayHorario dis in listBoxDisciplinas.Items)
            {
                int cargaHorario = int.Parse(((ComboBoxItem)dis.cbMin.SelectedValue).Content.ToString());
                for (int i = 0; i < cargaHorario; i += 100)
                {
                    maximo += 100;
                }
            }
            if (maximo > 1500) {
                MessageBox.Show("Criação automática de horarios só é suportada para uma carga de até 1500 minutos\nHorario não gerado");
                return;
            }
            foreach (DisciplinaDisplayHorario dis in listBoxDisciplinas.Items)
            {
                int cargaHorario = int.Parse(((ComboBoxItem)dis.cbMin.SelectedValue).Content.ToString());
                for (int i = 0; i< cargaHorario; i+=100) { 
                    Random rnd = new Random();
                    int select = rnd.Next(posFreeHorario.Count);
                    PermutacoesHorario temp = posFreeHorario.ElementAt(select);
                    posFreeHorario.RemoveAt(select);
                    try
                    {
                        CMD = new SqlCommand();
                        CMD.Connection = CN;
                        CMD.CommandText = "SELECT PROJETO.ultimoIdHorario_Da_Disciplina(@idDis)";
                        CMD.Parameters.AddWithValue("@idDis", dis.Id);
                        int ret = (int)CMD.ExecuteScalar();

                        CMD.CommandText = "EXEC PROJETO.p_insertHorario_DA_DISCIPLINA @disciplinaInfo,@id,@duracao,@horaIncio,@diaSemana,@sala";
                        CMD.Parameters.AddWithValue("@disciplinaInfo", dis.Id);
                        CMD.Parameters.AddWithValue("@id", ret + 1);
                        CMD.Parameters.AddWithValue("@duracao", 100);
                        CMD.Parameters.AddWithValue("@horaIncio", temp.HoraInicio);
                        CMD.Parameters.AddWithValue("@diaSemana", temp.DiaSemana);
                        CMD.Parameters.AddWithValue("@sala", ((Sala)dis.cbSala.SelectedValue).Id);
                        int status = CMD.ExecuteNonQuery();
                        Console.WriteLine(status+" " +dis.Id + " " + temp.DiaSemana + " " + temp.HoraInicio);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                }
            }
            openPopupWindow(((Turma)cblistTurma.SelectedValue));
            loadData();
        }


        private void openPopupWindow(Turma selected)
        {
            HorarioPreview popupWind = new HorarioPreview( selected, CN);
            popupWind.Top = (SystemParameters.FullPrimaryScreenHeight - popupWind.Height) / 2;
            popupWind.Left = (SystemParameters.FullPrimaryScreenWidth - popupWind.Width) / 2;
            popupWind.ShowDialog();
        }

        public class PermutacoesHorario {
            private string diaSemana;
            private string horaInicio;
            public PermutacoesHorario(string diaSemana,string horaInicio) {
                this.diaSemana = diaSemana;
                this.horaInicio = horaInicio;
            }

            public string DiaSemana
            {
                get
                {
                    return diaSemana;
                }

                set
                {
                    diaSemana = value;
                }
            }

            public string HoraInicio
            {
                get
                {
                    return horaInicio;
                }

                set
                {
                    horaInicio = value;
                }
            }
        }
        public class Sala
        {
            private int id;
            private string nome;

            public Sala(int id, string nome) {
                this.id = id;
                this.nome = nome;
            }

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

            public string Nome
            {
                get
                {
                    return nome;
                }

                set
                {
                    nome = value;
                }
            }

            public override string ToString()
            {
                return nome;
            }
        }

        private void cblistTurma_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            loadDisciplina();               
        }
    }
}
