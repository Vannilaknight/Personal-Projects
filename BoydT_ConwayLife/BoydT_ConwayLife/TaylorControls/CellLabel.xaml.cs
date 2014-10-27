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
using BoydT_ConwayLife.TypeConverterts;
using System.ComponentModel;

namespace BoydT_ConwayLife.TaylorControls {
    /// <summary>
    /// Interaction logic for CellLabel.xaml
    /// </summary>
    public partial class CellLabel : UserControl, INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isAlive;

        public bool isAlive {
            get {
                return _isAlive;
            }
            set {
                _isAlive = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("isAlive"));
            }
        }

        Point position;
        SolidColorBrush aliveBrush = new SolidColorBrush();
        SolidColorBrush deadBrush = new SolidColorBrush();

        public CellLabel(Point pos) {
            position = pos;
            InitializeComponent();
        }

        internal void ParentClicked(object sender, MouseButtonEventArgs e) {
            isAlive = !isAlive;
        }
    }
}
