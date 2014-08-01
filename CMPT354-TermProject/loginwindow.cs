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

    // Public Delegates
    public delegate void delegate_anonymousLogoff_click();
    public delegate void delegate_studentReqistrationLogoff_click();


    public partial class LoginWindow : Form
    {

        //Private Forms
        private AnonymousView anonView;
        private StudentRegistrationView stuRegView;


        // MAIN FUNCTIONS
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void cancelbutton_click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loginbutton_Click(object sender, EventArgs e)
        {
            ModelDataInterface datainterface = new ModelDataInterface();
            string username = textbox_username.Text;
            string typedPassword = textBox_password.Text;
            string realPassword = datainterface.getPassword(username);
            int studentID = datainterface.getStudentID(username);

            if (typedPassword == realPassword)
            {
                stuRegView = new StudentRegistrationView(studentID, username);

                //CONNECTIONS
                stuRegView.signal_studentRegLogoff_click += new delegate_studentReqistrationLogoff_click(studentRegistrationLogoff);
                stuRegView.FormClosed += studentRegistrationLogoff;

                stuRegView.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Password or Username is inccorrect. Please Try again.");
            }
        }

        private void anonymousbutton_click(object sender, EventArgs e)
        {
             anonView = new AnonymousView();
           
            //Connections
            anonView.signal_anonymousLogiff_click += new delegate_anonymousLogoff_click(anonymousLogoff);
            anonView.FormClosed += anonymousLogoff;

            anonView.Show();
            this.Hide();
            
        }



        //********************************************************
        // MY DELEGATE FUNCTIONS

        //ANONYMOUS WINDOW LOGOFF: Event Handler
        private void anonymousLogoff()
        {
            
            anonView.Close();
            this.Show();
        }

        //OVERLOAD: Anonymous Window
        private void anonymousLogoff(Object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        //ANONYMOUS WINDOW LOGOFF: Event Handler
        private void studentRegistrationLogoff()
        {
            stuRegView.Close();
            this.Show();
        }
        //OVERLOAD
        private void studentRegistrationLogoff(Object sender, FormClosedEventArgs e)
        {
            this.Show();
        }


    }
}
