using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static capstone_server.DataStructure;

namespace capstone_server
{
    
    public class ManageDb
    {
        
        public static void Insert()
        {
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=currentdata;Uid=root;Pwd=offset01!"))
            {
                string insertQuery = "INSERT INTO test(section,name,location,ip) VALUES('2','dev11','busan','203.250.77.245')";
                try//예외 처리
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand(insertQuery, connection);

                    // 만약에 내가처리한 Mysql에 정상적으로 들어갔다면 메세지를 보여주라는 뜻이다
                    if (command.ExecuteNonQuery() == 1)
                    {
                        Console.WriteLine("인서트 성공");
                    }
                    else
                    {
                        Console.WriteLine("인서트 실패");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());
                }

            }
        }
        /*
        public static Features SelectAE(string point_id, string time)
        {

            using (MySqlConnection connection = new MySqlConnection("Server=203.250.77.74;Port=3306;Database=yeop;Uid=yeop;Pwd=Offset01!"))
            {
                Features featrue_data = new Features();
                try//예외 처리 
                {
                    
                    connection.Open();
                    string sql = "SELECT facilities.id as facility_id,facilities.name,facilities.location,points.name,points.sensor_type,acquired_at,value,point_id, features.feature_type_id " +
                        "from features, points, facilities, feature_types " +
                        "where facilities.id = points.facility_id " +
                        "and features.point_id = points.id " +
                        "and features.feature_type_id = feature_types.id " +
                        //"and facilities.id = " + facility_id + " " +
                        "and features.point_id = " + point_id + " " +
                        //"and points.sensor_type = " + "'" + sensor_type + "' " +
                        "and acquired_at = " + "'" + time + "';";

                    //ExecuteReader를 이용하여
                    //연결 모드로 데이타 가져오기
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    MySqlDataReader table = cmd.ExecuteReader();


                    while (table.Read())
                    {
                        //AMPLITUDE
                        if((UInt64)table["feature_type_id"] == 50)
                        {
                            featrue_data.Amplitude = (double)table["value"];
                        }
                        else if((UInt64)table["feature_type_id"] == 51)
                        {
                            featrue_data.RMS = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 60)
                        {
                            featrue_data.High_RMS = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 61)
                        {
                            featrue_data.ECU_RMS = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 62)
                        {
                            featrue_data.ISO_RMS_speed = (double)table["value"];
                        }

                        else if ((UInt64)table["feature_type_id"] == 68)
                        {
                            featrue_data.H2S = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 69)
                        {
                            featrue_data.NH3 = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 70)
                        {
                            featrue_data.CH3SH = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 71)
                        {
                            featrue_data.CO = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 72)
                        {
                            featrue_data.CO2 = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 73)
                        {
                            featrue_data.CH4 = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 74)
                        {
                            featrue_data.Temperature = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 75)
                        {
                            featrue_data.TGS826 = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 76)
                        {
                            featrue_data.TGS2603 = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 77)
                        {
                            featrue_data.TGS2600 = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 78)
                        {
                            featrue_data.TGS2602 = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 79)
                        {
                            featrue_data.TGS2610 = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 80)
                        {
                            featrue_data.TGS2620 = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 81)
                        {
                            featrue_data.PcbTemp = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 82)
                        {
                            featrue_data.C4H6 = (double)table["value"];
                        }
                        else if ((UInt64)table["feature_type_id"] == 83)
                        {
                            featrue_data.WaterLevel = (double)table["value"];
                        }
                    }


                    //Console.WriteLine("{0} {1}", table["No"], table["sig1"]);
                    table.Close();
                    return featrue_data;



                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());
                    
                    return featrue_data;
                }

            }
        }

        */


