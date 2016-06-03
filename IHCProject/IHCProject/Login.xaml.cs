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
        private static string key = "WASERDTFVGYHJCKC";
        

        public Login()
        {
            InitializeComponent();
            CN = new SqlConnection("Data Source =tcp: 193.136.175.33\\SQLSERVER2012,8293;Initial Catalog = ''; uid = p4g1;password =deadpool;");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            int userId;
            if (!int.TryParse(userBox.Text, out userId)) {
                Console.WriteLine("user não numérico return");
                MessageBox.Show("Número de utilizador inválido");
                return;
            }


            string pass = null;
            int id = -1;
            string dataNascimento = null;
            string nome = null;
            int idade = -1;

            int contextoUtilizador = 0;
            try
            {

                if (CN.State==ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "EXEC PROJETO.p_loginProf @idProf,@key1";
                CMD.Parameters.AddWithValue("@idProf", userBox.Text);
                CMD.Parameters.AddWithValue("@key1", key);
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
                    contextoUtilizador = 1;
                    CMD.CommandText = "EXEC PROJETO.p_loginAluno @idAluno,@key2";
                    CMD.Parameters.AddWithValue("@idAluno", userBox.Text);
                    CMD.Parameters.AddWithValue("@key2", key);
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

                // é um admistrador
                if (pass == null)
                {
                    contextoUtilizador = 2;
                    CMD.CommandText = "EXEC PROJETO.p_loginSecretaria @idUser,@key";
                    CMD.Parameters.AddWithValue("@idUser", userBox.Text);
                    CMD.Parameters.AddWithValue("@key", key);
                    RDR = CMD.ExecuteReader();
                    if (RDR.Read())
                    {
                        pass = RDR["password"].ToString();
                        id = int.Parse(RDR["idAdmin"].ToString());
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
                if (contextoUtilizador == 0)
                {
                    //verificar pass
                    if (password.Password.Equals(pass))
                    {
                        //entrar na conta do prof
                        Professor prof = new Professor(id, nome, idade, dataNascimento);
                        MinhasDisciplinas Menu = new MinhasDisciplinas(prof, CN);
                        this.NavigationService.Navigate(Menu);
                    }
                    else {
                        MessageBox.Show("Password de utilizador inválida");
                        return;
                    }
                }
                else if (contextoUtilizador == 1)
                {
                    //verificar pass
                    if (password.Password.Equals(pass))
                    {
                        //entrar na conta do aluno
                        Aluno aluno = new Aluno(id, nome, idade, dataNascimento);
                        AlunoInterface.AlunoHome home = new AlunoInterface.AlunoHome(aluno, CN);
                        this.NavigationService.Navigate(home);
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
                        Console.WriteLine("secretaria");
                        //entrar na conta do aluno
                        SecretariaData sec = new SecretariaData(id, nome, idade, dataNascimento);
                        Secretaria.Inicio home = new Secretaria.Inicio(sec, CN);
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

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) {
                button_Click(sender, null);
            }
        }
    }
}
