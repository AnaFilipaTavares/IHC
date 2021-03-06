﻿using System;

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

namespace IHCProject.ContextoDisciplina
{
    /// <summary>
    /// Interaction logic for MinhasDisciplinas.xaml
    /// </summary>
    public partial class EditarAula : Window
    {
        private Professor professor;
        private Aula aulaEdit;
        private SqlConnection CN;
        private SqlCommand CMD;
        private List<string> listaFaltasMarcar;
        private List<ListBoxItem> list2Reset;
        public EditarAula()
        {
            InitializeComponent();
            listaFaltasMarcar = new List<string>();
            list2Reset = new List<ListBoxItem>();
        }

        // Construtor para a classe Prof_Home
        public EditarAula(Professor prof,Aula aula,SqlConnection cn) : this()
        {
            // Associa os dados ao contexto da nova página.
            this.professor = prof;
            this.aulaEdit = aula;
            this.CN = cn;
            loadData();
        }

        public void addList(string falta)
        {
            listaFaltasMarcar.Add(falta);
        }
        
        private void loadData() {

            // query para obter os alunos do inscritos

            label.Content = aulaEdit.Disciplina + " "+aulaEdit.AnoDisciplina +" - Editar Aula";
            nAula1.Text = aulaEdit.NumeroAula+"";
            datalabel.Content = "Data: " + aulaEdit.Data;
            sumarioBox.Text = aulaEdit.Sumário;
            List<int> listaAlunocomFalta = new List<int>();
            List<Aluno> listaAluno = new List<Aluno>();
            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN;
                CMD.CommandText = "EXEC PROJETO.p_perfilDisciplina @idHorario";
                CMD.Parameters.AddWithValue("@idHorario", aulaEdit.Horario);
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    listaAluno.Add( new Aluno(int.Parse(RDR["aluno"].ToString()), RDR["nome"].ToString()));
                }
                RDR.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            //query que vê quais os alunos que tem falta nessa aula
            
            try
            {
                if (CN.State == ConnectionState.Closed) CN.Open();

                CMD = new SqlCommand();
                CMD.Connection = CN; 
                CMD.CommandText = "EXEC PROJETO.p_faltasAula @idHorario,@numeroAula";
                CMD.Parameters.AddWithValue("@idHorario", aulaEdit.Horario);
                CMD.Parameters.AddWithValue("@numeroAula", aulaEdit.NumeroAula);
                SqlDataReader RDR = CMD.ExecuteReader();
                while (RDR.Read())
                {
                    listaAlunocomFalta.Add(int.Parse(RDR["aluno"].ToString()));
                }
                RDR.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            //faz dizable a quem já tem falta
            
            foreach (Aluno al in listaAluno)
            {
                int ocorrencias = 0;
                foreach (int i in listaAlunocomFalta) {
                    if (al.IdAluno == i) {
                        ocorrencias++;
                        
                    }
                }
                if (ocorrencias==0)
                    ListaAluno.Items.Add(new CelulaAlunoFalta(al));
            }
        }


        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Despois de guardar só poderá alterar a informação desta aula no histórico de aulas.\n\nTem a certeza que pretende continuar?", "Guardar Aula", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                return;
            }

            //criar aula
            int editarAula = 0;

            if (!nAula1.Text.Equals("") && sumarioBox.Text.Length <= 199)
            {
                try
                {
                    if (CN.State == ConnectionState.Closed) CN.Open();

                    CMD = new SqlCommand();
                    CMD.Connection = CN; 
                    CMD.CommandText = "EXEC PROJETO.p_editSumario @idHorario,@numeroAula,@sumario";
                    CMD.Parameters.AddWithValue("@idHorario", aulaEdit.Horario);
                    CMD.Parameters.AddWithValue("@numeroAula", nAula1.Text);
                    CMD.Parameters.AddWithValue("@sumario", sumarioBox.Text);
                    editarAula = CMD.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                
            }
            else
            {
                MessageBox.Show("Parametros inválidos");
            }


            if (editarAula != 0)
            {
                //MARCAR FALTAS se não existirem erros
                Console.WriteLine("Aula Editada");
                foreach (CelulaAlunoFalta elementos in ListaAluno.Items)
                {
                    if (elementos.getMarcarfalta() == true)
                    {
                        int erros = 0;
                        try
                        {
                            if (CN.State == ConnectionState.Closed) CN.Open();

                            CMD = new SqlCommand();
                            CMD.Connection = CN;
                            CMD.CommandText = "EXEC PROJETO.p_insertFaltas @idAluno,@idHorario,@id,@numeroAula,@tipo";
                            CMD.Parameters.AddWithValue("@idAluno", elementos.getNumero());
                            CMD.Parameters.AddWithValue("@idHorario", aulaEdit.Horario);
                            CMD.Parameters.AddWithValue("@id", aulaEdit.Id);
                            CMD.Parameters.AddWithValue("@numeroAula", nAula1.Text);
                            CMD.Parameters.AddWithValue("@tipo", elementos.getTipofalta()[0]);
                            erros = CMD.ExecuteNonQuery();

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        Console.WriteLine(elementos + " " + elementos.getTipofalta());
                        if (erros != 0)
                        {
                            Console.WriteLine("Falta adicionada");
                        }
                        else {
                            Console.WriteLine("Erro na falta " + elementos);
                        }
                    }
                }

            }
            else {
                MessageBox.Show("Erro na edição da aula, não foram marcadas as faltas");
                Console.WriteLine("Erro na edição da aula");
                resetFaltas_Click(sender, e);
            }

            this.Close();

        }



        private void resetFaltas_Click(object sender, RoutedEventArgs e)
        {
            foreach (CelulaAlunoFalta elementos in ListaAluno.Items)
            {
                elementos.checkMarcarFalta.IsChecked = false;
                elementos.cbTipoFalta.SelectedIndex = 0;
            }
        }
        /*
            retorna true se for para sair
            
        */
        private bool warningExit()
        {
            if (MessageBox.Show("Se saires desta página todas as alterações seram perdidas.\n\nDesejas Continuar?", "Guardar Aula", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                return true;
            }
            return false;
        }
    }
    
}
