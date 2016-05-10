using System.Data.SqlClient;

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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using IHCProject.infoClass;

namespace IHCProject
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        private SqlConnection CN;
        private SqlCommand CMD;
        

        public Login()
        {
            InitializeComponent();
            CN = new SqlConnection("Data Source =tcp: 193.136.175.33\\SQLSERVER2012,8293;Initial Catalog = ''; uid = p4g1;password =deadpool;");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string pass = null;
            int id = -1;
            string dataNascimento = null;
            string nome = null;
            int idade = -1;

            bool isProf = true;
            try
            {

                if (CN.State==ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "SELECT T.idProf,nome,idade,dataNascimento,pass FROM ESCOLA_SECUNDARIA.PESSOA JOIN ESCOLA_SECUNDARIA.PROFESSOR JOIN (SELECT * FROM ESCOLA_SECUNDARIA.PROFESSORCONTAS WHERE idProf=@idProf) AS T ON T.idProf=PROFESSOR.idProf ON PESSOA.ncc=PROFESSOR.ncc;";
                CMD.Parameters.AddWithValue("@idProf",userBox.Text);
                SqlDataReader RDR = CMD.ExecuteReader();
                if (RDR.Read()) {
                    pass=RDR["pass"].ToString();
                    id = int.Parse(RDR["idProf"].ToString());
                    nome = RDR["nome"].ToString();
                    dataNascimento = RDR["dataNascimento"].ToString().Split()[0];
                    idade = int.Parse(RDR["idade"].ToString());
                }
                Console.WriteLine("IdProf: "+pass);
                RDR.Close();
                
                
                // é um aluno
                if (pass==null) {
                    isProf = false;
                    CMD.CommandText = "SELECT T.idAluno,nome,idade,dataNascimento,pass FROM ESCOLA_SECUNDARIA.PESSOA JOIN ESCOLA_SECUNDARIA.ALUNO JOIN  (SELECT * FROM ESCOLA_SECUNDARIA.ALUNOCONTAS WHERE idAluno=@idAluno) AS T ON T.idAluno=ALUNO.idAluno ON PESSOA.ncc=Aluno.ncc;";
                    CMD.Parameters.AddWithValue("@idAluno", userBox.Text);
                    RDR = CMD.ExecuteReader();
                    if (RDR.Read())
                    {
                        pass = RDR["pass"].ToString();
                        id = int.Parse(RDR["idAluno"].ToString());
                        nome = RDR["nome"].ToString();
                        dataNascimento = RDR["dataNascimento"].ToString().Split()[0];
                        idade = int.Parse(RDR["idade"].ToString());
                    }
                    RDR.Close();
                    
                }

                //erro nao encontrou utilizador
                if (pass == null) {
                    MessageBox.Show("Número de utilizador inválido");
                    return;
                }

                //entrar na conta respetiva
                if (isProf)
                {
                    //verificar pass
                    if (password.Password.Equals(pass))
                    {
                        //entrar na conta do prof
                        Professor prof = new Professor(id,nome,idade,dataNascimento);
                        MinhasDisciplinas Menu = new MinhasDisciplinas(prof,CN);
                        this.NavigationService.Navigate(Menu);
                    }
                    else {
                        MessageBox.Show("Password de utilizador inválida");
                        return;
                    }
                }
                else {
                    //verificar pass
                    if (password.Password.Equals(pass))
                    {
                        //entrar na conta do aluno
                        Aluno aluno = new Aluno(id, nome, idade, dataNascimento);
                        AlunoInterface.AlunoHome home = new AlunoInterface.AlunoHome(aluno,CN);
                        this.NavigationService.Navigate(home);
                    }
                    else {
                        MessageBox.Show("Password de utilizador inválida");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }
    }
}
