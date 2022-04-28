using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandSim
{
    class MailCar : Mail
    {
        public string mailIntoCar()
        {
            String res;
            bMailIntoProcess = !(bMailIntoProcess);
            if (bMailIntoProcess)
            {
                res = "Mail into car start";
            }
            else
            {
                res = "Mail into car stop";
            }
            this.ChangeStatus(res);
            return status;
        }
        public string MailToCar()
        {
            String res;
            bMailToProcess = !(bMailToProcess);
            if (bMailToProcess)
            {
                res = "Mail to car start";
            }
            else
            {
                res = "Mail to car stop";
            }
            this.ChangeStatus(res);
            return status;
        }
        public MailCar(String name)
        {
            this.name = name;
            this.type = 2;
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
