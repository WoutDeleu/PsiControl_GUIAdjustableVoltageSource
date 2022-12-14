
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System;
using System.Windows.Media;

namespace AdjustableVoltageSource
{
	public partial class MainWindow
    {
        private string FormatBusdata_pt1()
		{
			string data = (int)BoardFunctions.CONNECT_TO_BUS + ",";
			data += 1 + "," + BoolToInt(IsConnectedToBus_1) + ",";
			data += 2 + "," + BoolToInt(IsConnectedToBus_2) + ",";
			data += 3 + "," + BoolToInt(IsConnectedToBus_3) + ",";
			data += 4 + "," + BoolToInt(IsConnectedToBus_4) + ",";
			data += 5 + "," + BoolToInt(IsConnectedToBus_5) + ",";
			data += 6 + "," + BoolToInt(IsConnectedToBus_6) + ",";
			data += 7 + "," + BoolToInt(IsConnectedToBus_7) + ",";
			data += 8 + "," + BoolToInt(IsConnectedToBus_8);
			data += ";";
			return data;
		}
		private string FormatBusdata_pt2()
		{
			string data = (int)BoardFunctions.CONNECT_TO_BUS + ",";
			data += 9 + "," + BoolToInt(IsConnectedToBus_9) + ",";
			data += 10 + "," + BoolToInt(IsConnectedToBus_10) + ",";
			data += 11 + "," + BoolToInt(IsConnectedToBus_11) + ",";
			data += 12 + "," + BoolToInt(IsConnectedToBus_12) + ",";
			data += 13 + "," + BoolToInt(IsConnectedToBus_13) + ",";
			data += 14 + "," + BoolToInt(IsConnectedToBus_14) + ",";
			data += 15 + "," + BoolToInt(IsConnectedToBus_15) + ",";
			data += 16 + "," + BoolToInt(IsConnectedToBus_16);
			data += ";";
			return data;
		}
		private string FormatGrounddata_pt1()
		{
			string data = (int)BoardFunctions.CONNECT_TO_GROUND + ",";
			data += 1 + "," + BoolToInt(IsConnectedToGround_1) + ",";
			data += 2 + "," + BoolToInt(IsConnectedToGround_2) + ",";
			data += 3 + "," + BoolToInt(IsConnectedToGround_3) + ",";
			data += 4 + "," + BoolToInt(IsConnectedToGround_4) + ",";
			data += 5 + "," + BoolToInt(IsConnectedToGround_5) + ",";
			data += 6 + "," + BoolToInt(IsConnectedToGround_6) + ",";
			data += 7 + "," + BoolToInt(IsConnectedToGround_7) + ",";
			data += 8 + "," + BoolToInt(IsConnectedToGround_8);
			data += ";";
			return data;
		}
		private string FormatGrounddata_pt2()
		{
			string data = (int)BoardFunctions.CONNECT_TO_GROUND + ",";
			data += 9 + "," + BoolToInt(IsConnectedToGround_9) + ",";
			data += 10 + "," + BoolToInt(IsConnectedToGround_10) + ",";
			data += 11 + "," + BoolToInt(IsConnectedToGround_11) + ",";
			data += 12 + "," + BoolToInt(IsConnectedToGround_12) + ",";
			data += 13 + "," + BoolToInt(IsConnectedToGround_13) + ",";
			data += 14 + "," + BoolToInt(IsConnectedToGround_14) + ",";
			data += 15 + "," + BoolToInt(IsConnectedToGround_15) + ",";
			data += 16 + "," + BoolToInt(IsConnectedToGround_16);
			data += ";";
			return data;
		}

		private static int BoolToInt(bool boolean)
		{
			if (boolean) return 1;
			else return 0;
		}
		private static bool IsValidVoltage(string s)
		{
			bool isNumeric = double.TryParse(s, out double voltage);
			if (isNumeric)
			{
				if (voltage > 0.0 && voltage < 30.0)
				{
					return true;
				}
				else return false;
			}
			else
			{
				return false;
			}
		}
        private static bool IsValidCOMPort(string s)
        {
			return Regex.IsMatch(s, "^COM[0-9][0-9]?[0-9]?[0-9]?[0-9]?[0-9]?[0-9]?[0-9]?[0-9]?[0-9]?$");

        }

        public static SolidColorBrush BrushFromHex(string hexColorString)
        {
            return (SolidColorBrush)(new BrushConverter().ConvertFrom(hexColorString));
        }
		
		public void FilterInput(string readSciMessage)
		{
			Debug.WriteLine(readSciMessage);
            string[] strings = readSciMessage.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (String str in strings)
            {
                if (str.Contains("||"))
                {
                    StatusBox_Error = ExtractErrorMessage(str);
                }
                if (str.Contains("##"))
                {
                    StatusBox_Status = ExtractStatusMessage(str);
                }
                if (str.Contains("(("))
                {
                    RegisterBox = ExtractRegistersMessage(str);
                }
            }
        }
    }
}
