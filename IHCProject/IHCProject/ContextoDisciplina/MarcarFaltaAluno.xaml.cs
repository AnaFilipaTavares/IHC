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
    public partial class MarcarFaltaAluno : Window
    {

        private Aluno aluno;
        private List<string> listaAlunosMarcarFalta;
        private SqlConnection CN;
        private SqlCommand CMD;

        public MarcarFaltaAluno()
        {
            InitializeComponent();
        }

        // Construtor para a classe Prof_Home
        public MarcarFaltaAluno(Aluno aluno,List<string> listamarcarfalta,SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.aluno = aluno;
            listaAlunosMarcarFalta = listamarcarfalta;
            this.CN = cn;
            loadData();
        }

      
        private void loadData() {
            nomeAluno.Content = "Nome: " + aluno.Nome;
            numeroAluno.Content = "Número: " + aluno.IdAluno;

        }
        
        private void marcarFalta_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Tem a certeza que pretende marcar uma falta de "+((ComboBoxItem)comboBox.SelectedValue).Content+ " ao aluno \n" + aluno.Nome, "Marcar falta", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                listaAlunosMarcarFalta.Add(aluno.IdAluno+" "+((ComboBoxItem)comboBox.SelectedValue).Content);
                this.Close();
            }
            else {
                Console.WriteLine("Não marcou falta");
            }
        }
    }
    
}
