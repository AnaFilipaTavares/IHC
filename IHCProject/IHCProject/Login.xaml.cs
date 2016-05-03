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
            bool isProf = true;
            try
            {

                if (CN.State==ConnectionState.Open) CN.Close();
                CN.Open();
                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "SELECT * FROM ESCOLA_SECUNDARIA.PROFESSORCONTAS WHERE idProf=@idProf;";
                CMD.Parameters.AddWithValue("@idProf",userBox.Text);
                SqlDataReader RDR = CMD.ExecuteReader();
                if (RDR.Read()) {
                    pass=RDR["pass"].ToString();
                }
                Console.WriteLine("IdProf: "+pass);
                RDR.Close();
                
                
                // é um aluno
                if (pass==null) {
                    isProf = false;
                    CMD.CommandText = "SELECT * FROM ESCOLA_SECUNDARIA.ALUNOCONTAS WHERE idAluno=@idAluno;";
                    CMD.Parameters.AddWithValue("@idAluno", userBox.Text);
                    RDR = CMD.ExecuteReader();
                    if (RDR.Read())
                    {
                        pass = RDR["pass"].ToString();
                    }
                    
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
                        Prof_Home Menu = new Prof_Home();
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
                        Console.WriteLine("entrar conta aluno");
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
