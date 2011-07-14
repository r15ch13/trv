using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;

namespace TR_Verwaltung.Model
{
    public class search
    {
        public DataTable searchstr(string inVorname, string inNachname,string inKlasse)//(string inVorname)//, string nachname)
        {
            //string inVorname = "Maik";
            //string inNachname = "";
            //string inKlasse = "";
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Vorname", typeof(string));
            dt.Columns.Add("Nachname", typeof(string));
            dt.Columns.Add("Klasse", typeof(string));
            dt.Columns.Add("Score", typeof(double));

            #region Testdaten
            //Liste der zu durchsuchenden Daten (hier noch hartkodiert)

            dt.Rows.Add(1, "William", "Lasso Paredes");
            dt.Rows.Add(2, "Mohannad", "Mahde Andraus");
            dt.Rows.Add(3, "Steve Miguel", "Ritter Lemus");
            dt.Rows.Add(4, "Ussaama", "Ahmed");
            dt.Rows.Add(5, "Mikail", "Dal");
            dt.Rows.Add(6, "Michalsky", "Tolksdorf");
            dt.Rows.Add(7, "Nadal", "El-Abed");
            dt.Rows.Add(8, "Ebru", "Erdem");
            dt.Rows.Add(9, "Karim", "Faraj");
            dt.Rows.Add(10, "Berrin", "Gürbüz");
            dt.Rows.Add(11, "Zuher", "Kassem");
            dt.Rows.Add(12, "Miriam", "Laubach");
            dt.Rows.Add(13, "Jennifer", "Tolksdorf");
            dt.Rows.Add(14, "Mic", "Tolksdorf");
            dt.Rows.Add(15, "Michael", "Tolksdorf");
            dt.Rows.Add(16, "Mike", "Ruckl");
            dt.Rows.Add(17, "Maikel", "Ruchl");
            dt.Rows.Add(18, "Rachel", "Maibach");
            dt.Rows.Add(19, "Maibach", "Rachel");
            #region IA109
            dt.Rows.Add(20, "Natallia", "Beckerhoff", "IA109");
            dt.Rows.Add(21, "Michael Georg", "Böning", "IA109");
            dt.Rows.Add(22, "Maximilian", "Buttlies", "IA109");
            dt.Rows.Add(23, "Dennis", "Dörr", "IA109");
            dt.Rows.Add(24, "Eugen", "Dremluk", "IA109");
            dt.Rows.Add(25, "Marcel", "Eppel", "IA109");
            dt.Rows.Add(26, "Oliver", "Hepperle", "IA109");
            dt.Rows.Add(27, "Artur", "Janzen", "IA109");
            dt.Rows.Add(28, "Dominik", "Knafelj", "IA109");
            dt.Rows.Add(29, "Richard", "Kuhnt", "IA109");
            dt.Rows.Add(30, "Christian", "Lingscheidt", "IA109");
            dt.Rows.Add(31, "Anton", "Oreskin", "IA109");
            dt.Rows.Add(32, "Adrian", "Sadowski", "IA109");
            dt.Rows.Add(33, "Axel Dino", "Wahlen", "IA109");
            dt.Rows.Add(34, "Christopher-Daniel", "Wandrey", "IA109");
            dt.Rows.Add(35, "Maik", "Ruchel", "IA109");
            dt.Rows.Add(35, "Bertram", "Wolf", "IA109");
            #endregion
            #endregion

            //Start der verarbeitung
            inVorname = inVorname.Trim();
            inNachname = inNachname.Trim();
            inKlasse = inKlasse.Trim();

            if (inVorname != "" || inNachname != "")
            {
                double adweight = 0.8; //Vornamen
                double bcweight = 1.0; //Nachnamen
                double kweight = 0.3; //Klasse

                /* in = eingabeparameter
                 * 
                 * a. in_vorname, vorname
                 * b. in_nachname, nachname 
                 * c. in_vorname, nachname
                 * d. in_nachname, vorname
                 * k. in_klasse, klasse
                 * 
                 * MIN(a+b+k,c+d+k)
                 * Vornamen:  80% (a&d)
                 * Nachname: 100% (b&c)
                 * Klasse:    30% (k)
                 * 
                 * ist kein vorname vorhanden dann a=b und c=d
                 * ist kein nachname vorhanden dann b=a und d=c
                 */

 

                double sa, sb, sc, sd, sk, scombo = 0; //scores

                string cvorname = ""; //current vorname
                string cnachname = ""; //current nachname
                string cklasse = ""; //current klasse

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    /* in = eingabeparameter
                     * 
                     * a. in_vorname, vorname
                     * b. in_nachname, nachname 
                     * c. in_vorname, nachname
                     * d. in_nachname, vorname
                     * 
                     * MIN(a+b,c+d)
                     */
                    cvorname = dt.Rows[i]["Vorname"].ToString();
                    cnachname = dt.Rows[i]["Nachname"].ToString();
                    cklasse = dt.Rows[i]["Klasse"].ToString();

                    sa = EditDistance(inVorname, cvorname) * adweight;
                    sb = EditDistance(inNachname, cnachname) * bcweight;
                    sc = EditDistance(inVorname, cnachname) * bcweight;
                    sd = EditDistance(inNachname, cvorname) * adweight;
                    sk =  EditDistance(inKlasse, cklasse) * kweight;

                    // ist kein vorname vorhanden dann a=b und c=d
                    if (inVorname == "")
                    {
                        sa = sb;
                        sc = sd;
                    }
                    // ist kein nachname vorhanden dann b=a und d=c
                    if (inNachname == "")
                    {
                        sb = sa;
                        sd = sc;
                    }

                    if (inKlasse != "")
                    {
                        scombo = Math.Min(sa + sb + sk, sc + sd + sk);
                    }
                    else
                    {
                        scombo = Math.Min(sa + sb + sk, sc + sd + sk);
                    }

                    dt.Rows[i]["Score"] = scombo;

                    //MessageBox.Show(
                    //    String.Format("OriginalVorname: {0}\nOriginalNachname: {7}\nModifiedVorname: {1}\nModifiedNachname: {8}\nScore A: {2}\nScore B: {3}\nScore C: {4}\nScore D: {5}\n\nScore Combined: {6}"
                    //    , inVorname, cvorname, sa, sb, sc, sd, scombo, inNachname, cnachname));
                }
            }

