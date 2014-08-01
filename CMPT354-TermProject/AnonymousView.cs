/* NOTES: 
 * Unhandeld exceptions: When the comboboxes are out of Bounds
 * 
*/
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



    public partial class AnonymousView : Form
    {
        // EVENTs
        public event delegate_anonymousLogoff_click signal_anonymousLogiff_click;

        //Private Vars
        private DataTable table;
        private MainController control;

        public AnonymousView()
        {
            InitializeComponent();
            control = new MainController();

            //GUI INIT
            courselist_dataGridView.MultiSelect = false;
            coursenumber_radioButton.Checked = true;
            semester_comboBox.SelectedIndex = 2;
            program_comboBox.SelectedIndex = 0;
            courselevel_comboBox.SelectedIndex = 0;
        }

        //SEARCH BUTTON
        private void searchButton_Click(object sender, EventArgs e)
        {
            string semester = semester_comboBox.Text;
            string program = program_comboBox.Text;
            string courselevel = courselevel_comboBox.Text;
            string courseNumber = coursenumber_numericUpDown.Value.ToString();
            bool allCourse = allcourse_checkBox.Checked;
            
            if (coursenumber_radioButton.Checked)
            {
                //Course Number
                courselist_dataGridView.UseWaitCursor = true;
                control.createQueryforCourseNumber(semester, program, courseNumber, allCourse, ref table);
                courselist_dataGridView.DataSource = table;
                courselist_dataGridView.UseWaitCursor = false;
            }
            else
            {
                // Course Level
                courselist_dataGridView.UseWaitCursor = true;
                control.createQueryforCourseLevel(semester, program, courselevel, allCourse, ref table);
                courselist_dataGridView.DataSource = table;
                courselist_dataGridView.UseWaitCursor = false;
            }
        }

        private void logout_button_Click(object sender, EventArgs e)
        {  //Event
            signal_anonymousLogiff_click();
        }

        //IGNOR
        private void AnonTitleName_Click(object sender, EventArgs e)
        {
        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void AnonymousView_Load(object sender, EventArgs e)
        {
        }
        //END IGNOR


  





    } // END CLASS DEF


} // END NAMESPACE DEF
