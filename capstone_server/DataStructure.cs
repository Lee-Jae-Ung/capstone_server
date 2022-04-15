using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace capstone_server
{
    public class DataStructure
    {
        /*
        public struct Features
        {

            public double Amplitude;
            public double RMS;
            public double High_RMS;
            public double ECU_RMS;
            public double ISO_RMS_speed;
            public double H2S;
            public double NH3;
            public double CH3SH;
            public double CO;
            public double CO2;
            public double CH4;
            public double Temperature;
            public double TGS826;
            public double TGS2603;
            public double TGS2600;
            public double TGS2602;
            public double TGS2610;
            public double TGS2620;
            public double PcbTemp;
            public double C4H6;
            public double WaterLevel;


        };
        */
        public struct SDG_F
        {
            public double RMS_1;
            public double RMS_2;
            public double RMS_3;
            public double RMS_4;
            public string date;

            public double[] RMS1_arr;
            public double[] RMS2_arr;
            public double[] RMS3_arr;
            public double[] RMS4_arr;
            public string date2;
            public int count;


        }
    }
}
