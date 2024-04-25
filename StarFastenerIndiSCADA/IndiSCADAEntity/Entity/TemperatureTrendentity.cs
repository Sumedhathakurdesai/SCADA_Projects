using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class TemperatureTrendentity : INotifyPropertyChanged
    {
        #region "Public/Private Property"

        private int _SoakDegreasing;
        public int SoakDegreasing
        {
            get
            {
                return _SoakDegreasing;
            }
            set
            {
                _SoakDegreasing = value;
                OnPropertyChanged("SoakDegreasing");

            }
        }
        private int _Anodic1;
        public int Anodic1
        {
            get
            {
                return _Anodic1;
            }
            set
            {
                _Anodic1 = value;
                OnPropertyChanged("Anodic1");

            }
        }
        private int _Anodic2;
        public int Anodic2
        {
            get
            {
                return _Anodic2;
            }
            set
            {
                _Anodic2 = value;
                OnPropertyChanged("Anodic2");

            }
        }
        private int _AlZinc1;
        public int AlZinc1
        {
            get
            {
                return _AlZinc1;
            }
            set
            {
                _AlZinc1 = value;
                OnPropertyChanged("AlZinc1");

            }
        }
        private int _AlZinc2;
        public int AlZinc2
        {
            get
            {
                return _AlZinc2;
            }
            set
            {
                _AlZinc2 = value;
                OnPropertyChanged("AlZinc2");

            }
        }
        private int _AlZinc3;
        public int AlZinc3
        {
            get
            {
                return _AlZinc3;
            }
            set
            {
                _AlZinc3 = value;
                OnPropertyChanged("AlZinc3");

            }
        }
        private int _AlZinc4;
        public int AlZinc4
        {
            get
            {
                return _AlZinc4;
            }
            set
            {
                _AlZinc4 = value;
                OnPropertyChanged("AlZinc4");

            }
        }
        private int _AlZinc5;
        public int AlZinc5
        {
            get
            {
                return _AlZinc5;
            }
            set
            {
                _AlZinc5 = value;
                OnPropertyChanged("AlZinc5");

            }
        }
        private int _AlZinc6;
        public int AlZinc6
        {
            get
            {
                return _AlZinc6;
            }
            set
            {
                _AlZinc6 = value;
                OnPropertyChanged("AlZinc6");

            }
        }
        private int _AlZinc7;
        public int AlZinc7
        {
            get
            {
                return _AlZinc7;
            }
            set
            {
                _AlZinc7 = value;
                OnPropertyChanged("AlZinc7");

            }
        }
        private int _AlZinc8;
        public int AlZinc8
        {
            get
            {
                return _AlZinc8;
            }
            set
            {
                _AlZinc8 = value;
                OnPropertyChanged("AlZinc8");

            }
        }
        private int _AlZinc9;
        public int AlZinc9
        {
            get
            {
                return _AlZinc9;
            }
            set
            {
                _AlZinc9 = value;
                OnPropertyChanged("AlZinc9");

            }
        }
        private int _Pass1;
        public int Pass1
        {
            get
            {
                return _Pass1;
            }
            set
            {
                _Pass1 = value;
                OnPropertyChanged("Pass1");

            }
        }
        private int _Pass2;
        public int Pass2
        {
            get
            {
                return _Pass2;
            }
            set
            {
                _Pass2 = value;
                OnPropertyChanged("Pass2");

            }
        }
        private int _Pass3;
        public int Pass3
        {
            get
            {
                return _Pass3;
            }
            set
            {
                _Pass3 = value;
                OnPropertyChanged("Pass3");

            }
        }
        private int _Pass4;
        public int Pass4
        {
            get
            {
                return _Pass4;
            }
            set
            {
                _Pass4 = value;
                OnPropertyChanged("Pass4");

            }
        }
        private int _Pass5;
        public int Pass5
        {
            get
            {
                return _Pass5;
            }
            set
            {
                _Pass5 = value;
                OnPropertyChanged("Pass5");

            }
        }
        private int _Dryer1;
        public int Dryer1
        {
            get
            {
                return _Dryer1;
            }
            set
            {
                _Dryer1= value;
                OnPropertyChanged("Dryer1");

            }
        }
        private int _Dryer2;
        public int Dryer2
        {
            get
            {
                return _Dryer2;
            }
            set
            {
                _Dryer2 = value;
                OnPropertyChanged("Dryer2");

            }
        }
  
        private DateTime _DateTimeCol;
        public DateTime DateTimeCol
        {
            get
            {
                return _DateTimeCol;
            }
            set
            {
                _DateTimeCol = value;
                OnPropertyChanged("DateTimeCol");
            }
        }

        private string _TimeCol;
        public string TimeCol
        {
            get
            {
                return _TimeCol;
            }
            set
            {
                _TimeCol = value;
                OnPropertyChanged("TimeCol");
            }
        }

        private int _Temp20;
        public int Temp20
        {
            get
            {
                return _Temp20;
            }
            set
            {
                _Temp20 = value;
                OnPropertyChanged("Temp20");

            }
        }
        private int _Temp21;
        public int Temp21
        {
            get
            {
                return _Temp21;
            }
            set
            {
                _Temp21 = value;
                OnPropertyChanged("Temp21");

            }
        }
        private int _Temp22;
        public int Temp22
        {
            get
            {
                return _Temp22;
            }
            set
            {
                _Temp22 = value;
                OnPropertyChanged("Temp22");

            }
        }
        private int _Temp23;
        public int Temp23
        {
            get
            {
                return _Temp23;
            }
            set
            {
                _Temp23 = value;
                OnPropertyChanged("Temp23");

            }
        }
        private int _Temp24;
        public int Temp24
        {
            get
            {
                return _Temp24;
            }
            set
            {
                _Temp24 = value;
                OnPropertyChanged("Temp24");

            }
        }
        private int _Temp25;
        public int Temp25
        {
            get
            {
                return _Temp25;
            }
            set
            {
                _Temp25 = value;
                OnPropertyChanged("Temp25");

            }
        }



        private DateTime _LiveTime;
        public DateTime LiveTime
        {
            get
            {
                return _LiveTime;
            }
            set
            {
                _LiveTime = value;
                OnPropertyChanged("LiveTime");

            }
        }
        private string _TempStationName;
        public string TempStationName
        {
            get
            {
                return _TempStationName;
            }
            set
            {
                _TempStationName = value;
                OnPropertyChanged("TempStationName");

            }
        }
        private double _LiveTemp1;
        public double LiveTemp1
        {
            get
            {
                return _LiveTemp1;
            }
            set
            {
                _LiveTemp1 = value;
                OnPropertyChanged("LiveTemp1");

            }
        }
        private double _LiveTemp2;
        public double LiveTemp2
        {
            get
            {
                return _LiveTemp2;
            }
            set
            {
                _LiveTemp2 = value;
                OnPropertyChanged("LiveTemp2");

            }
        }
        private double _LiveTemp3;
        public double LiveTemp3
        {
            get
            {
                return _LiveTemp3;
            }
            set
            {
                _LiveTemp3 = value;
                OnPropertyChanged("LiveTemp3");

            }
        }
        private double _LiveTemp4;
        public double LiveTemp4
        {
            get
            {
                return _LiveTemp4;
            }
            set
            {
                _LiveTemp4 = value;
                OnPropertyChanged("LiveTemp4");

            }
        }
        private double _LiveTemp5;
        public double LiveTemp5
        {
            get
            {
                return _LiveTemp5;
            }
            set
            {
                _LiveTemp5 = value;
                OnPropertyChanged("LiveTemp5");

            }
        }
        private double _LiveTemp6;
        public double LiveTemp6
        {
            get
            {
                return _LiveTemp6;
            }
            set
            {
                _LiveTemp6 = value;
                OnPropertyChanged("LiveTemp6");

            }
        }
        private double _LiveTemp7;
        public double LiveTemp7
        {
            get
            {
                return _LiveTemp7;
            }
            set
            {
                _LiveTemp7 = value;
                OnPropertyChanged("LiveTemp7");

            }
        }
        private double _LiveTemp8;
        public double LiveTemp8
        {
            get
            {
                return _LiveTemp8;
            }
            set
            {
                _LiveTemp8= value;
                OnPropertyChanged("LiveTemp8");

            }
        }
        private double _LiveTemp9;
        public double LiveTemp9
        {
            get
            {
                return _LiveTemp9;
            }
            set
            {
                _LiveTemp9= value;
                OnPropertyChanged("LiveTemp9");

            }
        }
        private double _LiveTemp10;
        public double LiveTemp10
        {
            get
            {
                return _LiveTemp10;
            }
            set
            {
                _LiveTemp10 = value;
                OnPropertyChanged("LiveTemp10");

            }
        }
        private double _LiveTemp11;
        public double LiveTemp11
        {
            get
            {
                return _LiveTemp11;
            }
            set
            {
                _LiveTemp11 = value;
                OnPropertyChanged("LiveTemp11");

            }
        }
        private double _LiveTemp12;
        public double LiveTemp12
        {
            get
            {
                return _LiveTemp12;
            }
            set
            {
                _LiveTemp12 = value;
                OnPropertyChanged("LiveTemp12");

            }
        }
        private double _LiveTemp13;
        public double LiveTemp13
        {
            get
            {
                return _LiveTemp13;
            }
            set
            {
                _LiveTemp13 = value;
                OnPropertyChanged("LiveTemp13");

            }
        }
        private double _LiveTemp14;
        public double LiveTemp14
        {
            get
            {
                return _LiveTemp14;
            }
            set
            {
                _LiveTemp14 = value;
                OnPropertyChanged("LiveTemp14");

            }
        }
        private double _LiveTemp15;
        public double LiveTemp15
        {
            get
            {
                return _LiveTemp15;
            }
            set
            {
                _LiveTemp15 = value;
                OnPropertyChanged("LiveTemp15");

            }
        }
        private double _LiveTemp16;
        public double LiveTemp16
        {
            get
            {
                return _LiveTemp16;
            }
            set
            {
                _LiveTemp16 = value;
                OnPropertyChanged("LiveTemp16");

            }
        }
        private double _LiveTemp17;
        public double LiveTemp17
        {
            get
            {
                return _LiveTemp17;
            }
            set
            {
                _LiveTemp17 = value;
                OnPropertyChanged("LiveTemp17");

            }
        }
        private double _LiveTemp18;
        public double LiveTemp18
        {
            get
            {
                return _LiveTemp18;
            }
            set
            {
                _LiveTemp18 = value;
                OnPropertyChanged("LiveTemp18");

            }
        }
        private double _LiveTemp19;
        public double LiveTemp19
        {
            get
            {
                return _LiveTemp19;
            }
            set
            {
                _LiveTemp19 = value;
                OnPropertyChanged("LiveTemp19");

            }
        }
        private double _LiveTemp20;
        public double LiveTemp20
        {
            get
            {
                return _LiveTemp20;
            }
            set
            {
                _LiveTemp20 = value;
                OnPropertyChanged("LiveTemp20");

            }
        }
        private double _LiveTemp21;
        public double LiveTemp21
        {
            get
            {
                return _LiveTemp21;
            }
            set
            {
                _LiveTemp21 = value;
                OnPropertyChanged("LiveTemp21");

            }
        }
        private double _LiveTemp22;
        public double LiveTemp22
        {
            get
            {
                return _LiveTemp22;
            }
            set
            {
                _LiveTemp22 = value;
                OnPropertyChanged("LiveTemp22");

            }
        }
        private double _LiveTemp23;
        public double LiveTemp23
        {
            get
            {
                return _LiveTemp23;
            }
            set
            {
                _LiveTemp23 = value;
                OnPropertyChanged("LiveTemp23");

            }
        }
        private double _LiveTemp24;
        public double LiveTemp24
        {
            get
            {
                return _LiveTemp24;
            }
            set
            {
                _LiveTemp24 = value;
                OnPropertyChanged("LiveTemp24");

            }
        }
        private double _LiveTemp25;
        public double LiveTemp25
        {
            get
            {
                return _LiveTemp25;
            }
            set
            {
                _LiveTemp25 = value;
                OnPropertyChanged("LiveTemp25");

            }
        }
        #endregion


        #region Constructor
        public TemperatureTrendentity()
        { }

        public TemperatureTrendentity(TemperatureTrendentity Obj)
        {
            _TempStationName = Obj.TempStationName;
            _LiveTime = Obj.LiveTime;
            _LiveTemp1 = Obj.LiveTemp1;
            _LiveTemp2 = Obj.LiveTemp2;
            _LiveTemp3 = Obj.LiveTemp3;
            _LiveTemp4 = Obj.LiveTemp4;
            _LiveTemp5 = Obj.LiveTemp5;
            _LiveTemp6 = Obj.LiveTemp6;
            _LiveTemp7 = Obj.LiveTemp7;
            _LiveTemp8 = Obj.LiveTemp8;
            _LiveTemp9 = Obj.LiveTemp9;
            _LiveTemp10 = Obj.LiveTemp10;
            _LiveTemp11 = Obj.LiveTemp11;
            _LiveTemp12 = Obj.LiveTemp12;
            _LiveTemp13 = Obj.LiveTemp13;
            _LiveTemp14 = Obj.LiveTemp14;
            _LiveTemp15 = Obj.LiveTemp15;
            _LiveTemp16 = Obj.LiveTemp16;
            _LiveTemp17 = Obj.LiveTemp17;
            _LiveTemp18 = Obj.LiveTemp18;
            _LiveTemp19 = Obj.LiveTemp19;
            _LiveTemp20 = Obj.LiveTemp20;
            _LiveTemp21 = Obj.LiveTemp21;
            _LiveTemp22 = Obj.LiveTemp22;
            _LiveTemp23 = Obj.LiveTemp23;
            _LiveTemp24 = Obj.LiveTemp24;
            _LiveTemp25 = Obj.LiveTemp25;

            _DateTimeCol = Obj.DateTimeCol;
            _SoakDegreasing = Obj.SoakDegreasing;
            _Anodic1 = Obj.Anodic1;
            _Anodic2 = Obj.Anodic2;
            _AlZinc1 = Obj.AlZinc1;
            _AlZinc2 = Obj.AlZinc2;
            _AlZinc3 = Obj.AlZinc3;
            _AlZinc4 = Obj.AlZinc4;
            _AlZinc5 = Obj.AlZinc5;
            _AlZinc6 = Obj.AlZinc6;
            _AlZinc7 = Obj.AlZinc7;
            _AlZinc8 = Obj.AlZinc8;
            _AlZinc9 = Obj.AlZinc9;
            _Pass1 = Obj.Pass1;
            _Pass2 = Obj.Pass2;
            _Pass3 = Obj.Pass3;
            _Pass4 = Obj.Pass4;
            _Pass5 = Obj.Pass5;
            _Dryer1 = Obj.Dryer1;
            _Dryer2 = Obj.Dryer2;

            _Temp20 = Obj.Temp20;
            _Temp21 = Obj.Temp21;
            _Temp22 = Obj.Temp22;
            _Temp23 = Obj.Temp23;
            _Temp24 = Obj.Temp24;
            _Temp25 = Obj.Temp25;

            _TimeCol = Obj.TimeCol;
        }
        #endregion
        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
        #endregion
    }
}
