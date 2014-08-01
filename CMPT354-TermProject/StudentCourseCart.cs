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
    public partial class StudentCourseCart : Form
    {

        // EVENTs
        public event  delegate_studentCart_cancelCLick signal_cancelClick;
        public event delegate_studentCart_checkOutClick signal_checkoutClick;

        //PRIVATE Vars
        private string studentID;
        private string studentUsername;
        private DataTable cartTable;


        public StudentCourseCart(string stuID, string username)
        {
            InitializeComponent();
            studentID = stuID;
            studentUsername = username;

            //GUI Init
            username_label.Text = username;

            //Initialize Table
            ModelDataInterface dataInterface = new ModelDataInterface();
            cartTable = dataInterface.getStudentCourseCart(studentID);
            coursecart_dataGridView.DataSource = cartTable;

        }

        //REFRESH LIST IN CART
        public void refreshCartList()
        {
            ModelDataInterface dataInterface = new ModelDataInterface();
            cartTable = dataInterface.getStudentCourseCart(studentID);
            coursecart_dataGridView.DataSource = cartTable;
        }

        private void cancel_button_Click(object sender, EventArgs e)
        {
            signal_cancelClick();
        }

        private void confirmcart_button_Click(object sender, EventArgs e)
        {
            signal_checkoutClick(cartTable);
        }

        private void dropcourse_button_Click(object sender, EventArgs e)
        {
            ModelDataInterface dataInterface = new ModelDataInterface();
            int index = coursecart_dataGridView.SelectedRows[0].Index;
            DataGridViewRow selectedRow = coursecart_dataGridView.Rows[index];
            string cID = Convert.ToString(selectedRow.Cells["CourseID"].Value);

            dataInterface.deleteStudentCourseCartInstance(cID);
            refreshCartList();

        }
    }
}
