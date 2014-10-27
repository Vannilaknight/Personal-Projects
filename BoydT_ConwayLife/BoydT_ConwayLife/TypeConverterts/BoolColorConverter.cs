using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace BoydT_ConwayLife.TypeConverterts {
    class BoolColorConverter : IValueConverter, INotifyPropertyChanged {

        //Converts boolean to brush to paint cell
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {

            if (targetType != typeof(Brush))
                throw new Exception("The conversion target must be of type Brush");

            SolidColorBrush scb = null;

            bool state;
            if (bool.TryParse(value.ToString(), out state)) {
                scb = state ? TrueBrush : FalseBrush;
            }

            return scb;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private SolidColorBrush _trueBrush;
        private SolidColorBrush _falseBrush;

        public SolidColorBrush TrueBrush {
            get { return _trueBrush; }
            set {
                _trueBrush = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        public SolidColorBrush FalseBrush {
            get { return _falseBrush; }
            set {
                _falseBrush = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }
    }
}
