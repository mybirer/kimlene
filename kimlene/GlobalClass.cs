using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data.SqlServerCe;

namespace kimlene
{
    static class GlobalClass
    {
        /* 
        //Programdaki global değişkenler burada tanımlanıyor
        */
        public const string appVersion = "1.0.0";
        public static string dbFilePath = "KMLN.db";
        public static string logFolder = "";
        //public static SqlCeEngine SQLEn;
        public static SqlCeConnection SQLConn;
        public static bool terminateApp = false;
        //public static string loginName;
        //public static string userEnteredPsw;
        //public static string actualPsw;
        public static string fullUserName = "";
        public static short userId = 1;
        public static string connectionString;
        public static string fileName = "KMLN.sdf";
        public static string password = "123456";

        /* 
         * Bu fonksiyon, SQL bağlantısını yapmak içindir. 
        */
        public static void connectDB()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                connectionString = string.Format("DataSource='{0}'; Password='{1}'", fileName, password);
                //SQLEn = new SqlCeEngine(connectionString);
                //SQLEn.CreateDatabase();
                if (!File.Exists(fileName))
                {
                    MessageBox.Show("Veritabanı bulunamadı!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    terminateApp = true;
                    return;
                }
                SQLConn = new SqlCeConnection(connectionString);
                if (SQLConn.State == ConnectionState.Closed)
                {
                    SQLConn.Open();
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                string errorMessage = ex.Message;
                MessageBox.Show("Veritabanına bağlanırken bir hata oluştu! \n\n Hata mesajı: " + errorMessage, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                GlobalClass.terminateApp = true;
            }
            Cursor.Current = Cursors.Default;
        }

        /* 
         * Bu fonksiyon, SQL sorgularını yapmak içindir. 
        */
        public static DataTable executeSqlQuery(string query, SqlCeConnection sqlConn)
        {
            try
            {

                DataTable dt = new DataTable();
                SqlCeCommand sqlCmnd = new SqlCeCommand();
                sqlCmnd.Connection = sqlConn;
                sqlCmnd.CommandType = CommandType.Text;
                sqlCmnd.CommandText = query;
                dt.Load(sqlCmnd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
                throw new Exception("Veritabanı işleminde hata! \n" + errMsg);
            }
        }
        public static DataTable executeSqlQuery(string query, SqlCeConnection sqlConn, string[][] parameters = null)
        {
            try
            {

                DataTable dt = new DataTable();
                SqlCeCommand sqlCmnd = new SqlCeCommand(query, sqlConn);
                for (int j = 0; j < parameters.Length; j += 1)
                {
                    string paramname = parameters[j][0];
                    string paramvalue = parameters[j][1];
                    if (!string.IsNullOrEmpty(paramvalue))
                    {
                        sqlCmnd.Parameters.AddWithValue("@" + paramname, paramvalue);
                    }
                    else
                    {
                        sqlCmnd.Parameters.AddWithValue("@" + paramname, DBNull.Value);
                    }
                }
                dt.Load(sqlCmnd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
                throw new Exception("Veritabanı işleminde hata! \n" + errMsg);
            }
        }
        public static DataTable executeSqlQuery(string query, SqlCeConnection sqlConn, List<string[]> parameters = null)
        {
            try
            {

                DataTable dt = new DataTable();
                SqlCeCommand sqlCmnd = new SqlCeCommand(query, sqlConn);
                for (int j = 0; j < parameters.Count; j += 1)
                {
                    string paramname = parameters[j][0];
                    string paramvalue = parameters[j][1];
                    if (!string.IsNullOrEmpty(paramvalue))
                    {
                        sqlCmnd.Parameters.AddWithValue("@" + paramname, paramvalue);
                    }
                    else
                    {
                        sqlCmnd.Parameters.AddWithValue("@" + paramname, DBNull.Value);
                    }

                }
                dt.Load(sqlCmnd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
                throw new Exception("Veritabanı işleminde hata! \n" + errMsg);
            }
        }
        /*
         * Bu fonksiyon, string olarak gelen passwordun değerini MD5 algoritmasına göre hashleyip geri gönderir.
        */
        public static string hashPassword(string psw)
        {
            StackFrame sf = new StackFrame();
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                System.Security.Cryptography.HashAlgorithm algorithm = System.Security.Cryptography.MD5.Create();
                byte[] hashedPsw = algorithm.ComputeHash(Encoding.UTF8.GetBytes(psw));
                StringBuilder sOutput = new StringBuilder(hashedPsw.Length);
                for (int i = 0; i < hashedPsw.Length - 1; i++)
                    sOutput.Append(hashedPsw[i].ToString(("X2")));
                Cursor.Current = Cursors.Default;
                return sOutput.ToString();
            }
            catch (Exception ex)
            {
                string errMsg = ex.Message;
                Cursor.Current = Cursors.Default;
                throw new Exception(errMsg);
            }
        }
    }
}
