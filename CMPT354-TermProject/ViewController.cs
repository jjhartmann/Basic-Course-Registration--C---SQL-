using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMPT354_TermProject
{
      public delegate void initiateCourseRegForm();
    public class ViewController
    {
        private static ViewController instance;

        private ViewController() { }


        public static ViewController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ViewController();
                }

                return instance;

            }
        }

        public void initiateLoginScreen()
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();



        }

        



    }
}
