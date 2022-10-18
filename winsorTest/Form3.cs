using MediaDevices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winsorTest
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        Form2 form2 = new Form2();
        public Action showAction = null;
        public Action CloseAction = null;
        private void Form3_Load(object sender, EventArgs e)
        {

            //form2.show();
          
            // List<MediaDevice> MediaDeviceList = MediaDevice.GetDevices() as List<MediaDevice>;
            // thread = new Thread(Conneting);
            //thread.Start();
            
        }
        Thread thread = null;
        private static readonly object obj=new object() ;
        bool isConnecting = false;
        
        private void Conneting()
        {
            bool isConnected = false;
            while (true)
            {
               

                lock (obj)
                {
                    try
                    {
                        List<MediaDevice> MediaDeviceList = MediaDevice.GetDevices() as List<MediaDevice>;
                        if (MediaDeviceList == null || MediaDeviceList.Count == 0)
                        {
                            isConnected = false;
                            isConnecting = false;
                            Thread.Sleep(500);
                            continue;
                        }

                        if (isConnected)
                        {
                            isConnecting = false;
                            Thread.Sleep(500);
                            continue;
                        }

                        foreach (var device in MediaDeviceList)
                        {
                            isConnecting = true;
                            device.Disconnect();
                            if (!device.IsConnected)
                                device.Connect();
                            if (!device.IsConnected)
                                return;

                            var path = device.GetDrives();

                            if (path != null)
                            {
                                foreach (var driver in path)
                                {
                                    string sdName = driver.VolumeLabel;
                                }

                                if (path.Count() > 0)
                                {
                                    string full = path[0].VolumeLabel;

                                    var dirs = device.GetDirectories(full);
                                    isConnected = true;
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
            
        }

        private static readonly object obj1 = new object();
        private void button1_Click(object sender, EventArgs e)
        {
            form2.action();

            while (isConnecting)
            {
                Thread.Sleep(300);
            }

            DateTime dt1 = DateTime.Now;

            lock (obj)
            {
                List<MediaDevice> MediaDeviceList = MediaDevice.GetDevices() as List<MediaDevice>;
                if (MediaDeviceList == null || MediaDeviceList.Count == 0) return;

                var device = MediaDeviceList[0];
               

                while (!device.IsConnected)
                {
                    Thread.Sleep(300);
                }

                 device.Disconnect();
                if (!device.IsConnected)
                    device.Connect();
                if (!device.IsConnected)
                    return;

                var path = device.GetDrives();

                if (path != null)
                {
                    foreach (var driver in path)
                    {
                        string sdName = driver.VolumeLabel;
                    }

                    if (path.Count() > 0)
                    {
                        string full = path[0].VolumeLabel;

                        device.Connect();
                        var dirss = device.GetDirectories(full, "林调通|林长制|SunToonGis");
                        var dirs = device.GetDirectories(full);

                        //  var dirs = device.GetDirectories(full).Where(x => ProductFolder.Contains(new DirectoryInfo(x).Name)).ToArray();
                        if (dirs == null) return;
                        foreach (string dic in dirs)
                        {
                            var dirInfo = new DirectoryInfo(dic);
                        }
                    }
                }
            }
            DateTime dt2 = DateTime.Now;
            form2.CloseAction();

            TimeSpan timeSpan = dt2 - dt1;
            string span = timeSpan.TotalMilliseconds.ToString();
            MessageBox.Show(timeSpan.TotalMilliseconds.ToString());
        }

        private void refresh()
        {
            DataTable dataTable = new DataTable();
            DataColumn dataColumn1 = new DataColumn("a");
            DataColumn dataColumn2 = new DataColumn("b");
            dataTable.Columns.Add(dataColumn1);
            dataTable.Columns.Add(dataColumn2);
            for (int i=0;i<100;i++)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = i / 10;
                dataRow[1] = i;
                dataTable.Rows.Add(dataRow);
            }

            var array = dataTable.Rows.Cast<DataRow>().Select(x=>x[0].ToString()).Distinct().ToList();

            var array1 = dataTable.Rows.Cast<DataRow>().Where(x => x[0].ToString() == "").Distinct().ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
             //form2.showSplash();
            form2.CloseAction ();
        }

      

        private void button3_Click(object sender, EventArgs e)
        {
          
            //form2.showSplash();
            form2.action ();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.thread?.Abort();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }
    }
}
