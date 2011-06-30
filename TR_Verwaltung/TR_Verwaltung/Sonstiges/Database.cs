using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;

namespace TR_Verwaltung.Sonstiges
{
    public class Database
    {
        private static SqlCeConnection _connection;

        public static SqlCeConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlCeConnection(@"Data Source=trv_db.sdf;Encrypt Database=False;Password=;File Mode=shared read;Persist Security Info=False;");
                    _connection.Open();
                }
                else
                {
                    if (_connection.State != ConnectionState.Open)
                    {
                        _connection.Open();
                    }
                }
                return _connection;
            }
        }

        //public static object Scalar(string statement, object[] parameters)
        //{
        //    SqlCeCommand command = new SqlCeCommand(statement, Connection);
        //    command.Parameters.Add(

        //    return command.ExecuteScalar();
        //}

        public static int CreateTest(SqlCeConnection con, string Statement)
        {
            if (Statement == "")
            {
                Statement = @"";
            }

            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }

            SqlCeCommand cmd = new SqlCeCommand(Statement);
            cmd.Connection = con;
            return cmd.ExecuteNonQuery();
        }


    }
}
