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
    class ModelDataInterface
    {
        SqlConnection objConn;

        public ModelDataInterface()
        {
            string sConnectionString = "data source=cypress.csil.sfu.ca;" + "initial catalog=jhartman354;" + "Trusted_Connection=yes;";
            objConn = new SqlConnection(sConnectionString);
        }

        //GET PASSWORD
        public string getPassword(string username)
        {
            string result = null;
            string sSQL = "SELECT Password FROM Student WHERE UserName = '" + username + "'";

            executeSqlString(sSQL, ref result);

            return result;
        }


        //GET STUDENTID
        public int getStudentID(string username)
        {
            int result = 0;
            string sSQL = "SELECT StudentID FROM Student WHERE UserName = '" + username + "'";
            string str = null;

            executeSqlString(sSQL, ref str);
            if (!(Int32.TryParse(str, out result)))
                MessageBox.Show("Fields are empty");

            return result;
        }

        //GET COURSE INFORMATION
        //RETURN: DataTable

        public DataTable getCourses(string semester, string program, string courseNumber)
        {
            DataTable table = new DataTable();
            String sSQL = "SELECT CourseID, CourseName, CourseNumber, Semester, Year, CreditHours FROM Course WHERE CourseNumber = " + courseNumber
                + " AND Semester = '" + semester + "' AND CourseName = '" + program + "' "
                + "AND Year = 2014";

            executeSqlTable(sSQL, ref table);

            return table;
        }

        //getCoursesBounds()
        //RETURN: DataTable
        public DataTable getCoursesBounds(string semester, string program, int lowerBound, int upperBound)
        {
            DataTable table = new DataTable();

            String sSQL = "SELECT CourseID, CourseName, CourseNumber, Semester, Year, CreditHours FROM Course WHERE CourseNumber >= " + lowerBound
                + " AND CourseNumber < " + upperBound + " AND CourseName = '" + program + "' AND Semester = '" + semester + "' "
                + "AND Year = 2014";

            executeSqlTable(sSQL, ref table);

            return table;
        }


        //gerCoursesOpen
        //RETURN: DataTable
        public DataTable getCoursesOpen(string semester, string program, string coursenumber)
        {
            DataTable table = new DataTable();

            String sSQL = "SELECT Course.CourseID, CourseName, CourseNumber, Semester, Year, CreditHours FROM Course " 
                + "FULL OUTER JOIN CourseRoom ON CourseRoom.CourseID = Course.CourseID "
                + "FULL OUTER JOIN Room ON CourseRoom.RoomID = Room.RoomID "
                + "WHERE CourseRoom.EnrolledAmmount < Room.Capacity "
                + "AND CourseNumber = " + coursenumber + "AND CourseName = '" + program + "' "
                + "AND Semester = '" + semester + "' "
                + "AND Year = 2014";

            executeSqlTable(sSQL, ref table);

            return table;

        }

        //gerCoursesOpen
        //RETURN: DataTable
        public DataTable getCoursesOpenBounds(string semester, string program, int lowerbound, int upperbound)
        {
            DataTable table = new DataTable();

            String sSQL = "SELECT Course.CourseID, CourseName, CourseNumber, Semester, Year, CreditHours FROM Course "
                + "FULL OUTER JOIN CourseRoom ON CourseRoom.CourseID = Course.CourseID "
                + "FULL OUTER JOIN Room ON CourseRoom.RoomID = Room.RoomID "
                + "WHERE CourseRoom.EnrolledAmmount < Room.Capacity "
                + "AND CourseNumber >= " + lowerbound 
                +" AND courseNumber < " + upperbound 
                +" AND CourseName = '" + program + "' "
                + "AND Semester = '" + semester + "' "
                + "AND Year = 2014";

            executeSqlTable(sSQL, ref table);

            return table;

        }

        //GET StudentCoruseCart Updates
        //RETRUNS: DataTable
        public DataTable getStudentCourseCart(string studentID)
        {
            DataTable table = new DataTable();
            string sSQL = "SELECT Course.CourseID, CourseName, CourseNumber, Semester, Year, CreditHours FROM Course "
                + "JOIN StudentCourseCart ON StudentCourseCart.CourseID = Course.CourseID "
                + " WHERE StudentCourseCart.StudentID = " + studentID
                + " AND Year = 2014 AND Semester = 'FALL'";

            executeSqlTable(sSQL, ref table);
            return table;

        }

        //GET StudentCourseRegistrations All course for FALL 2014
        //RETRUNS: DataTable
        public DataTable getStudentCourseRegistrations(string studentID)
        {
            DataTable table = new DataTable();
            string sSQL = "SELECT Course.CourseID, CourseName, CourseNumber, Semester, Year, CreditHours FROM Course "
                + "JOIN StudentCourseRegistration ON StudentCourseRegistration.CourseID = Course.CourseID "
                + " WHERE StudentCourseRegistration.StudentID = " + studentID
                + " AND Year = 2014 AND Semester = 'FALL'";

            executeSqlTable(sSQL, ref table);
            return table;

        }

        //GET StudentCourseWaitList All course for FALL 2014
        //RETRUNS: DataTable
        public DataTable getStudentCourseWaitList(string studentID)
        {
            DataTable table = new DataTable();
            string sSQL = "SELECT Course.CourseID, CourseName, CourseNumber, StudentCourseWaitList.TimeStamp FROM Course "
                + "JOIN StudentCourseWaitList ON StudentCourseWaitList.CourseID = Course.CourseID "
                + " WHERE StudentCourseWaitList.StudentID = " + studentID
                + " AND Year = 2014 AND Semester = 'FALL'";

            executeSqlTable(sSQL, ref table);
            return table;

        }

        //GET: Total SUM() of all current courses in the StudentRegestration Table
        //RETURNS: int
        public int getSumCreditHoursRegestration(string studentID)
        {
            int result = 0;
            string sSQL = "SELECT SUM(CreditHours) AS TotalCreaditHours "
                + "FROM Course "
                + "JOIN StudentCourseRegistration ON Course.CourseID = StudentCourseRegistration.CourseID "
                + "JOIN Student ON StudentCourseRegistration.StudentID = Student.StudentID "
                + "WHERE Student.StudentID = " + studentID
                + " AND Semester = 'FALL' AND Year = 2014"; //CHANGE THESE?? - Impliment semeste/year at login

            string str = "";
            executeSqlString(sSQL, ref str);
            
            if (str != "")
            {
                result = Int32.Parse(str);
            }

            return result;
        }

        //GET: Total SUM() of all current courses in the StudentCart Table
        //RETURNS: int
        public int getSumCreditHoursCourseCart(string studentID)
        {
            int result = 0;
            string sSQL = "SELECT SUM(CreditHours) AS TotalCreditHours "
                + "FROM Course "
                + "JOIN StudentCourseCart ON Course.CourseID = StudentCourseCart.CourseID "
                + "JOIN Student ON StudentCourseCart.StudentID = Student.StudentID "
                + "WHERE Student.StudentID = " + studentID
                + " AND Semester = 'FALL' AND Year = 2014"; //CHANGE THESE?? - Impliment semeste/year at login

            string str = "";
            executeSqlString(sSQL, ref str);
            
            if (str != "")
            {
                result = Int32.Parse(str);
            }

            return result;
        }

        //GET: Cumlative GPA FOR all courses NOT LIKE 'FALL' -- Could implement different varibles for Semester???
        //RETURN: Float
        public float getCumlativeGPA(string studentID)
        {
            float result = 0;
            string sSQL = "SELECT AVG(CourseGrade) AS CumlativeGPA "
                + "FROM StudentCourseRegistration "
                + "JOIN Course on Course.CourseID = StudentCourseRegistration.CourseID "
                + "WHERE StudentID = " + studentID
                + "AND (Semester + CONVERT(nvarchar(20), Course.Year)) NOT LIKE 'FALL2014'"; //FIX Dydnamic Update?? FALL2014 var??

            string str = "";
            executeSqlString(sSQL, ref str);
            if (str != "")
            {
                result = float.Parse(str);
            }

            return result;
        }

        //GET: All Prerequisites for StudnetID and CourseID
        //RETURN: DataTable
        public DataTable getGpaPreRequisites(string studnetID, string courseID)
        {
            DataTable table = new DataTable();
            string sSQL = "SELECT CourseGrade FROM StudentCourseRegistration "
                + "JOIN CoursePreReq ON CoursePreReq.CoursePreReqID = StudentCourseRegistration.CourseID "
                + "WHERE CoursePreReq.CourseID = " + courseID
                + " AND StudentCourseRegistration.StudentID = " + studnetID;

            executeSqlTable(sSQL, ref table);
            return table;
        }

        //GET: Total current enrolled Amount for Course
        //RETURN: int
        public int getTotalEnrolledCourse(string courseID)
        {
            int result = 0;
            string sSQL = "SELECT COUNT(StudentID) AS TotalEnrolled FROM StudentCourseRegistration "
                + "WHERE CourseID = " + courseID;
           
            string str = "";
            executeSqlString(sSQL, ref str);
            if (str != "")
                result = Int32.Parse(str);

            updateEnrolledAmount(courseID, result);
            return result;
        }


        //GET: Total current enrolled WaitList
        //RETURN: int
        public int getTotalEnrolledWaitList(string courseID)
        {
            int result = 0;
            string sSQL = "SELECT COUNT(StudentID) AS TotalEnrolled FROM StudentCourseWaitList "
                + "WHERE CourseID = " + courseID;

            string str = "";
            executeSqlString(sSQL, ref str);
            if (str != "")
                result = Int32.Parse(str);

            updateWaitListAmount(courseID, result);
            return result;
        }

        //GET: SUM of all capcity for the course (course can have multiple rooms)
        //RETURN: int
        public int getTotalRoomCapacity(string courseID)
        {
            int result = 0;
            string sSQL = "SELECT SUM(Capacity) FROM Room "
                + "JOIN CourseRoom ON CourseRoom.RoomID = Room.RoomID "
                + "WHERE CourseRoom.CourseID = " + courseID;

            string str = "";
            executeSqlString(sSQL, ref str);
            if (str != "")
                result = Int32.Parse(str);

            return result;
        }

        //GET: If student already enrolled into the course or in cart or waitlist
        //RETURN: BOOL
        public bool getIfCurrentlyEnrolled(string studentID, string courseID)
        {
            bool result = false;
            string sSQL = "SELECT COUNT(*) AS EnrolledAmount FROM StudentCourseRegistration "
                + "FULL JOIN StudentCourseCart ON StudentCourseCart.CourseID = StudentCourseRegistration.CourseID "
                + "FULL JOIN StudentCourseWaitList ON StudentCourseWaitList.CourseID = StudentCourseRegistration.CourseID "
                + "WHERE (StudentCourseRegistration.CourseID = " + courseID
                + " AND StudentCourseRegistration.StudentID = " + studentID + ") "
                + "OR (StudentCourseCart.CourseID = " + courseID + " AND StudentCourseCart.StudentID = " + studentID + ") "
                + "OR (StudentCourseWaitList.CourseID = " + courseID + " AND StudentCourseWaitList.StudentID = " + studentID + ")";

            string str = "";
            int amount = 0;
            executeSqlString(sSQL, ref str);

            if (Int32.TryParse(str, out amount))
            {
                if (amount > 0 )
                    result = true;
            }
            return result;

        }



        //*********************************************************
        // INSERT FUNCTIONS SQL
        //*********************************************************

        public void insertStuedentCourseCart(string studentID, string CourseID)
        {
            string sSQL = "INSERT INTO StudentCourseCart VALUES( " 
                + studentID + ", " + CourseID + ")";

            executeSqlVoid(sSQL);
        }

        public void insertStudentCourseRegistration(string studentID, string courseID)
        {
            string sSQL = "INSERT INTO StudentCourseRegistration(StudentID, CourseID) VALUES("
                + studentID + ", " + courseID + ")";

            executeSqlVoid(sSQL);
        }

        public void insertStudentCourseWaitList(string studentID, string courseID)
        {
            string sSQL = "INSERT INTO StudentCourseWaitList(StudentID, CourseID) VALUES("
                + studentID + ", " + courseID + ")";

            executeSqlVoid(sSQL);
        }

        //*********************************************************
        // DELETE FUNCTIONS SQL
        //*********************************************************
        public void deleteStudentCourseCartInstance(string CourseID)
        {
            string sSQL = "DELETE FROM StudentCourseCart WHERE CourseID = " + CourseID;
            executeSqlVoid(sSQL);
        }

        public void deleteStudentCourseRegistrationInstance(string CourseID)
        {
            string sSQL = "DELETE FROM StudentCourseRegistration WHERE CourseID = " + CourseID;
            executeSqlVoid(sSQL);
        }

        public void deleteStudentCourseWaitListInstance(string CourseID)
        {
            string sSQL = "DELETE FROM StudentCourseWaitList WHERE CourseID = " + CourseID;
            executeSqlVoid(sSQL);
        }



        //**********************************************************
        // UPDATE FUCNTIONS
        //**********************************************************
        public void updateWaitListAmount(string courseID, int amount)
        {
            string sSQL = "UPDATE CourseRoom SET WaitList = " + amount
                + " WHERE CourseID = " + courseID;

            executeSqlVoid(sSQL);
        }

        public void updateEnrolledAmount(string courseID, int amount)
        {
            string sSQL = "UPDATE CourseRoom SET EnrolledAmmount = " + amount
                + " WHERE CourseID = " + courseID;

            executeSqlVoid(sSQL);
        }

        public void updateDecrementWaitList(string courseID)
        {
            string sSQL = "UPDATE CourseRoom "
                + "SET WaitList = (WaitList - 1) "
                + "WHERE CourseID = " + courseID;

            executeSqlVoid(sSQL);
        }

        public void updateDecrementEnrolledAmount(string courseID)
        {
            string sSQL = "UPDATE CourseRoom "
                + "SET EnrolledAmmount = (EnrolledAmmount - 1) "
                + "WHERE CourseID = " + courseID;

            executeSqlVoid(sSQL);
        }

        //**********************************************************
        // PRIVATE HELPER FUCNTIONS
        //**********************************************************
        
        private void executeSqlTable(string sSQL, ref DataTable table)
        {
            try
            {
                objConn.Open();
                SqlCommand objCmd = new SqlCommand(sSQL, objConn);
                SqlDataReader reader = objCmd.ExecuteReader();
                table.Load(reader);
                objConn.Close();
            }
            catch (SqlException sqlEx) { MessageBox.Show("SQL Error: " + sqlEx.Message); }
            catch (System.Exception e) { MessageBox.Show("System Error: " + e.Message); }
        } //END


        private void executeSqlString(string sSQL, ref string str)
        {
            try
            {
                objConn.Open();
                SqlCommand objCCmd = new SqlCommand(sSQL, objConn);
                var myVar = objCCmd.ExecuteScalar();
                if (myVar != null)
                    str = myVar.ToString();

                objConn.Close();
                //   MessageBox.Show("StudentID is: " + result);
            }
            catch (SqlException sqlEx) { MessageBox.Show("SQL Error: " + sqlEx.Message); }
            catch (System.Exception e) { MessageBox.Show("System Error: " + e.Message); }
        }//END

        private void executeSqlVoid(string sSQL)
        {
            try
            {
                objConn.Open();
                SqlCommand objCCmd = new SqlCommand(sSQL, objConn);
                objCCmd.ExecuteNonQuery();
                objConn.Close();
                //   MessageBox.Show("StudentID is: " + result);
            }
            catch (SqlException sqlEx) { MessageBox.Show("SQL Error: " + sqlEx.Message); }
            catch (System.Exception e) { MessageBox.Show("System Error: " + e.Message); }
        }

       



    }//END CLASS
}//END NAMESPACE
