using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace CMPT354_TermProject
{
    class ModelDataInterfasce
    {
        SqlConnection objConn;

        public ModelDataInterfasce()
        {
            string sConnectionString = "data source=cypress.csil.sfu.ca;" + "initial catalog=jhartman354;" + "Trusted_Connection=yes;";
            objConn = new SqlConnection(sConnectionString);
        }

        public string getUsernamePassword(string username)
        {
            string result = null;
            try
            {
                string sSQL = "SELECT Password FROM Student WHERE UserName = '" + username + "'";
                objConn.Open();
                SqlCommand objCCmd = new SqlCommand(sSQL, objConn);
                result = objCCmd.ExecuteScalar().ToString();

                MessageBox.Show("Password is: " + result);
            }
            catch (SqlException sqlEx) { MessageBox.Show("SQL Error: ", sqlEx.Message); }
            catch (System.Exception e) { MessageBox.Show("System Error: ", e.Message); }

            return result;
        }


    }
}
