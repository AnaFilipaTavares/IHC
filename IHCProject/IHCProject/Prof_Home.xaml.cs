﻿using System;
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

namespace IHCProject
{
    /// <summary>
    /// Interaction logic for Prof_Home.xaml
    /// </summary>
    public partial class Prof_Home : Page
    {
        public Prof_Home()
        {
            InitializeComponent();
        }

        private void horarioClick(object sender, RoutedEventArgs e)
        {
           MessageBox.Show("horaior");
        }

        private void disciplinaClick(object sender, RoutedEventArgs e)
        {
            DisciplinaPopUp d = new DisciplinaPopUp();
            d.ShowDialog();
        }

    }
}
