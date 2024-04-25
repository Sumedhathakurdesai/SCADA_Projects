using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiSCADAEntity.Entity
{
    public partial class RectifierTrendEntity : INotifyPropertyChanged
    {
        #region "Public/Private Property"

        #region trade current properties
        private int _Anodic1ActualCurrent;
        public int Anodic1ActualCurrent
        {
            get
            {
                return _Anodic1ActualCurrent;
            }
            set
            {
                _Anodic1ActualCurrent = value;
                OnPropertyChanged("Anodic1ActualCurrent");

            }
        }
        private int _Anodic1SetCurrent;
        public int Anodic1SetCurrent
        {
            get
            {
                return _Anodic1SetCurrent;
            }
            set
            {
                _Anodic1SetCurrent = value;
                OnPropertyChanged("Anodic1SetCurrent");

            }
        }

        private int _Anodic2ActualCurrent;
        public int Anodic2ActualCurrent
        {
            get
            {
                return _Anodic2ActualCurrent;
            }
            set
            {
                _Anodic2ActualCurrent = value;
                OnPropertyChanged("Anodic2ActualCurrent");

            }
        }
        private int _Anodic2SetCurrent;
        public int Anodic2SetCurrent
        {
            get
            {
                return _Anodic2SetCurrent;
            }
            set
            {
                _Anodic2SetCurrent = value;
                OnPropertyChanged("Anodic2SetCurrent");

            }
        }
        private int _Anodic3ActualCurrent;
        public int Anodic3ActualCurrent
        {
            get
            {
                return _Anodic3ActualCurrent;
            }
            set
            {
                _Anodic3ActualCurrent = value;
                OnPropertyChanged("Anodic3ActualCurrent");

            }
        }
        private int _Anodic3SetCurrent;
        public int Anodic3SetCurrent
        {
            get
            {
                return _Anodic3SetCurrent;
            }
            set
            {
                _Anodic3SetCurrent = value;
                OnPropertyChanged("Anodic3SetCurrent");

            }
        }


        private int _AlZn1SetCurrent;
        public int AlZn1SetCurrent
        {
            get
            {
                return _AlZn1SetCurrent;
            }
            set
            {
                _AlZn1SetCurrent = value;
                OnPropertyChanged("AlZn1SetCurrent");

            }
        }

        private int _AlZn1ActualCurrent;
        public int AlZn1ActualCurrent
        {
            get
            {
                return _AlZn1ActualCurrent;
            }
            set
            {
                _AlZn1ActualCurrent = value;
                OnPropertyChanged("AlZn1ActualCurrent");

            }
        }
        private int _AlZn2SetCurrent;
        public int AlZn2SetCurrent
        {
            get
            {
                return _AlZn2SetCurrent;
            }
            set
            {
                _AlZn2SetCurrent = value;
                OnPropertyChanged("AlZn2SetCurrent");

            }
        }


        private int _AlZn2ActualCurrent;
        public int AlZn2ActualCurrent
        {
            get
            {
                return _AlZn2ActualCurrent;
            }
            set
            {
                _AlZn2ActualCurrent = value;
                OnPropertyChanged("AlZn2ActualCurrent");

            }
        }
        private int _AlZn3SetCurrent;
        public int AlZn3SetCurrent
        {
            get
            {
                return _AlZn3SetCurrent;
            }
            set
            {
                _AlZn3SetCurrent = value;
                OnPropertyChanged("AlZn3SetCurrent");

            }
        }


        private int _AlZn3ActualCurrent;
        public int AlZn3ActualCurrent
        {
            get
            {
                return _AlZn3ActualCurrent;
            }
            set
            {
                _AlZn3ActualCurrent = value;
                OnPropertyChanged("AlZn3ActualCurrent");

            }
        }
        private int _AlZn4SetCurrent;
        public int AlZn4SetCurrent
        {
            get
            {
                return _AlZn4SetCurrent;
            }
            set
            {
                _AlZn4SetCurrent = value;
                OnPropertyChanged("AlZn4SetCurrent");

            }
        }


        private int _AlZn4ActualCurrent;
        public int AlZn4ActualCurrent
        {
            get
            {
                return _AlZn4ActualCurrent;
            }
            set
            {
                _AlZn4ActualCurrent = value;
                OnPropertyChanged("AlZn4ActualCurrent");

            }
        }
        private int _AlZn5SetCurrent;
        public int AlZn5SetCurrent
        {
            get
            {
                return _AlZn5SetCurrent;
            }
            set
            {
                _AlZn5SetCurrent = value;
                OnPropertyChanged("AlZn5SetCurrent");

            }
        }


        private int _AlZn5ActualCurrent;
        public int AlZn5ActualCurrent
        {
            get
            {
                return _AlZn5ActualCurrent;
            }
            set
            {
                _AlZn5ActualCurrent = value;
                OnPropertyChanged("AlZn5ActualCurrent");

            }
        }
        private int _AlZn6SetCurrent;
        public int AlZn6SetCurrent
        {
            get
            {
                return _AlZn6SetCurrent;
            }
            set
            {
                _AlZn6SetCurrent = value;
                OnPropertyChanged("AlZn6SetCurrent");

            }
        }


        private int _AlZn6ActualCurrent;
        public int AlZn6ActualCurrent
        {
            get
            {
                return _AlZn6ActualCurrent;
            }
            set
            {
                _AlZn6ActualCurrent = value;
                OnPropertyChanged("AlZn6ActualCurrent");

            }
        }
        private int _AlZn7SetCurrent;
        public int AlZn7SetCurrent
        {
            get
            {
                return _AlZn7SetCurrent;
            }
            set
            {
                _AlZn7SetCurrent = value;
                OnPropertyChanged("AlZn7SetCurrent");

            }
        }


        private int _AlZn7ActualCurrent;
        public int AlZn7ActualCurrent
        {
            get
            {
                return _AlZn7ActualCurrent;
            }
            set
            {
                _AlZn7ActualCurrent = value;
                OnPropertyChanged("AlZn7ActualCurrent");

            }
        }
        private int _AlZn8SetCurrent;
        public int AlZn8SetCurrent
        {
            get
            {
                return _AlZn8SetCurrent;
            }
            set
            {
                _AlZn8SetCurrent = value;
                OnPropertyChanged("AlZn8SetCurrent");

            }
        }


        private int _AlZn8ActualCurrent;
        public int AlZn8ActualCurrent
        {
            get
            {
                return _AlZn8ActualCurrent;
            }
            set
            {
                _AlZn8ActualCurrent = value;
                OnPropertyChanged("AlZn8ActualCurrent");

            }
        }
        private int _AlZn9SetCurrent;
        public int AlZn9SetCurrent
        {
            get
            {
                return _AlZn9SetCurrent;
            }
            set
            {
                _AlZn9SetCurrent = value;
                OnPropertyChanged("AlZn9SetCurrent");

            }
        }


        private int _AlZn9ActualCurrent;
        public int AlZn9ActualCurrent
        {
            get
            {
                return _AlZn9ActualCurrent;
            }
            set
            {
                _AlZn9ActualCurrent = value;
                OnPropertyChanged("AlZn9ActualCurrent");

            }
        }
        private int _AlZn10SetCurrent;
        public int AlZn10SetCurrent
        {
            get
            {
                return _AlZn10SetCurrent;
            }
            set
            {
                _AlZn10SetCurrent = value;
                OnPropertyChanged("AlZn10SetCurrent");

            }
        }


        private int _AlZn10ActualCurrent;
        public int AlZn10ActualCurrent
        {
            get
            {
                return _AlZn10ActualCurrent;
            }
            set
            {
                _AlZn10ActualCurrent = value;
                OnPropertyChanged("AlZn10ActualCurrent");

            }
        }

        private int _AlZn11SetCurrent;
        public int AlZn11SetCurrent
        {
            get
            {
                return _AlZn11SetCurrent;
            }
            set
            {
                _AlZn11SetCurrent = value;
                OnPropertyChanged("AlZn11SetCurrent");

            }
        }


        private int _AlZn11ActualCurrent;
        public int AlZn11ActualCurrent
        {
            get
            {
                return _AlZn11ActualCurrent;
            }
            set
            {
                _AlZn11ActualCurrent = value;
                OnPropertyChanged("AlZn11ActualCurrent");

            }
        }



        private int _AlZn12SetCurrent;
        public int AlZn12SetCurrent
        {
            get
            {
                return _AlZn12SetCurrent;
            }
            set
            {
                _AlZn12SetCurrent = value;
                OnPropertyChanged("AlZn12SetCurrent");

            }
        }


        private int _AlZn12ActualCurrent;
        public int AlZn12ActualCurrent
        {
            get
            {
                return _AlZn12ActualCurrent;
            }
            set
            {
                _AlZn12ActualCurrent = value;
                OnPropertyChanged("AlZn12ActualCurrent");

            }
        }

        private int _AlZn13SetCurrent;
        public int AlZn13SetCurrent
        {
            get
            {
                return _AlZn13SetCurrent;
            }
            set
            {
                _AlZn13SetCurrent = value;
                OnPropertyChanged("AlZn13SetCurrent");

            }
        }


        private int _AlZn13ActualCurrent;
        public int AlZn13ActualCurrent
        {
            get
            {
                return _AlZn13ActualCurrent;
            }
            set
            {
                _AlZn13ActualCurrent = value;
                OnPropertyChanged("AlZn13ActualCurrent");

            }
        }

        private int _AlZn14SetCurrent;
        public int AlZn14SetCurrent
        {
            get
            {
                return _AlZn14SetCurrent;
            }
            set
            {
                _AlZn14SetCurrent = value;
                OnPropertyChanged("AlZn14SetCurrent");

            }
        }


        private int _AlZn14ActualCurrent;
        public int AlZn14ActualCurrent
        {
            get
            {
                return _AlZn14ActualCurrent;
            }
            set
            {
                _AlZn14ActualCurrent = value;
                OnPropertyChanged("AlZn14ActualCurrent");

            }
        }

        private int _AlZn15SetCurrent;
        public int AlZn15SetCurrent
        {
            get
            {
                return _AlZn15SetCurrent;
            }
            set
            {
                _AlZn15SetCurrent = value;
                OnPropertyChanged("AlZn15SetCurrent");

            }
        }


        private int _AlZn15ActualCurrent;
        public int AlZn15ActualCurrent
        {
            get
            {
                return _AlZn15ActualCurrent;
            }
            set
            {
                _AlZn15ActualCurrent = value;
                OnPropertyChanged("AlZn15ActualCurrent");

            }
        }

        private int _AlZn16SetCurrent;
        public int AlZn16SetCurrent
        {
            get
            {
                return _AlZn16SetCurrent;
            }
            set
            {
                _AlZn16SetCurrent = value;
                OnPropertyChanged("AlZn16SetCurrent");

            }
        }


        private int _AlZn16ActualCurrent;
        public int AlZn16ActualCurrent
        {
            get
            {
                return _AlZn16ActualCurrent;
            }
            set
            {
                _AlZn16ActualCurrent = value;
                OnPropertyChanged("AlZn16ActualCurrent");

            }
        }

        private int _AlZn17SetCurrent;
        public int AlZn17SetCurrent
        {
            get
            {
                return _AlZn17SetCurrent;
            }
            set
            {
                _AlZn17SetCurrent = value;
                OnPropertyChanged("AlZn17SetCurrent");

            }
        }


        private int _AlZn17ActualCurrent;
        public int AlZn17ActualCurrent
        {
            get
            {
                return _AlZn17ActualCurrent;
            }
            set
            {
                _AlZn17ActualCurrent = value;
                OnPropertyChanged("AlZn17ActualCurrent");

            }
        }

        private int _AlZn18SetCurrent;
        public int AlZn18SetCurrent
        {
            get
            {
                return _AlZn18SetCurrent;
            }
            set
            {
                _AlZn18SetCurrent = value;
                OnPropertyChanged("AlZn18SetCurrent");

            }
        }


        private int _AlZn18ActualCurrent;
        public int AlZn18ActualCurrent
        {
            get
            {
                return _AlZn18ActualCurrent;
            }
            set
            {
                _AlZn18ActualCurrent = value;
                OnPropertyChanged("AlZn18ActualCurrent");

            }
        }
        #endregion


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


        #region live current

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
        private string _RectStationName;
        public string RectStationName
        {
            get
            {
                return _RectStationName;
            }
            set
            {
                _RectStationName = value;
                OnPropertyChanged("RectStationName");

            }
        }
        private double _LiveRect1;
        public double LiveRect1
        {
            get
            {
                return _LiveRect1;
            }
            set
            {
                _LiveRect1 = value;
                OnPropertyChanged("LiveRect1");

            }
        }
        private double _LiveRect2;
        public double LiveRect2
        {
            get
            {
                return _LiveRect2;
            }
            set
            {
                _LiveRect2 = value;
                OnPropertyChanged("LiveRect2");

            }
        }
        private double _LiveRect3;
        public double LiveRect3
        {
            get
            {
                return _LiveRect3;
            }
            set
            {
                _LiveRect3 = value;
                OnPropertyChanged("LiveRect3");

            }
        }
        private double _LiveRect4;
        public double LiveRect4
        {
            get
            {
                return _LiveRect4;
            }
            set
            {
                _LiveRect4 = value;
                OnPropertyChanged("LiveRect4");

            }
        }
        private double _LiveRect5;
        public double LiveRect5
        {
            get
            {
                return _LiveRect5;
            }
            set
            {
                _LiveRect5 = value;
                OnPropertyChanged("LiveRect5");

            }
        }
        private double _LiveRect6;
        public double LiveRect6
        {
            get
            {
                return _LiveRect6;
            }
            set
            {
                _LiveRect6 = value;
                OnPropertyChanged("LiveRect6");

            }
        }
        private double _LiveRect7;
        public double LiveRect7
        {
            get
            {
                return _LiveRect7;
            }
            set
            {
                _LiveRect7 = value;
                OnPropertyChanged("LiveRect7");

            }
        }
        private double _LiveRect8;
        public double LiveRect8
        {
            get
            {
                return _LiveRect8;
            }
            set
            {
                _LiveRect8 = value;
                OnPropertyChanged("LiveRect8");

            }
        }
        private double _LiveRect9;
        public double LiveRect9
        {
            get
            {
                return _LiveRect9;
            }
            set
            {
                _LiveRect9 = value;
                OnPropertyChanged("LiveRect9");

            }
        }
        private double _LiveRect10;
        public double LiveRect10
        {
            get
            {
                return _LiveRect10;
            }
            set
            {
                _LiveRect10 = value;
                OnPropertyChanged("LiveRect10");

            }
        }
        private double _LiveRect11;
        public double LiveRect11
        {
            get
            {
                return _LiveRect11;
            }
            set
            {
                _LiveRect11 = value;
                OnPropertyChanged("LiveRect11");

            }
        }
        private double _LiveRect12;
        public double LiveRect12
        {
            get
            {
                return _LiveRect12;
            }
            set
            {
                _LiveRect12 = value;
                OnPropertyChanged("LiveRect12");

            }
        }
        private double _LiveRect13;
        public double LiveRect13
        {
            get
            {
                return _LiveRect13;
            }
            set
            {
                _LiveRect13 = value;
                OnPropertyChanged("LiveRect13");

            }
        }
        private double _LiveRect14;
        public double LiveRect14
        {
            get
            {
                return _LiveRect14;
            }
            set
            {
                _LiveRect14 = value;
                OnPropertyChanged("LiveRect14");

            }
        }
        private double _LiveRect15;
        public double LiveRect15
        {
            get
            {
                return _LiveRect15;
            }
            set
            {
                _LiveRect15 = value;
                OnPropertyChanged("LiveRect15");

            }
        }
        private double _LiveRect16;
        public double LiveRect16
        {
            get
            {
                return _LiveRect16;
            }
            set
            {
                _LiveRect16 = value;
                OnPropertyChanged("LiveRect16");

            }
        }
        private double _LiveRect17;
        public double LiveRect17
        {
            get
            {
                return _LiveRect17;
            }
            set
            {
                _LiveRect17 = value;
                OnPropertyChanged("LiveRect17");

            }
        }
        private double _LiveRect18;
        public double LiveRect18
        {
            get
            {
                return _LiveRect18;
            }
            set
            {
                _LiveRect18 = value;
                OnPropertyChanged("LiveRect18");

            }
        }
        private double _LiveRect19;
        public double LiveRect19
        {
            get
            {
                return _LiveRect19;
            }
            set
            {
                _LiveRect19 = value;
                OnPropertyChanged("LiveRect19");

            }
        }
        private double _LiveRect20;
        public double LiveRect20
        {
            get
            {
                return _LiveRect20;
            }
            set
            {
                _LiveRect20 = value;
                OnPropertyChanged("LiveRect20");

            }
        }
        private double _LiveRect21;
        public double LiveRect21
        {
            get
            {
                return _LiveRect21;
            }
            set
            {
                _LiveRect21 = value;
                OnPropertyChanged("LiveRect21");

            }
        }
        private double _LiveRect22;
        public double LiveRect22
        {
            get
            {
                return _LiveRect22;
            }
            set
            {
                _LiveRect22 = value;
                OnPropertyChanged("LiveRect22");

            }
        }
        private double _LiveRect23;
        public double LiveRect23
        {
            get
            {
                return _LiveRect23;
            }
            set
            {
                _LiveRect23 = value;
                OnPropertyChanged("LiveRect23");

            }
        }
        private double _LiveRect24;
        public double LiveRect24
        {
            get
            {
                return _LiveRect24;
            }
            set
            {
                _LiveRect24 = value;
                OnPropertyChanged("LiveRect24");

            }
        }
        private double _LiveRect25;
        public double LiveRect25
        {
            get
            {
                return _LiveRect25;
            }
            set
            {
                _LiveRect25 = value;
                OnPropertyChanged("LiveRect25");

            }
        }
        #endregion

        #endregion

        #region Constructor

        public RectifierTrendEntity()
        { }

        public RectifierTrendEntity(RectifierTrendEntity Obj)
        {
            _RectStationName = Obj.RectStationName;
            _LiveTime = Obj.LiveTime;
            _LiveRect1 = Obj.LiveRect1;
            _LiveRect2 = Obj.LiveRect2;
            _LiveRect3 = Obj.LiveRect3;
            _LiveRect4 = Obj.LiveRect4;
            _LiveRect5 = Obj.LiveRect5;
            _LiveRect6 = Obj.LiveRect6;
            _LiveRect7 = Obj.LiveRect7;
            _LiveRect8 = Obj.LiveRect8;
            _LiveRect9 = Obj.LiveRect9;
            _LiveRect10 = Obj.LiveRect10;
            _LiveRect11 = Obj.LiveRect11;
            _LiveRect12 = Obj.LiveRect12;
            _LiveRect13 = Obj.LiveRect13;
            _LiveRect14 = Obj.LiveRect14;
            _LiveRect15 = Obj.LiveRect15;
            _LiveRect16 = Obj.LiveRect16;
            _LiveRect17 = Obj.LiveRect17;
            _LiveRect18 = Obj.LiveRect18;
            _LiveRect19 = Obj.LiveRect19;
            _LiveRect20 = Obj.LiveRect20;
            _LiveRect21 = Obj.LiveRect21;
            _LiveRect22 = Obj.LiveRect22;
            _LiveRect23 = Obj.LiveRect23;
            _LiveRect24 = Obj.LiveRect24;
            _LiveRect25 = Obj.LiveRect25;


            _Anodic1ActualCurrent = Obj.Anodic1ActualCurrent;
            _Anodic1SetCurrent = Obj.Anodic1SetCurrent;
            _Anodic2ActualCurrent = Obj.Anodic2ActualCurrent;
            _Anodic2SetCurrent = Obj.Anodic2SetCurrent;
            _Anodic3ActualCurrent = Obj.Anodic3ActualCurrent;
            _Anodic3SetCurrent = Obj.Anodic3SetCurrent;
            

            _AlZn1ActualCurrent = Obj.AlZn1ActualCurrent;
            _AlZn1SetCurrent = Obj.AlZn1SetCurrent;
            _AlZn2ActualCurrent = Obj.AlZn2ActualCurrent;
            _AlZn2SetCurrent = Obj.AlZn2SetCurrent;
            _AlZn3ActualCurrent = Obj.AlZn3ActualCurrent;
            _AlZn3SetCurrent = Obj.AlZn3SetCurrent;
            _AlZn4ActualCurrent = Obj.AlZn4ActualCurrent;
            _AlZn4SetCurrent = Obj.AlZn4SetCurrent;
            _AlZn5ActualCurrent = Obj.AlZn5ActualCurrent;
            _AlZn5SetCurrent = Obj.AlZn5SetCurrent;
            _AlZn6ActualCurrent = Obj.AlZn6ActualCurrent;
            _AlZn6SetCurrent = Obj.AlZn6SetCurrent;
            _AlZn7ActualCurrent = Obj.AlZn7ActualCurrent;
            _AlZn7SetCurrent = Obj.AlZn7SetCurrent;
            _AlZn8ActualCurrent = Obj.AlZn8ActualCurrent;
            _AlZn8SetCurrent = Obj.AlZn8SetCurrent;
            _AlZn9ActualCurrent = Obj.AlZn9ActualCurrent;
            _AlZn9SetCurrent = Obj.AlZn9SetCurrent;
            _AlZn10ActualCurrent = Obj.AlZn10ActualCurrent;
            _AlZn10SetCurrent = Obj.AlZn10SetCurrent;
            _AlZn11ActualCurrent = Obj.AlZn11ActualCurrent;
            _AlZn11SetCurrent = Obj.AlZn11SetCurrent;

            _AlZn12ActualCurrent = Obj.AlZn12ActualCurrent;
            _AlZn12SetCurrent = Obj.AlZn12SetCurrent;
            _AlZn13ActualCurrent = Obj.AlZn13ActualCurrent;
            _AlZn13SetCurrent = Obj.AlZn13SetCurrent;
            _AlZn14ActualCurrent = Obj.AlZn14ActualCurrent;
            _AlZn14SetCurrent = Obj.AlZn14SetCurrent;
            _AlZn15ActualCurrent = Obj.AlZn15ActualCurrent;
            _AlZn15SetCurrent = Obj.AlZn15SetCurrent;
            _AlZn16ActualCurrent = Obj.AlZn16ActualCurrent;
            _AlZn16SetCurrent = Obj.AlZn16SetCurrent;
            _AlZn17ActualCurrent = Obj.AlZn17ActualCurrent;
            _AlZn17SetCurrent = Obj.AlZn17SetCurrent;
            _AlZn18ActualCurrent = Obj.AlZn18ActualCurrent;
            _AlZn18SetCurrent = Obj.AlZn18SetCurrent;

            _DateTimeCol = Obj.DateTimeCol;

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
