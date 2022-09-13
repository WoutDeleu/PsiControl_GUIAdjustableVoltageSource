using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace AdjustableVoltageSource
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _measuredValue;

        private Visibility selectionVisible;
        public Visibility SelectionVisible 
        {
            get
            {
                return selectionVisible;
            }
            set
            {
                selectionVisible = value;
                OnPropertyChanged("SelectionVisible");
            }
        }

        public string MeasuredValue
        {
            get { return _measuredValue; }
            set
            {
                if (value != _measuredValue)
                {
                    _measuredValue = value;
                    OnPropertyChanged("MeasuredValue");
                }
            }
        }

        private bool handle = true;

        private void selectMeasureFunction_DropDownClosed(object sender, EventArgs e)
        {
            if (handle) Handle();
            handle = true;
        }
        private void selectMeasureFunction_Changed(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle();
        }
        private void Handle()
        {
            switch (SelectMeasureFunction.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "Measure Current":
                    SelectionVisible = Visibility.Collapsed;
                    break;
                default:
                    SelectionVisible = Visibility.Visible;
                    break;
            }
        }
        private void updateMeasureBoxes()
        {
            ch1.IsEnabled = ConnectedToBus_1;
            ch2.IsEnabled = ConnectedToBus_2;
            ch3.IsEnabled = ConnectedToBus_3;
            ch4.IsEnabled = ConnectedToBus_4;
            ch5.IsEnabled = ConnectedToBus_5;
            ch6.IsEnabled = ConnectedToBus_6;
            ch7.IsEnabled = ConnectedToBus_7;
            ch8.IsEnabled = ConnectedToBus_8;
            ch9.IsEnabled = ConnectedToBus_9;
            ch10.IsEnabled = ConnectedToBus_10;
            ch11.IsEnabled = ConnectedToBus_11;
            ch12.IsEnabled = ConnectedToBus_12;
            ch13.IsEnabled = ConnectedToBus_13;
            ch14.IsEnabled = ConnectedToBus_14;
            ch15.IsEnabled = ConnectedToBus_15;
            ch16.IsEnabled = ConnectedToBus_16;
        }
    }
}
