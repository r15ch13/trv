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

        #region "fields"

        private static SqlCeConnection _connection;
        private static System.Timers.Timer _timer = new System.Timers.Timer(30000);

        #endregion

        #region "connection"

        /// <summary>
        /// Stellt die Verbindung zur Datenbank hinzu
        /// </summary>
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
                _timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_elapsed);
                _timer.Stop();
                _timer.Start();
                return _connection;
            }
        }

        /// <summary>
        /// Schliesst die Verbindung
        /// </summary>
        private static void close()
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        /// <summary>
        /// Schliesst die Verbindung wenn der Timer abgelaufen ist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void timer_elapsed(Object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_connection.State != ConnectionState.Executing)
                close();
            _timer.Stop();
        }

        #endregion

        /// <summary>
        /// SQL Parameter richtig escapen
        /// </summary>
        /// <param name="param">SQL Parameter</param>
        /// <returns>Object-Array</returns>
        private static object[] cleanParams(params object[] param)
        {
            List<object> tmp = new List<object>();

            foreach (object obj in param)
            {
                if (obj is string)
                {
                    string str = Convert.ToString(obj).Replace(@"'", @"''").Replace(@"""", @"""""");
                    tmp.Add(str);
                }
                else
                {
                    tmp.Add(obj);
                }
            }
            return tmp.ToArray();
        }

        #region "executeRow"

        /// <summary>
        /// Gibt nur die erste Zeile als Dictionary zurueck
        /// </summary>
        /// <param name="queryStr">SQL Query</param>
        /// <returns></returns>
        public static Dictionary<string, object> executeRow(string queryStr)
        {
            return executeRow(queryStr, new string[] { }, null);
        }

        /// <summary>
        /// Gibt nur die erste Zeile als Dictionary zurueck
        /// </summary>
        /// <param name="queryStr">SQL Query</param>
        /// <param name="param">SQL Parameter</param>
        /// <returns></returns>
        public static Dictionary<string, object> executeRow(string queryStr, object[] param)
        {
            return executeRow(queryStr, param, null);
        }

        /// <summary>
        /// Gibt nur die erste Zeile als Dictionary zurueck
        /// </summary>
        /// <param name="queryStr">SQL Query</param>
        /// <param name="param">SQL Parameter</param>
        /// <param name="exclusiveConnection">Optionale Datenbankverbindung</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> executeRow(string queryStr, object[] param, SqlCeConnection exclusiveConnection)
        {
            if (queryStr == "") throw new ArgumentNullException();

            // Parameter in Query einbauen
            String str = String.Format(queryStr, cleanParams(param));
#if DEBUG
            Debug.WriteLine(str);
#endif
            Dictionary<string, object> retVal = new Dictionary<string, object>();
            SqlCeDataReader reader = null;
            try
            {
                using (SqlCeCommand com = new SqlCeCommand(str, exclusiveConnection == null ? Connection : exclusiveConnection))
                {
                    com.Prepare();
                    reader = com.ExecuteReader();
                    int c = reader.FieldCount;

                    // Die Daten der ersten Zeile in ein Dictionary speichern
                    if (reader.Read())
                    {
                        for (int i = 0; i < c; i++)
                        {
                            retVal.Add(reader.GetName(i), reader.GetValue(i));
                        }
                    }
                    reader.Close();
                }
            }
            catch (SqlCeException ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return retVal;
        }
        #endregion


        #region "executeScalar"

        /// <summary>
        /// Gibt nur einen Wert zurueck
        /// </summary>
        /// <typeparam name="T">Typ der zurueck gegeben werden soll</typeparam>
        /// <param name="queryStr">SQL Query</param>
        /// <param name="defaultValue">Standardwert der zurueck gegeben wird wenn das Ergebniss NULL oder DBNULL ist</param>
        /// <returns>Objekt vom Typ T</returns>
        public static T executeScalar<T>(string queryStr, T defaultValue)
        {
            return executeScalar<T>(queryStr, defaultValue, null, "");
        }

        /// <summary>
        /// Gibt nur einen Wert zurueck
        /// </summary>
        /// <typeparam name="T">Typ der zurueck gegeben werden soll</typeparam>
        /// <param name="queryStr">SQL Query</param>
        /// <param name="defaultValue">Standardwert der zurueck gegeben wird wenn das Ergebniss NULL oder DBNULL ist</param>
        /// <param name="param">SQL Parameter</param>
        /// <returns>Objekt vom Typ T</returns>
        public static T executeScalar<T>(string queryStr, T defaultValue, params object[] param)
        {
            return executeScalar<T>(queryStr, defaultValue, null, param);
        }

        /// <summary>
        /// Gibt nur einen Wert zurueck
        /// </summary>
        /// <typeparam name="T">Typ der zurueck gegeben werden soll</typeparam>
        /// <param name="queryStr">SQL Query</param>
        /// <param name="defaultValue">Standardwert der zurueck gegeben wird wenn das Ergebniss NULL oder DBNULL ist</param>
        /// <param name="exclusiveConnection">Optionale Datenbankverbindung</param>
        /// <param name="param">SQL Parameter</param>
        /// <returns>Objekt vom Typ T</returns>
        public static T executeScalar<T>(string queryStr, T defaultValue, SqlCeConnection exclusiveConnection, object[] param)
        {
            if (queryStr == "") throw new ArgumentNullException();
            String str = String.Format(queryStr, cleanParams(param));
#if DEBUG
            Debug.WriteLine(str);
#endif
            object retVal = null;
            try
            {
                using (SqlCeCommand com = new SqlCeCommand(str, exclusiveConnection == null ? Connection : exclusiveConnection))
                {
                    com.Prepare();
                    retVal = com.ExecuteScalar();
                }
            }
            catch (SqlCeException ex)
            {
                Debug.WriteLine(ex.Message);
            }

            // Pruefen ob das Ergebnis NULL oder DBNULL ist
            if (null == retVal || DBNull.Value == retVal)
                // Standartwert zurueck geben
                return defaultValue;
            else
                // Ergebnis casten und zurueck geben
                return (T)retVal;
        }

        #endregion

        #region "executeNonQuery"

        /// <summary>
        /// Fuehrt einen SQL Query aus und gibt den Wert von ExecuteNonQuery zurueck
        /// </summary>
        /// <param name="queryStr">SQL Query</param>
        /// <returns>Ergebnis von ExecuteNonQuery()</returns>
        public static int executeNonQuery(string queryStr)
        {
            return executeNonQuery(queryStr, null, "");
        }

        /// <summary>
        /// Fuehrt einen SQL Query aus und gibt den Wert von ExecuteNonQuery zurueck
        /// </summary>
        /// <param name="queryStr">SQL Query</param>
        /// <param name="param">SQL Parameter</param>
        /// <returns>Ergebnis von ExecuteNonQuery()</returns>
        public static int executeNonQuery(string queryStr, params object[] param)
        {
            return executeNonQuery(queryStr, null, param);
        }

        /// <summary>
        /// Fuehrt einen SQL Query aus und gibt den Wert von ExecuteNonQuery zurueck
        /// </summary>
        /// <param name="queryStr">SQL Query</param>
        /// <param name="exclusiveConnection">Optionale Datenbankverbindung</param>
        /// <param name="param">SQL Parameter</param>
        /// <returns>Ergebnis von ExecuteNonQuery()</returns>
        public static int executeNonQuery(string queryStr, SqlCeConnection exclusiveConnection, params object[] param)
        {
            if (queryStr == "") throw new ArgumentNullException();
            String str = String.Format(queryStr, cleanParams(param));
#if DEBUG
            Debug.WriteLine(str);
#endif
            int retVal = 0;
            try
            {
                using (SqlCeCommand com = new SqlCeCommand(str, exclusiveConnection == null ? Connection : exclusiveConnection))
                {
                    com.Prepare();
                    retVal = com.ExecuteNonQuery();
                }
            }
            catch (SqlCeException ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return retVal;
        }

        #endregion

        #region "executeReader"

        /// <summary>
        /// Fuehrt einen SQL Query aus und gibt den SQL Reader zurueck
        /// </summary>
        /// <param name="queryStr">SQL Query</param>
        /// <returns>SQL Reader</returns>
        public static SqlCeDataReader executeReader(string queryStr)
        {
            return executeReader(queryStr, null, "");
        }

        /// <summary>
        /// Fuehrt einen SQL Query aus und gibt den SQL Reader zurueck
        /// </summary>
        /// <param name="queryStr">SQL Query</param>
        /// <param name="param">SQL Parameter</param>
        /// <returns>SQL Reader</returns>
        public static SqlCeDataReader executeReader(string queryStr, params object[] param)
        {
            return executeReader(queryStr, null, param);
        }

        /// <summary>
        /// Fuehrt einen SQL Query aus und gibt den SQL Reader zurueck
        /// </summary>
        /// <param name="queryStr">SQL Query</param>
        /// <param name="exclusiveConnection">Optionale Datenbankverbindung</param>
        /// <param name="param">SQL Parameter</param>
        /// <returns>SQL Reader</returns>
        public static SqlCeDataReader executeReader(string queryStr, SqlCeConnection exclusiveConnection, params object[] param)
        {
            if (queryStr == "") throw new ArgumentNullException();
            String str = String.Format(queryStr, cleanParams(param));
#if DEBUG
            Debug.WriteLine(str);
#endif
            SqlCeDataReader reader = null;
            try
            {
                using (SqlCeCommand com = new SqlCeCommand(str, exclusiveConnection == null ? Connection : exclusiveConnection))
                {
                    com.Prepare();
                    reader = com.ExecuteReader();
                }
            }
            catch (SqlCeException ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return reader;
        }

        #endregion


        #region "kram"

        public static DataSet executeDataset(string queryStr, SqlCeConnection exclusiveConnection)
        {
            DataSet retVal = new DataSet();
            /*
            using (SqlCeCommand com = new SqlCeCommand(queryStr, exclusiveConnection == null ? Connection : exclusiveConnection))
            {
                reader = com.Ex;
                SqlCeDataAdapter adapter = new SqlCeDataAdapter();
            }
            return reader;
            SqlCeDataAdapter adapter = new SqlCeDataAdapter();
            adapter.SelectCommand = (COMMAND_TYPE)cmd;


            adapter.Fill(retVal);
            */
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
                //int klasse = Klasse.GetByBezeichnung(s.Klasse);
                //InsertSchuelerklasse(s.DatenbankId, klasse);
            }

            return s.DatenbankId;
        }

        public static bool InsertSchuelerklasse(int SchuelerID, int KlasseID)
        {
            if (Database.executeScalar<int>(String.Format("SELECT COUNT(ID) FROM Schuelerklasse WHERE SchuelerID = {0} AND KlasseID = {1}", SchuelerID, KlasseID), -1) == 0)
            {
                Database.executeNonQuery(String.Format("INSERT INTO Schuelerklasse (SchuelerID, KlasseID, Datum, Aktiv) VALUES ({0},{1},GETDATE(),1)", SchuelerID, KlasseID));
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

        #endregion
    }
}
