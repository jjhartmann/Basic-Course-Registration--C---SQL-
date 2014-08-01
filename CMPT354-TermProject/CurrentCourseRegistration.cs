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
    public partial class CurrentCourseRegistration : Form
    {
        //EVENTS
        public event delegate_currentReg_doneClick signal_doneClick;
        //Private Vars
        private string studentID;
        private string studentUsername;

        public CurrentCourseRegistration(string stuID, string username)
        {
            InitializeComponent();
            studentID = stuID;
            studentUsername = username;
            username_label.Text = studentUsername;

            ModelDataInterface dataInterface = new ModelDataInterface();
            registered_dataGridView.DataSource = dataInterface.getStudentCourseRegistrations(studentID);
            waitlist_dataGridView.DataSource = dataInterface.getStudentCourseWaitList(studentID);

        }

        private void refreshSystemCheckforEnrollment()
        {
            ModelDataInterface dataInterface = new ModelDataInterface();
            int rowCount = waitlist_dataGridView.RowCount;
            for (int i = 0; i < rowCount; i++)
            {
                DataGridViewRow selectedRow = waitlist_dataGridView.Rows[i];
                string cID = Convert.ToString(selectedRow.Cells["CourseID"].Value);
                int enrolledAmount = dataInterface.getTotalEnrolledCourse(cID);
                int courseCapacity = dataInterface.getTotalRoomCapacity(cID);

                if (enrolledAmount < courseCapacity)
                {
                    dataInterface.deleteStudentCourseWaitListInstance(cID);
                    dataInterface.insertStudentCourseRegistration(studentID, cID);
                    refreshAllTables();
                }
            }

        }

        public void refreshAllTables()
        {
            ModelDataInterface dataInterface = new ModelDataInterface();
            registered_dataGridView.DataSource = dataInterface.getStudentCourseRegistrations(studentID);
            waitlist_dataGridView.DataSource = dataInterface.getStudentCourseWaitList(studentID);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dropReg_button_Click(object sender, EventArgs e)
        {
            ModelDataInterface dataInterface = new ModelDataInterface();
            int index = registered_dataGridView.SelectedRows[0].Index;
            DataGridViewRow selectedRow = registered_dataGridView.Rows[index];
            string cID = Convert.ToString(selectedRow.Cells["CourseID"].Value);

            dataInterface.deleteStudentCourseRegistrationInstance(cID);
            dataInterface.updateDecrementEnrolledAmount(cID);
            refreshAllTables();
        }

        private void dropWaitList_button_Click(object sender, EventArgs e)
        {
            ModelDataInterface dataInterface = new ModelDataInterface();
            int index = waitlist_dataGridView.SelectedRows[0].Index;
            DataGridViewRow selectedRow = waitlist_dataGridView.Rows[index];
            string cID = Convert.ToString(selectedRow.Cells["CourseID"].Value);

            dataInterface.deleteStudentCourseWaitListInstance(cID);
            dataInterface.updateDecrementWaitList(cID);
            
        }

        private void done_button_Click(object sender, EventArgs e)
        {
            signal_doneClick();
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            refreshSystemCheckforEnrollment();
            refreshAllTables();
        }

        
    }
}
