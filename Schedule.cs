using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandSim
{
    class Schedule
    {
        private static Schedule instance;
        static BindingList<Mail> mails;
        Timer tmrMain;
        String sActions = String.Empty;
        int DifficultRepairRate = 0;
        public Schedule(BindingList<Mail> f)
        {
            mails = f;
            tmrMain = new Timer();
            tmrMain.Tick += new EventHandler(TmrMain_Tick);
        }

        private void TmrMain_Tick(object sender, EventArgs e)
        {
            PhoneMail fPhone = (PhoneMail)mails[0];
            MailHouse fFurn = (MailHouse)mails[1];
            MailCar fCar = (MailCar)mails[2];
            //sTimer = DateTime.Now.ToString();


            Random rnd = new Random();
            int value = rnd.Next();
            if (value % 6 == 0)
            {
                //car Mail
                sActions = fCar.MailToCar();
            }
            else if (value % 6 == 1)
            {
                //house Mail
                sActions = fCar.mailIntoCar();
            }
            else if (value % 6 == 2)
            {
                //house Mail
                sActions = fFurn.mailTo();
            }
            else if (value % 6 == 3)
            {
                //house Mail
                sActions = fFurn.mailInto();
            }
            else if (value % 6 == 4)
            {
                //phone Mail
                sActions = fPhone.MailIntoPhone();
            }
            else
            {
                //phone Mail
                DifficultRepairRate++;
                if (DifficultRepairRate == 2)
                {
                    sActions = fPhone.mailToPhone();
                    DifficultRepairRate = 0;
                }
            }
        }

        public void timerStart()
        {
            tmrMain.Interval = 1000;
            tmrMain.Start();
        }

        public void timerStop()
        {
            tmrMain.Stop();
        }

        public String GetStatus()
        {
            return sActions;
        }
        public void ChangeStatus(string s)
        {
            s = sActions;
        }
        
        public static Schedule getInstance(BindingList<Mail> mails) //singleton
        {
            if (instance == null)
                instance = new Schedule(mails);
            return instance;
        }
    }
}
