using IHCProject.infoClass;
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

namespace IHCProject.AlunoInterface
{
    /// <summary>
    /// Interaction logic for Notas.xaml
    /// </summary>
    public partial class Faltas : Page
    {

        private Aluno aluno;
        private SqlConnection CN;
        private SqlCommand CMD;


        public Faltas()
        {
            InitializeComponent();
        }

        public Faltas(Aluno aluno, SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.aluno = aluno;
            this.CN = cn;
            loadData();
        }

        private void Perfil_Click(object sender, RoutedEventArgs e)
        {
            AlunoHome p = new AlunoHome(aluno, CN);
            this.NavigationService.Navigate(p);
        }

        private void Horario_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HorarioAluno(aluno, CN));
        }

        private void Avaliacao_Click(object sender, RoutedEventArgs e)
        {
            ConsultarAvaliação p = new ConsultarAvaliação(aluno, CN);
            this.NavigationService.Navigate(p);
        }

        private void Notas_Click(object sender, RoutedEventArgs e)
        {
            Notas p = new Notas(aluno, CN);
            this.NavigationService.Navigate(p);
        }

        private void Falta_Click(object sender, RoutedEventArgs e)
        {
            Faltas p = new Faltas(aluno, CN);
            this.NavigationService.Navigate(p);
        }

        private void Plano_Click(object sender, RoutedEventArgs e)
        {
            PlanoCurso p = new PlanoCurso(aluno, CN);
            this.NavigationService.Navigate(p);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Login());
        }

        private void Aula_Click(object sender, RoutedEventArgs e)
        {
            DisciplinasAluno d = new DisciplinasAluno(aluno, CN);
            this.NavigationService.Navigate(d);
        }

        private void loadData()
        {
            List<Falta> items = new List<Falta>();
            faltas.Background = Brushes.LightSeaGreen;
            try
            {
                if (CN.State == System.Data.ConnectionState.Closed) CN.Open();
                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "EXEC PROJETO.p_numFaltasAluno @aluno;";
                CMD.Parameters.AddWithValue("@aluno", aluno.IdAluno);
                SqlDataReader RDR = CMD.ExecuteReader();

                while (RDR.Read())
                {
                    items.Add(new Falta() { Disciplina = RDR["nome"].ToString(), Numero = (int)RDR["nFaltas"], Datas = new List<Data>()});
                }
                RDR.Close();
                CMD.CommandText = "EXEC PROJETO.p_faltasAluno @idaluno";
                CMD.Parameters.AddWithValue("@idaluno", aluno.IdAluno);
                SqlDataReader RDR2 = CMD.ExecuteReader();
                int j = 1;
                while (RDR2.Read())
                {
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].Disciplina.Equals(RDR2["disciplina"].ToString()))
                        {
                            items[i].Datas.Add(new Data() { Date = RDR2["data"].ToString().Split(' ')[0], Id = j, Sumario = RDR2["sumario"].ToString(), Descrição = RDR2["justificação"].ToString(), Tipo = RDR2["tipo"].ToString() });
                        }
                    }
                    j++;
                }

                listView.ItemsSource = items;
                RDR2.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public static T FindAncestorOrSelf<T>(DependencyObject obj)
       where T : DependencyObject
        {
            while (obj != null)
            {
                T objTest = obj as T;

                if (objTest != null)
                    return objTest;

                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //buscar a falta da linha da listview aonde combobox foi escolhida
            ListViewItem lvItem = FindAncestorOrSelf<ListViewItem>(sender as ComboBox);
            ListView listView = ItemsControl.ItemsControlFromItemContainer(lvItem) as ListView;
            int index = listView.ItemContainerGenerator.IndexFromContainer(lvItem);
            ItemCollection listitem = listView.Items;
            Falta f = (Falta)listitem.GetItemAt(index);

            var comboBox = sender as ComboBox;

            Data data = comboBox.SelectedItem as Data;

            FaltasAluno fa = new FaltasAluno(aluno.IdAluno, f.Disciplina,data);
            fa.ShowDialog();
       }
    }
}

public class Falta
{
    private string disciplina;

    private int numero;

    private List<Data> datas;

    public string Disciplina
    {
        get
        {
            return disciplina;
        }

        set
        {
            disciplina = value;
        }
    }

    public int Numero
    {
        get
        {
            return numero;
        }

        set
        {
            numero = value;
        }
    }

    public List<Data> Datas
    {
        get
        {
            return datas;
        }

        set
        {
            datas = value;
        }
    }
}

public class Data
{
    private string data;

    private int id;

    private string sumario;

    private string descrição;

    private string tipo;
    public string Date
    {
        get
        {
            return data;
        }

        set
        {
            data = value;
        }
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

    public string Sumario
    {
        get
        {
            return sumario;
        }

        set
        {
            sumario = value;
        }
    }

    public string Descrição
    {
        get
        {
            return descrição;
        }

        set
        {
            descrição = value;
        }
    }

    public string Tipo
    {
        get
        {
            return tipo;
        }

        set
        {
            tipo = value;
        }
    }

    public override string ToString()
    {
        return Id + " - " + Date;
    }
}
