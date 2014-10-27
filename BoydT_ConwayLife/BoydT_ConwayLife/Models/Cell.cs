using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BoydT_ConwayLife.Models {
    class Cell : INotifyPropertyChanged {

        public event PropertyChangedEventHandler PropertyChanged;
        bool _isAlive;

        //switches the cell to alive or dead
        internal void ParentClicked(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            isAlive = !isAlive;
            Console.WriteLine(isAlive);
        }

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
    }
}
