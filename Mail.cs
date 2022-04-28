using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandSim
{
    delegate void MyDelegate();

    class Mail
    {
        public string name { get; set; }
        public int type { get; set; }
        public int count { get; set; }

        public bool bMailIntoProcess { get; set; }
        public bool bMailToProcess { get; set; }
        public String status;
        public static void typeOfWork(){}
        public event MyDelegate StatusChanged;  

        public override string ToString()
        {
            return name;
        }

        public void ChangeStatus(string str)
        {
            this.status = str;
            if (StatusChanged != null)
            {
                this.count += 1;
                StatusChanged();
            }  
        }

    }
}
