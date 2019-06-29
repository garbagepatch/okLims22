using okLims.Helpers;
using okLims.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace okLims.Data
{
    public class DA
    {
        public string _ConnectionStrVC { get; set; }

        public DA(string ConnectionStrVC)
        {
            _ConnectionStrVC = ConnectionStrVC;
        }

        private SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(_ConnectionStrVC);
            conn.Open();

            return conn;
        }

        private void CloseConnection(SqlConnection conn)
        {
            conn.Close();
        }

        public List<Request> GetCalendarRequests(string Start, string End)
        {
            List<Request> Requests = new List<Request>();

            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(@"select
                                                            RequestId
                                                            ,MethodName
                                                            ,LaboratoryName
                                                            ,RequesterEmail
                                                            ,Start
                                                            ,End
                                                        from
                                                            [Request]
                                                        where
                                                            Start between @Start and @End", conn)
                {
                    CommandType = CommandType.Text
                })
                {
                    cmd.Parameters.Add("@Start", SqlDbType.VarChar).Value = Start;
                    cmd.Parameters.Add("@End", SqlDbType.VarChar).Value = End;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Requests.Add(new Request()
                            {
                                RequestId = Convert.ToInt32(dr["RequestId"]),
                                MethodName = Convert.ToString(dr["MethodName"]),
                               RequesterEmail = Convert.ToString(dr["RequesterEmail"]),
                                Start = Convert.ToString(dr["Start"]),
                                End = Convert.ToString(dr["End"]),
                                LaboratoryName = Convert.ToString(dr["LaboratoryName"])
                            });
                        }
                    }
                }
            }

            return Requests;
        }

        public string UpdateRequest(Request evt)
        {
            string message = "";
            SqlConnection conn = GetConnection();
            SqlTransaction trans = conn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(@"update
	                                                [Requests]
                                                set
	                                                RequesterEmail=@RequesterEmail
                                                    ,MethodName=@MethodName
	                                                ,Start=@Start
	                                                ,End=@End 
	                                                ,LaboratoryName=@LaboratoryName
                                                where
	                                                RequestId=@RequestId", conn, trans)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.Add("@RequestId", SqlDbType.Int).Value = evt.RequestId;
                cmd.Parameters.Add("@MethodName", SqlDbType.VarChar).Value = evt.MethodName;
                cmd.Parameters.Add("@RequesterEmail", SqlDbType.VarChar).Value = evt.RequesterEmail;
                cmd.Parameters.Add("@Start", SqlDbType.DateTime).Value = evt.Start;
                cmd.Parameters.Add("@End", SqlDbType.DateTime).Value = Helper.ToDBNullOrDefault(evt.End);
        
                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception exp)
            {
                trans.Rollback();
                message = exp.Message;
            }
            finally
            {
                CloseConnection(conn);
            }

            return message;
        }

        public string AddRequest(Request evt, out int RequestId)
        {
            string message = "";
            SqlConnection conn = GetConnection();
            SqlTransaction trans = conn.BeginTransaction();
            RequestId = 0;

            try
            {
                SqlCommand cmd = new SqlCommand(@"insert into [Requests]
                                                (
	                                                MethodName
	                                                ,[RequesterEmail]
	                                                ,Start
	                                                ,End
	                                                ,LaboratoryName
                                                )
                                                values
                                                (
	                                                @MethodName
	                                                ,@RequesterEmail
	                                                ,@Start
	                                                ,@End
	                                                ,@LaboratoryName
                                                );
                                                select scope_identity()", conn, trans)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.Add("@MethodName", SqlDbType.VarChar).Value = evt.MethodName;
                cmd.Parameters.Add("@RequesterEmail", SqlDbType.VarChar).Value = evt.RequesterEmail;
                cmd.Parameters.Add("@Start", SqlDbType.DateTime).Value = evt.Start;
                cmd.Parameters.Add("@End", SqlDbType.DateTime).Value = Helper.ToDBNullOrDefault(evt.End);
                cmd.Parameters.Add("@LaboratoryName", SqlDbType.VarChar).Value = evt.LaboratoryName;

                RequestId = Convert.ToInt32(cmd.ExecuteScalar());

                trans.Commit();
            }
            catch (Exception exp)
            {
                trans.Rollback();
                message = exp.Message;
            }
            finally
            {
                CloseConnection(conn);
            }

            return message;
        }

        public string DeleteRequest(int RequestId)
        {
            string message = "";
            SqlConnection conn = GetConnection();
            SqlTransaction trans = conn.BeginTransaction();

            try
            {
                SqlCommand cmd = new SqlCommand(@"delete from 
	                                                [Requests]
                                                where
	                                                Request_id=@RequestId", conn, trans)
                {
                    CommandType = CommandType.Text
                };
                cmd.Parameters.Add("@RequestId", SqlDbType.Int).Value = RequestId;
                cmd.ExecuteNonQuery();

                trans.Commit();
            }
            catch (Exception exp)
            {
                trans.Rollback();
                message = exp.Message;
            }
            finally
            {
                CloseConnection(conn);
            }

            return message;
        }
    }
}