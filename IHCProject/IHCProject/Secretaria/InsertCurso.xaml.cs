using System;
using System.Collections.Generic;
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
    /// Interaction logic for InsertCurso.xaml
    /// </summary>
    public partial class InsertCurso : Page
    {
        private SqlConnection CN;
        private SqlCommand CMD;
        public InsertCurso()
        {
            InitializeComponent();
        }

        public InsertCurso(SqlConnection CN) : this()
        {
            this.CN = CN;
            loadData();
        }

        private void loadData()
        {

            try
            {
                if (CN.State == System.Data.ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "SELECT nome,codigo FROM PROJETO.DISCIPLINA;";
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    listView.Items.Add(new DisciplinaAno(RDR["nome"].ToString(), int.Parse(RDR["codigo"].ToString()), true));

                }
                RDR.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na base de dados");
                Console.WriteLine(ex.Message);
            }

            listView.Items.Add(new DisciplinaAno("", false));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (CN.State == System.Data.ConnectionState.Closed) CN.Open();

            try
            {
                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "EXEC PROJETO.p_codigoDisciplina;";
                int codigoMaxDisciplina = (int)CMD.ExecuteScalar();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "EXEC PROJETO.p_codigoCurso;";
                int codigoMaxCurso= (int)CMD.ExecuteScalar();
                String disciplina;
                int anos;
                String curso;
                if (textBox.Text.Length == 0)
                {
                    MessageBox.Show("Introduza um curso");
                    return;
                }

                else {
                    curso = textBox.Text.ToString();
                    CMD = new SqlCommand();
                    CMD.Connection = CN;
                    CMD.CommandText = "EXEC PROJETO.p_insertCurso @nome, @id,@duração;";
                    CMD.Parameters.AddWithValue("@nome", curso);
                    CMD.Parameters.AddWithValue("@id", ++codigoMaxCurso);
                    CMD.Parameters.AddWithValue("@duração", 3);
                    CMD.ExecuteNonQuery();
                }
                foreach (DisciplinaAno d in listView.Items)
                {
                    if (d.checkBox.IsChecked == true)
                    {
                        anos = (int)d.comboBox.SelectedItem;
                        if (d.nomeDisciplina.IsReadOnly == false) { 
                            disciplina = d.nomeDisciplina.Text;
                            CMD = new SqlCommand();
                            CMD.Connection = CN;
                            CMD.CommandText = "EXEC PROJETO.p_insertDisciplina @nome, @id;";
                            CMD.Parameters.AddWithValue("@nome", disciplina);
                            CMD.Parameters.AddWithValue("@id", ++codigoMaxDisciplina);
                            CMD.ExecuteNonQuery();
                            d.Codigo = codigoMaxDisciplina;

                        }
                        for (int i = 0; i < anos; i++)
                        {
                            CMD = new SqlCommand();
                            CMD.Connection = CN;
                            CMD.CommandText = "EXEC PROJETO.p_insertComposicaoCurso @disciplina,@curso,@ano";
                            CMD.Parameters.AddWithValue("@disciplina", d.Codigo);
                            CMD.Parameters.AddWithValue("@curso", codigoMaxCurso);
                            CMD.Parameters.AddWithValue("@ano", 10+i);
                            CMD.ExecuteNonQuery();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro na base de dados");
                Console.WriteLine(ex.Message);
            }
            listView.Items.Clear();
            loadData();
        }
    }
}
