using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace capstone_server
{
    //데이터 구조체 정의
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
        //수소 설비 데이터를 위한 구조체 정의
        public struct SDG_F
        {
            // 하나의 데이터가 아닌 각 기간별 데이터를 모두 담아야 하기 때문에 배열로 선언
            public double[] RMS1_arr;
            public double[] RMS2_arr;
            public double[] RMS3_arr;
            public double[] RMS4_arr;
            public string[] date2;

            //데이터의 행 개수를 파악하기 위해 count 변수 선언
            public int count;

        }
        
        //직접 만든 DAQ의 데이터를 위한 구조체 정의
        public struct DAQ_F
        {
            // 실시간으로 하나의 값을 전달하면 되기 떄문에 일반 double형 변수로 선언
            public double rms;

            //현재 날짜를 전달하기 위한 date 변수 선언
            public string date;


        }
    }
}