        public static SDG_F SelectSDG(string point_id,string start, string end)
        {
            int seq1 = 0;
            int seq2 = 0;
            int seq3 = 0;
            int seq4 = 0;

            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=sdg;Uid=root;Pwd=offset01!"))
            {
                SDG_F featrue_data = new SDG_F();


                try//예외 처리 
                {

                    connection.Open();
                    string sql = "select fac.id,f.point_id, f.feature_type_id, ft.name, ft.sensor_type, f.value, f.acquired_at " +
                        "from features as f,feature_types as ft,facilities as fac, points as p " +
                        "where f.point_id = p.id " +
                        "and p.facility_id = fac.id " +
                        "and fac.id = " + point_id + " " +
                        "and f.acquired_at between " + "'" + start + "' " +
                        "and " + "'" + end + "';";
                    //Console.WriteLine(sql);
                    //ExecuteReader를 이용하여
                    //연결 모드로 데이타 가져오기
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    MySqlDataReader table = cmd.ExecuteReader();
                    
                    while (table.Read())
                    {
                        
                        if ((UInt64)table["point_id"] % 4 == 1)
                        {
                            (featrue_data.RMS_1) = (double)table["value"];
                            seq1++;
                        }
                        else if ((UInt64)table["point_id"] % 4 == 2)
                        {
                            (featrue_data.RMS_2) = (double)table["value"];
                            seq2++;

                        }
                        else if ((UInt64)table["point_id"] % 4 == 3)
                        {
                            (featrue_data.RMS_3) = (double)table["value"];
                            seq3++;

                        }
                        else if ((UInt64)table["point_id"] % 4 == 0)
                        {
                            (featrue_data.RMS_4) = (double)table["value"];
                            seq4++;

                        }
                        featrue_data.date = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(((DateTime)table["acquired_at"]).ToString()));




                    }



                    //Console.WriteLine("{0} {1}", table["No"], table["sig1"]);
                    table.Close();
                    seq1 = 0;
                    seq2 = 0;
                    seq3 = 0;
                    seq4 = 0;



                    return featrue_data;



                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());

                    return featrue_data;
                }

            }
        }

        public static SDG_F SelectDAQ_test(string point_id, string time)
        {


            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=currentdata;Uid=root;Pwd=offset01!"))
            {
                SDG_F featrue_data = new SDG_F();


                try//예외 처리 
                {

                    connection.Open();
                    string sql = "select test_feature.signal,test_feature.acquired_at " +
                        "from test_feature " +
                        "where test_feature.acquired_at = " + "'" + time + "';";
                    //Console.WriteLine(sql);
                    //ExecuteReader를 이용하여
                    //연결 모드로 데이타 가져오기
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    MySqlDataReader table = cmd.ExecuteReader();

                    while (table.Read())
                    {
                        //특징 추가
                        featrue_data.RMS_1 = (double)table["signal"];
                        featrue_data.date = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(((DateTime)table["acquired_at"]).ToString()));

                    }



                    //Console.WriteLine("{0} {1}", table["No"], table["sig1"]);
                    table.Close();




                    return featrue_data;



                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());

                    return featrue_data;
                }

            }
        }

        public static SDG_F SelectSDG_day(string facility_id, string start, string end)
        {
            




            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=sdg;Uid=root;Pwd=offset01!"))
            {
                int seq1 = 0;
                int seq2 = 0;
                int seq3 = 0;
                int seq4 = 0;
                SDG_F featrue_data = new SDG_F();

                featrue_data.RMS1_arr = new double[420000];
                featrue_data.RMS2_arr = new double[420000];
                featrue_data.RMS3_arr = new double[420000];
                featrue_data.RMS4_arr = new double[420000];
                featrue_data.date2 = new string[420000];



                try//예외 처리 
                {
                    Console.WriteLine("func exec start date : " + start);
                    Console.WriteLine("func exec start date : " + end);

                    connection.Open();
                    string sql = "select fac.id,f.point_id, ft.name, ft.sensor_type, f.value, f.acquired_at " +
                        "from features as f,feature_types as ft,facilities as fac, points as p " +
                        "where f.point_id = p.id " +
                        "and p.facility_id = fac.id " +
                        "and fac.id = " + facility_id + " " +
                        "and f.acquired_at between " + "'" + start + "' " +
                        "and " + "'" + end + "' " +
                        "order by f.acquired_at;";
                    //Console.WriteLine(sql);
                    //ExecuteReader를 이용하여
                    //연결 모드로 데이타 가져오기
                    MySqlCommand cmd = new MySqlCommand(sql, connection);
                    MySqlDataReader table = cmd.ExecuteReader();


                    while (table.Read())
                    {

                        if ((UInt64)table["point_id"] % 4 == 1)
                        {
                            (featrue_data.RMS1_arr)[seq1] = (double)table["value"];
                            (featrue_data.date2)[seq1] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(((DateTime)table["acquired_at"]).ToString())); seq1++;
                        }
                        else if ((UInt64)table["point_id"] % 4 == 2)
                        {
                            (featrue_data.RMS2_arr)[seq2] = (double)table["value"];
                            seq2++;

                        }
                        else if ((UInt64)table["point_id"] % 4 == 3)
                        {
                            (featrue_data.RMS3_arr)[seq3] = (double)table["value"];
                            seq3++;

                        }
                        else if ((UInt64)table["point_id"] % 4 == 0)
                        {
                            (featrue_data.RMS4_arr)[seq4] = (double)table["value"];
                            seq4++;

                        }
                        //featrue_data.date2 = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(((DateTime)table["acquired_at"]).ToString()));




                    }
                    featrue_data.count = seq1;


                    //Console.WriteLine("{0} {1}", table["No"], table["sig1"]);
                    table.Close();
                   
                    return featrue_data;



                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());

                    return featrue_data;
                }

            }
        }

    }
}
