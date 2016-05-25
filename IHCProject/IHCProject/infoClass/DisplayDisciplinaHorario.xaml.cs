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

namespace IHCProject.infoClass
{
    /// <summary>
    /// Interaction logic for DisplayDisciplinaHorario.xaml
    /// </summary>
    public partial class DisplayDisciplinaHorario : UserControl
    {
        public DisplayDisciplinaHorario()
        {
            InitializeComponent();
        }
        public DisplayDisciplinaHorario(DisciplinaHorario dis) {
            InitializeComponent();
            Console.WriteLine(dis.Disciplina);
            txDisciplina.Text = dis.Disciplina;
            txSala.Text = dis.Sala;
            

            Grid.SetRow(this, dis.RowIndex);
            Grid.SetColumn(this, dis.ColumIndex);
        }

      
        
    }
}
