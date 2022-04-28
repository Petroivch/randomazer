using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandSim
{
    class MailHouse : Mail
    {
        public string mailInto()
        {
            String res;
            bMailIntoProcess = !(bMailIntoProcess);
            if (bMailIntoProcess)
            {
                res = "Mail into house start";
            }
            else
            {
                res = "Mail into house stop";
            }
            this.ChangeStatus(res);
            return status;
        }
        public string mailTo()
        {
            String res;
            bMailToProcess = !(bMailToProcess);
            if (bMailToProcess)
            {
                res = "Mail to house start";
            }
            else
            {
                res = "Mail to house stop";
            }
            this.ChangeStatus(res);
            return status;
        }
        public MailHouse(String name)
        {
            this.name = name;
            this.type = 1;
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
