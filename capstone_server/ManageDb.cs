using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static capstone_server.DataStructure;

namespace capstone_server
{
    //DB관리를 위한 클래스 정의
    public class ManageDb
    {


        // 수소설비 DB에 접근하기위한 SelectSDG_period 함수 선언
        // 설비 id, 시작날짜, 종료날짜 를 파라미터로 입력받음
        public static SDG_F SelectSDG_period(string facility_id, string start, string end)
        {
            // [서버 ip, 포트번호, DB이름, 유저이름, 유저비밀번호]의 정보를 이용해 해당 DB에 접근
            using (MySqlConnection connection = new MySqlConnection("Server=localhost;Port=3306;Database=sdg;Uid=root;Pwd=offset01!"))
            {
                //배열에 순차적으로 추가하기위해서 현재 인덱스 번호 변수를 추가한다.
                int seq1 = 0;
                int seq2 = 0;
                int seq3 = 0;
                int seq4 = 0;

                // DB에서 가져온 데이터를 저장하기위한 SDG_F형식의 변수 featrue_data를 선언한다.
                SDG_F feature_data = new SDG_F();

                // 각 배열들을 넉넉한 크기로 초기화 한다.
                feature_data.RMS1_arr = new double[420000];
                feature_data.RMS2_arr = new double[420000];
                feature_data.RMS3_arr = new double[420000];
                feature_data.RMS4_arr = new double[420000];
                feature_data.date2 = new string[420000];



                try//예외 처리 
                {
                    
                    //MySql DB와의 연결을 활성화한다.
                    connection.Open();

                    //DB에 적용할 sql문을 정의한다.
                    string sql = "select fac.id,f.point_id, ft.name, ft.sensor_type, f.value, f.acquired_at " +
                        "from features as f,feature_types as ft,facilities as fac, points as p " +
                        "where f.point_id = p.id " +
                        "and p.facility_id = fac.id " +
                        "and fac.id = " + facility_id + " " +
                        "and f.acquired_at between " + "'" + start + "' " +
                        "and " + "'" + end + "' " +
                        "order by f.acquired_at;";
                    
                    //sql문을 연결을 통해 활성화된 DB에 적용한다.
                    MySqlCommand cmd = new MySqlCommand(sql, connection);

                    //sql문을 실행하고 난 뒤의 데이터를 읽기위한 table 변수를 선언한다.
                    MySqlDataReader table = cmd.ExecuteReader();

                    //테이블의 마지막이 되기전까지 반복한다.
                    while (table.Read())
                    {
                        //테이블의 point_id
                        if ((UInt64)table["point_id"] % 4 == 1)
                        {
                            (feature_data.RMS1_arr)[seq1] = (double)table["value"];
                            (feature_data.date2)[seq1] = string.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(((DateTime)table["acquired_at"]).ToString())); seq1++;
                        }
                        else if ((UInt64)table["point_id"] % 4 == 2)
                        {
                            (feature_data.RMS2_arr)[seq2] = (double)table["value"];
                            seq2++;

                        }
                        else if ((UInt64)table["point_id"] % 4 == 3)
                        {
                            (feature_data.RMS3_arr)[seq3] = (double)table["value"];
                            seq3++;

                        }
                        else if ((UInt64)table["point_id"] % 4 == 0)
                        {
                            (feature_data.RMS4_arr)[seq4] = (double)table["value"];
                            seq4++;

                        }




                    }
                    feature_data.count = seq1;


                    table.Close();
                   
                    return feature_data;



                }
                catch (Exception ex)
                {
                    Console.WriteLine("실패");
                    Console.WriteLine(ex.ToString());

                    return feature_data;
                }

            }
        }



    }
}
