﻿using System;
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
    /// Interaction logic for InsertEE.xaml
    /// </summary>
    public partial class InsertEE : Page
    {
        private SqlConnection cN;
        public InsertEE()
        {
            InitializeComponent();
        }

        public InsertEE(SqlConnection cN): this()
        {
            this.cN = cN;
        }

        private void Curso_Click(object sender, RoutedEventArgs e)
        {
            InsertCurso c = new InsertCurso(cN);
            this.NavigationService.Navigate(c);
        }

        private void Prof_Click(object sender, RoutedEventArgs e)
        {
            InsertProf p = new InsertProf(cN);
            this.NavigationService.Navigate(p);
        }

        private void Aluno_Click(object sender, RoutedEventArgs e)
        {
            InsertAluno a = new InsertAluno(cN);
            this.NavigationService.Navigate(a);
        }

        private void EE_Click(object sender, RoutedEventArgs e)
        {
            InsertEE x = new InsertEE(cN);
            this.NavigationService.Navigate(x);
        }



        private void button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Gestao_Click(object sender, RoutedEventArgs e)
        {
            GestInico x = new GestInico(cN);
            this.NavigationService.Navigate(x);
        }
    }
}
