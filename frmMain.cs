using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RandSim
{    
    public partial class frmMain : Form
    {        

        public frmMain()
        {
            InitializeComponent();
        }
        BindingList<Mail> mails;
        Schedule sch;
        DataTable table;
        static String LOG_FILENAME = String.Empty;

        private void btnStart_Click(object sender, EventArgs e)
        {
            sch.timerStart();

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            btnUndo.Enabled = true;

            table.Rows[0][1] = String.Format("{0} - {1}\r\n{2}", DateTime.Now, "START", table.Rows[0][1]);
            table.Rows[1][1] = String.Format("{0} - {1}\r\n{2}", DateTime.Now, "START", table.Rows[1][1]);
            table.Rows[2][1] = String.Format("{0} - {1}\r\n{2}", DateTime.Now, "START", table.Rows[2][1]);

            LogMessageToFile("START");
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            sch.timerStop();

            btnStart.Enabled = true;
            btnStop.Enabled = false;
            btnOpenLog.Enabled = true;

            table.Rows[0][1] = String.Format("{0} - {1}\r\n{2}", DateTime.Now, "STOP", table.Rows[0][1]);
            table.Rows[1][1] = String.Format("{0} - {1}\r\n{2}", DateTime.Now, "STOP", table.Rows[1][1]);
            table.Rows[2][1] = String.Format("{0} - {1}\r\n{2}", DateTime.Now, "STOP", table.Rows[2][1]);

            LogMessageToFile("STOP");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            LOG_FILENAME = Directory.GetCurrentDirectory() + "\\log.txt";

            File.Delete(LOG_FILENAME);

            Mail phone = new PhoneMail("Mail phone");
            Mail house = new MailHouse("Mail house");
            Mail car = new MailCar("Mail car");

            phone.StatusChanged += Phone_StatusChanged;
            house.StatusChanged += House_StatusChanged;
            car.StatusChanged += Car_StatusChanged;

            mails = new BindingList<Mail>();
            mails.Add(phone);
            mails.Add(house);
            mails.Add(car);

            Schedule sch1 = Schedule.getInstance(mails); // create new singleton

            sch = Schedule.getInstance(mails);

            table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Status", typeof(string));
            table.Columns.Add("Count", typeof(string));

            for (int i = 0; i < mails.Count; i++)
            {
                table.Rows.Add(mails[i]);
            }

            dgrTable.DataSource = table;

            dgrTable.RowHeadersVisible = false;
            dgrTable.ColumnHeadersVisible = true;
            dgrTable.AllowUserToAddRows = false;

            dgrTable.Columns[0].Width = (int)(dgrTable.Width * 0.25);// 140;
            dgrTable.Columns[1].Width = (int)(dgrTable.Width * 0.61);
            dgrTable.Columns[2].Width = (int)(dgrTable.Width * 0.1);
            dgrTable.Columns[1].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgrTable.Rows[0].Height = (int)(dgrTable.Height * 0.31);
            dgrTable.Rows[1].Height = (int)(dgrTable.Height * 0.31);
            dgrTable.Rows[2].Height = (int)(dgrTable.Height * 0.31); 
        }

        private void Car_StatusChanged()
        {
            UpdateTable(2);
        }

        private void House_StatusChanged()
        {
            UpdateTable(1);
        }

        private void Phone_StatusChanged()
        {
            UpdateTable(0);
        }

        public void UpdateTable(int firmType)
        {
            switch (firmType)
            {
                case 0://Phone Mail
                    table.Rows[0][1] = String.Format("{0} - {1}\r\n{2}", DateTime.Now, mails[0].status, table.Rows[0][1]);
                    table.Rows[0][2] = mails[0].count;
                    LogMessageToFile(String.Format("{0} - {1}", mails[0].GetType(), mails[0].status));
                    break;
                case 1://house Mail
                    table.Rows[1][1] = String.Format("{0} - {1}\r\n{2}", DateTime.Now, mails[1].status, table.Rows[1][1]);
                    table.Rows[1][2] = mails[1].count;
                    LogMessageToFile(String.Format("{0} - {1}", mails[1].GetType(), mails[1].status));
                    break;
                case 2://Car Mail
                    table.Rows[2][1] = String.Format("{0} - {1}\r\n{2}", DateTime.Now, mails[2].status, table.Rows[2][1]);
                    table.Rows[2][2] = mails[2].count;
                    LogMessageToFile(String.Format("{0} - {1}", mails[2].GetType(), mails[2].status));
                    break;
                default:
                    break;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {            
            this.Close();
        }
        static void LogMessageToFile(string msg)
        {            
            try
            {
                System.IO.FileInfo f = new System.IO.FileInfo(LOG_FILENAME);
                long lLength = f.Length;

                if (lLength > 100 * 1024 * 1024)
                    f.Delete();
            }
            catch { }

            msg = string.Format("{0:G}: {1}\r\n", DateTime.Now, msg);
            System.IO.File.AppendAllText(LOG_FILENAME, msg);
        }

        private void btnOpenLog_Click(object sender, EventArgs e)
        {
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(LOG_FILENAME)
            {
                UseShellExecute = true
            };
            p.Start();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            foreach (Mail f in mails)
            {
                f.count = 0;
                f.status = String.Empty;
                f.bMailIntoProcess = false;
                f.bMailToProcess = false;
            }

            table.Rows[0][1] = String.Empty;
            table.Rows[1][1] = String.Empty;
            table.Rows[2][1] = String.Empty;

            table.Rows[0][2] = String.Empty;
            table.Rows[1][2] = String.Empty;
            table.Rows[2][2] = String.Empty;
        }
    }
}
