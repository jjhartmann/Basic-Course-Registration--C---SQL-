using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;


namespace CMPT354_TermProject
{
    public delegate void initiateCourseRegForm();
    
    //Private
     

    public class MainController
    {
        private static MainController instance;

        public MainController() 
        { 
            
        }

        // ********************************************
        // Get Coures Info Functions
        public void createQueryforCourseNumber(string semester, string program, string courseNumber, bool allCourse, ref DataTable table)
        {
            ModelDataInterface dataInterface = new ModelDataInterface();

            if (!allCourse)
            { // ONLY OPEN COURSE
                table = dataInterface.getCoursesOpen(semester, program, courseNumber);
            }
            else
            { //ALL COURSES
                table = dataInterface.getCourses(semester, program, courseNumber);
            }
        } //END createQueryforCourseNumber


        public void createQueryforCourseLevel(string semester, string program, string courselevel, bool allCourse, ref DataTable table)
        {
            ModelDataInterface dataInterface = new ModelDataInterface();
           

            //CHECK COMBOBOx
            if (courselevel == "500+ (Graduate)")
            {
                courselevel = "500";
            }

            //DECLARE BOUNDS FOR FUNCTION
            int lowerbound;
            int upperbound;

            if (Int32.TryParse(courselevel, out lowerbound))
            {
                if (lowerbound == 500)
                    upperbound = lowerbound + 500;
                else
                    upperbound = lowerbound + 100;

                //CHECK ALL COURSE CHECKED 
                if (!allCourse)
                {// SELECT ONLY OPEN COURSES
                    table = dataInterface.getCoursesOpenBounds(semester, program, lowerbound, upperbound);
                }
                else
                { //SELECT ALL COURSE
                    table = dataInterface.getCoursesBounds(semester, program, lowerbound, upperbound);
                }  
            }
            else { MessageBox.Show("Parsing Error: conversion string to int"); }



        } //END createQueryforCourseLevel
      

        



    }
}
