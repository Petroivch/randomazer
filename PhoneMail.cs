using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandSim
{
    class PhoneMail : Mail
    {
        
        public string MailIntoPhone()
        {
            String res;
            bMailIntoProcess = !(bMailIntoProcess);
            if (bMailIntoProcess)
            {
                res = "Mail into phone start";
            }
            else
            {
                res = "Mail into phone stop";
            }
            this.ChangeStatus(res);
            //this.status = String.Format("{0}:  {1}\r\n", DateTime.Now, res);
            return res;
        }
        public string mailToPhone()
        {
            String res;
            bMailToProcess = !(bMailToProcess);
            if (bMailToProcess)
            {
                res = "Mail to phone start";
            }
            else
            {
                res = "Mail to phone stop";
            }
            this.ChangeStatus(res);
            //this.status = String.Format("{0}:  {1}\r\n", DateTime.Now, res);
            return res;
        }

        public PhoneMail(String name)
        {
            this.name = name;
            this.type = 0;
            this.count = 0;
            this.bMailIntoProcess = false;
            this.bMailToProcess = false;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