            return dt;

        }

        public int EditDistance(string original, string modified)
        {
            int len_orig = original.Length;
            int len_diff = modified.Length;
            var matrix = new int[len_orig + 1, len_diff + 1];
            
            for (int i = 0; i <= len_orig; i++)
                matrix[i, 0] = i;
            for (int j = 0; j <= len_diff; j++)
                matrix[0, j] = j;
            for (int i = 1; i <= len_orig; i++)
            {
                for (int j = 1; j <= len_diff; j++)
                {
                    int cost = modified[j - 1] == original[i - 1] ? 0 : 1;
                    var vals = new int[] { matrix[i - 1, j] + 1, matrix[i, j - 1] + 1, matrix[i - 1, j - 1] + cost };
                    matrix[i, j] = ArrMin(vals);
                    
                    if (i > 1 && j > 1 && original[i - 1] == modified[j - 2] && original[i - 2] == modified[j - 1])
                        matrix[i, j] = Math.Min(matrix[i, j], matrix[i - 2, j - 2] + cost);
                }
            }

            //längendifferenz der namen bei der distanz abziehen
            //return matrix[len_orig, len_diff]-(modified.Length>original.Length?(int)(modified.Length-original.Length):original.Length-modified.Length);//matrix[len_orig, len_diff];
            //längendifferenz der namen bei der distanz abziehen
            return matrix[len_orig, len_diff] - (modified.Length > original.Length ? (int)(modified.Length - original.Length)/2 : (original.Length - modified.Length)/2);//matrix[len_orig, len_diff];
        }

        private int ArrMin(int[] vals)
        {
            int min = vals[0];
            foreach (int n in vals)
            {
                if (n < min)
                {
                    min = n;
                }
            }
            return min;
        }
    }

}