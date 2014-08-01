/*TASKS:
 * Create Form: Registration View
 * - All course registartions
 * - checkout
 * - delete/drop
 * - waitlist
 * 
 * Create Delegates 
 * - For RegistrationView
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CMPT354_TermProject
{

    //Delegates
    public delegate void delegate_studentCart_cancelCLick();
    public delegate void delegate_studentCart_checkOutClick(DataTable cartTable);
    public delegate void delegate_currentReg_doneClick();

    public partial class StudentRegistrationView : Form
    {

        // EVENTs
        public event delegate_studentReqistrationLogoff_click signal_studentRegLogoff_click;


        // PRIVATE VARS
        private string studentID;
        private string studentUsername;
        private MainController control;
        private StudentCourseCart studentCart;
        private CurrentCourseRegistration registrationView;
        private DataTable courseTable;
        private DataTable studentRegTable;


        //MAIN FUNCTIONS
        public StudentRegistrationView(int stuID, string username)
        {
            InitializeComponent();
            control = new MainController();
            studentID = stuID.ToString();
            studentUsername = username;
            studentName_label.Text = username;

            //Course Cart Init
            studentCart = new StudentCourseCart(studentID, studentUsername);
                //Connections
                studentCart.signal_cancelClick += new delegate_studentCart_cancelCLick(studentCart_cancelClick);
                studentCart.signal_checkoutClick += new delegate_studentCart_checkOutClick(studentCart_checkOutClick);

            //Registration View Init
            registrationView = new CurrentCourseRegistration(studentID, studentUsername);
                //Connections
                registrationView.signal_doneClick += new delegate_currentReg_doneClick(crouseReg_doneClick);


            //GUI Initialize
            courselist_dataGridView.MultiSelect = false;
            coursenumber_radioButton.Checked = true;
            semester_comboBox.SelectedIndex = 2;
            program_comboBox.SelectedIndex = 0;
            courselevel_comboBox.SelectedIndex = 0;

        }

        private void searchbutton_Click(object sender, EventArgs e)
        {
            string semester = semester_comboBox.Text;
            string program = program_comboBox.Text;
            string courselevel = courselevel_comboBox.Text;
            string courseNumber = coursenumber_numericUpDown.Value.ToString();
            bool allCourse = allcourse_checkBox.Checked;

            //SQL QUERY
            courselist_dataGridView.UseWaitCursor = true;
            if (coursenumber_radioButton.Checked)
            {
                //Course Number
                control.createQueryforCourseNumber(semester, program, courseNumber, allCourse, ref courseTable);
            }
            else
            {
                // Course Level
                control.createQueryforCourseLevel(semester, program, courselevel, allCourse, ref courseTable); 
            }
            courselist_dataGridView.DataSource = courseTable;
            courselist_dataGridView.UseWaitCursor = false;

        }

        //**************************************
        //MY FUNCTIONS
        private void crouseReg_doneClick()
        {
            registrationView.Hide();
        }

        private void studentCart_cancelClick()
        {
            studentCart.Hide();
        }


        //CHECKOUT: STUDENT CART CLICKED
        private void studentCart_checkOutClick(DataTable cartTable)
        {
            ModelDataInterface dataInterface = new ModelDataInterface();
            int courseCount = cartTable.Rows.Count;
            for (int i = 0; i < courseCount; i++)
            {
                string courseID = cartTable.Rows[i].Field<int>("CourseID").ToString();
                //MessageBox.Show("Checkout cIDD: " + courseID);

                int enrolledAmount = dataInterface.getTotalEnrolledCourse(courseID);
                int roomCapacity = dataInterface.getTotalRoomCapacity(courseID);
                int waitList = dataInterface.getTotalEnrolledWaitList(courseID);
               
                if (enrolledAmount < roomCapacity)
                {
                    dataInterface.insertStudentCourseRegistration(studentID, courseID);
                    dataInterface.deleteStudentCourseCartInstance(courseID);
                    dataInterface.updateEnrolledAmount(courseID, enrolledAmount+1);
                }
                else if (waitList < 5)
                {
                    dataInterface.insertStudentCourseWaitList(studentID, courseID);
                    dataInterface.deleteStudentCourseCartInstance(courseID);
                    dataInterface.updateWaitListAmount(courseID, waitList+1);
                    MessageBox.Show("Sorry! CourseID: " + courseID + " is currently full. But you have been added to the wait list.");
                }
                else { MessageBox.Show("Sorry! CourseID: " + courseID + " is currently full. There is currently no wait list open at this time."); }


            }
            
            registrationView.refreshAllTables();
            studentCart_refreshCourseList();
        }//END


        //REFRESH CART
        private void studentCart_refreshCourseList()
        {
            studentCart.refreshCartList();
        }

        //CHECK ALL PREREQS
        // C- = 1.70
        private bool checkAllPreReqs(string courseID)
        {
            bool result = true;
            ModelDataInterface dataInterface = new ModelDataInterface();
            DataTable prereqGPATable;
            prereqGPATable = dataInterface.getGpaPreRequisites(studentID, courseID);

            //MessageBox.Show("The Count is: " + prereqGPATable.Rows.Count);
            int rows = prereqGPATable.Rows.Count;
            if (rows > 0)
            {
                for (int i = 0; i < rows; i++)
                {
                    double gradeinstance = prereqGPATable.Rows[i].Field<double>("CourseGrade");
                    if (gradeinstance < 1.70)
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        //END MY FUCNTIONS
        //**************************************

        private void logout_button_Click(object sender, EventArgs e)
        {
            signal_studentRegLogoff_click();
        }

        //OPEN COURSE CART VIEW
        private void courseRegView_button_Click(object sender, EventArgs e)
        {
            studentCart.Show();
            studentCart_refreshCourseList();
        }

        //*************************
        //ADD COURSE TO CART
        private void addtocourse_button_Click(object sender, EventArgs e)
        {
            if (courselist_dataGridView.SelectedRows.Count > 0)
            {
                ModelDataInterface datainterface = new ModelDataInterface();

                float cumGPA = datainterface.getCumlativeGPA(studentID);
                if (cumGPA >= 2.25)
                {
                    //Retrieve CourseID From Selection
                    int index = courselist_dataGridView.SelectedRows[0].Index;
                    DataGridViewRow selectedRow = courselist_dataGridView.Rows[index];
                    string cID = Convert.ToString(selectedRow.Cells["CourseID"].Value);

                    //IF Already Enrolled or in Cart
                    if (!(datainterface.getIfCurrentlyEnrolled(studentID, cID)))
                    {

                        //Check Prerequisites
                        bool passedPreReq = checkAllPreReqs(cID);
                        if (passedPreReq)
                        {

                            //Collect CreditHours
                            int creditHoursReg = datainterface.getSumCreditHoursRegestration(studentID);
                            int creditHoursCart = datainterface.getSumCreditHoursCourseCart(studentID);
                            int creditHourSelection = Convert.ToInt32(selectedRow.Cells["CreditHours"].Value);

                            if (creditHoursCart + creditHourSelection + creditHoursReg <= 18)
                            {
                                datainterface.insertStuedentCourseCart(studentID, cID);

                                //REFRESH STUDENT CART LIST
                                studentCart_refreshCourseList();
                            }
                            else { MessageBox.Show("Credit Hours Exceed 18 Credit Units. Delete course from Cart, or drop a coruse you are currently registered in."); }

                        } //END Prereq
                        else { MessageBox.Show("The prerequisites for the course have not been satisfied. Please Contact the Academic Administrator for your department"); }
                    }//END IF ENROLLED
                    else { MessageBox.Show("You currently are enrolled/Wiatlist or the course is in your cart. Please check status of course, ID: " + cID); }
                } //END IF cumGPQ< 2.25
                else { MessageBox.Show("Your Cumlative GPA is Below 2.25. Please Contact a Academic Advisor."); }  

            } 
        }//END FUNCTION


        private void currentReg_button_Click(object sender, EventArgs e)
        {
            registrationView.Show();
        }



        //NOT NEEDED
        private void StudentRegistrationView_Load(object sender, EventArgs e)
        {

        }

        private void AnonTitleName_Click(object sender, EventArgs e)
        {

        }

 

       


    }
}
