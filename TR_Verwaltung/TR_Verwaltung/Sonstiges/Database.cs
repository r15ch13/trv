using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using System.Diagnostics;
using TR_Verwaltung.Model;

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
                        _connection.Open();
                }
                return _connection;
            }
        }

        private static void close()
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        public static T executeScalar<T>(string queryStr, T defaultValue)
        {
            return executeScalar<T>(queryStr, defaultValue, null);
        }

        public static T executeScalar<T>(string queryStr, T defaultValue, SqlCeConnection exclusiveConnection)
        {
            if (queryStr == "") throw new ArgumentNullException();
            object retVal = null;

            try
            {
                using (SqlCeCommand com = new SqlCeCommand(queryStr, exclusiveConnection == null ? Connection : exclusiveConnection))
                {
                    retVal = com.ExecuteScalar();
                }
            }
            catch (SqlCeException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                close();
            }

            if (null == retVal || DBNull.Value == retVal)
                return defaultValue;
            else
                return (T)retVal;
        }

        public static int executeNonQuery(string queryStr)
        {
            return executeNonQuery(queryStr, null);
        }

        public static int executeNonQuery(string queryStr, SqlCeConnection exclusiveConnection)
        {
            int retVal = 0;

            try
            {
                using (SqlCeCommand com = new SqlCeCommand(queryStr, exclusiveConnection == null ? Connection : exclusiveConnection))
                {
                    retVal = com.ExecuteNonQuery();
                }
            }
            catch (SqlCeException ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                close();
            }

            return retVal;
        }

        public static int InsertSchueler(Schueler s)
        {
            bool existiert = false;
            if (Database.executeScalar<int>(String.Format("SELECT ID FROM Schueler WHERE Vorname = '{0}' AND Nachname = '{1}'", s.Vorname, s.Nachname), -1) != -1)
            {
                existiert = true;
            }

            if (!existiert)
            {
                Database.executeNonQuery(String.Format("INSERT INTO Schueler (Vorname, Nachname) VALUES ('{0}','{1}')", s.Vorname, s.Nachname));
                //Klassenzuordnung
                s.DatenbankId = Database.executeScalar<int>(String.Format("SELECT ID FROM Schueler WHERE Vorname = '{0}' AND Nachname = '{1}'", s.Vorname, s.Nachname), -1);
                int klasse = Klasse.GetIDByBezeichnung(s.Klasse);
                InsertSchuelerklasse(s.DatenbankId, klasse);
            }

            return s.DatenbankId;
        }

        public static bool InsertSchuelerklasse(int SchuelerID, int KlasseID)
        {
            if (Database.executeScalar<int>(String.Format("SELECT COUNT(ID) FROM Schuelerklasse WHERE SchuelerID = {0} AND KlasseID = {1}", SchuelerID, KlasseID), -1) == -1)
            {
                Database.executeNonQuery(String.Format("INSERT INTO Schuelerklasse (SchuelerID, KlasseID,Datum) VALUES ({0},{1},GETDATE())", SchuelerID, KlasseID));
                return true;
            }
            return false;
            
        }
        /*
        public int doSomeInsert(string link, string date)
        {
            int retVal;
            using (SqlCeConnection con = new SqlCeConnection(_connectionString))
            {
                con.Open();
                using (SqlCeCommand com = new SqlCeCommand("INSERT INTO cp_link  (link, date) VALUES (@link, @date )", con))
                {
                    com.Parameters.AddWithValue("@link", link);
                    com.Parameters.AddWithValue("@date", date);
                    retVal = com.ExecuteNonQuery();
                }
            }
            return retVal;
        }
        */
    }
}
