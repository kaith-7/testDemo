using MediaDevices;
using Syncfusion.Windows.Forms.Tools;
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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }
       // MediaDevice device;
        private void Form4_Load(object sender, EventArgs e)
        {
            DeveiceInit();
        }

        private void DeveiceInit()
        {
            Thread thread = new Thread(() =>
            {
                deleteDirInit("1");
                //await Task.Delay(100);
                // deleteDirInit("2");

            });

            thread.Start();

            //Thread thread1 = new Thread(deleteDirInit);

            //thread1.Start("1");



            //Thread thread2 = new Thread(deleteDirInit);

            //thread2.Start("2");
        }
        private  static readonly object obj = "obj";

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Thread thread = new Thread(() =>
                {
                    DateTime dateTime1 = DateTime.Now;
                    lock (obj)
                    {
                        List<MediaDevice> mediaDeviceList = MediaDevice.GetDevices() as List<MediaDevice>;

                        //获取设备，注意，该代码的需求是只有一个USB设备
                        var device = mediaDeviceList.First();
                        bool isc = device.IsConnected;
                        //device.Disconnect();
                        //if (!device.IsConnected)
                        device.GetServices(MediaDeviceServices.Apps);

                            device.Connect();
                        //if (!device.IsConnected)
                        //    return;


                        device.DirectoryExists(@"\内部存储\test\test\test");
                    }
                        //driver.Cancel();
                        //driver.Dispose();
                        //driver.Disconnect();

                        DateTime dateTime2 = DateTime.Now;
                    string toals = (dateTime2 - dateTime1).TotalMilliseconds.ToString();
                    MessageBox.Show(toals, "提示");
                });
                thread.Start();
                
            }
            catch { }
        }
        
        List<string> list = null;

        private void button2_Click(object sender, EventArgs e)
        {

            Thread thread = new Thread(() => { deleteDir(@"\内部存储\test\test\test2"); });

            thread.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // ThreadStart threadStart = new ThreadStart(deleteDir);
            //Thread thread = new Thread(()=> { deleteDir(@"\内部存储\test\test\test1"); });

            //thread.Start();
            progressBarAdv1.Value = 1;
            if (!backgroundWorker1.IsBusy )
                backgroundWorker1.RunWorkerAsync();

            backgroundWorker1.ReportProgress(10);
        }
        private void deleteDirInit(object obj)
        {
            DateTime dateTime1 = DateTime.Now;
            // string path = "";
            lock (obj)
            {
                List<MediaDevice> mediaDeviceList = MediaDevice.GetDevices() as List<MediaDevice>;

                //获取设备，注意，该代码的需求是只有一个USB设备
                var device = mediaDeviceList.First();
               
               // var array = device.GetDirectoryInfo(@"\内部存储\test\test\test2");
                 device.Connect(MediaDeviceAccess.Default , MediaDeviceShare.Delete);
                //device.Disconnect();
                //if (!device.IsConnected)
                   // device.Connect();
                //if (!device.IsConnected)
                //    return;

                //device.Connect(MediaDeviceAccess.Default, MediaDeviceShare.Delete);
                var drivers = device.GetDrives();
                var driver = drivers?[0];
                var path = driver.VolumeLabel;

                var dirs = device.GetDirectories(path);
                //手机
                // device.DeleteDirectory(@"\内部存储\林调通\系统文件\test1");
                //平板
                var PathTest1 = @"\" + driver.VolumeLabel + @"\test\test\test";
                var PathTest2 = @"\" + driver.VolumeLabel + @"\test\test\test1";
                if (obj.ToString() == "1")
                {
                    if (!device.DirectoryExists(PathTest1))
                        device.CreateDirectory(PathTest1);
                    device.DeleteDirectory(PathTest1, true);
                }
                else if (obj.ToString() == "2")
                {
                    if (!device.DirectoryExists(PathTest2))
                        device.CreateDirectory(PathTest2);
                    device.DeleteDirectory(PathTest2, true);
                }
                device.DirectoryExists(@"\test\test\test1");
            }
            

            //  driver.ResetDevice();
            //driver.Cancel();
            //driver.Dispose();
            //driver.Disconnect();
            DateTime dateTime2 = DateTime.Now;
            string toals = (dateTime2 - dateTime1).TotalMilliseconds.ToString();
            //MessageBox.Show(toals, "提示");
        }
        private void deleteDir(string   path)
        {
            DateTime dateTime1 = DateTime.Now;

            // string path = "";
            lock (obj)
            {
                List<MediaDevice> mediaDeviceList = MediaDevice.GetDevices() as List<MediaDevice>;

                //获取设备，注意，该代码的需求是只有一个USB设备
                var device = mediaDeviceList.First();
                
                device.Disconnect();
                if (!device.IsConnected)
                    device.Connect(MediaDeviceAccess.Default, MediaDeviceShare.Delete);
                if (!device.IsConnected)
                    return;

                // device.Connect(MediaDeviceAccess.Default, MediaDeviceShare.Delete );
                //手机
                // device.DeleteDirectory(@"\内部存储\林调通\系统文件\test1");
                //平板
                if (!device.DirectoryExists(path))
                    device.CreateDirectory(path);
                device.DeleteDirectory(path, true);
            }
            //  device.ResetDevice();
            //device.Cancel();
            //device.Dispose();
            //device.Disconnect();
            DateTime dateTime2 = DateTime.Now;
            string toals = (dateTime2 - dateTime1).TotalMilliseconds.ToString();
            //MessageBox.Show(toals, "提示");
        }

        private void pD()
        {
            PortableDevices.PortableDeviceCollection devices = new PortableDevices.PortableDeviceCollection();
            devices.Refresh();
            if (devices.Count == 0 || devices == null) return;
            PortableDevices.PortableDevice devic = devices.First();
            devic.Connect();
            string name = devic.FriendlyName;

            var folders = devic.GetContents();
            PortableDevices.PortableDevice device = new PortableDevices.PortableDevice(devices[0].DeviceId);
        }

        private void MTP()
        {
            WpdMtpLib.MtpCommand mtp = new WpdMtpLib.MtpCommand();
            string[] ids = mtp.GetDeviceIds();
            string deveiceId = ids[0];
            mtp.Open(deveiceId);
            uint[] dd = new uint[] { };
            var response = mtp.Execute(WpdMtpLib.MtpOperationCode.GetDeviceInfo, dd);
            // var response1 = mtp.Execute(WpdMtpLib.MtpOperationCode.GetThumb , dd);
            WpdMtpLib.DeviceInfo device = new WpdMtpLib.DeviceInfo(response.Data);
            //uint[] dds = new uint[5];
            //var responses = mtp.Execute(WpdMtpLib.MtpOperationCode.GetStorageIDs , dds);

            // WpdMtpLib.StorageInfo  info = new WpdMtpLib.StorageInfo(responses.Data);

            uint[] dds = new uint[5];
            var responses = mtp.Execute(WpdMtpLib.MtpOperationCode.GetDevicePropValue, dds);

            WpdMtpLib.ObjectInfo info = new WpdMtpLib.ObjectInfo(responses.Data);
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
           // Thread.Sleep(2000);
            deleteDir(@"\内部存储\test\test\test3");
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {  
            progressBarAdv1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                Thread thread = new Thread(() => {
                    DateTime dateTime1 = DateTime.Now;
                    lock (obj)
                    {
                        
                        List<MediaDevice> mediaDeviceList = MediaDevice.GetDevices() as List<MediaDevice>;

                        //获取设备，注意，该代码的需求是只有一个USB设备
                        var device = mediaDeviceList.First();

                        device.Disconnect();
                        if (!device.IsConnected)
                            device.Connect();
                        if (!device.IsConnected)
                            return;

                        device.CreateDirectory(@"\内部存储\test\test\test5");
                        //driver.Cancel();
                        //driver.Dispose();
                        //driver.Disconnect();
                    }
                DateTime dateTime2 = DateTime.Now;
                string toals = (dateTime2 - dateTime1).TotalMilliseconds.ToString();
                MessageBox.Show(toals, "提示");
                });
                thread.Start();
            }
            catch { }
        }
        private static readonly object objs;

        private void button5_Click(object sender, EventArgs e)
        {
             Guid EVENT_NOTIFICATION = new Guid(0x2ba2e40a, 0x6b4c, 0x4295, 0xbb, 0x43, 0x26, 0x32, 0x2b, 0x99, 0xae, 0xb2);
            try
            {
                Thread thread = new Thread(() =>
                {
                    DateTime dateTime1 = DateTime.Now;

                    lock (obj)
                    {
                        List<MediaDevice> mediaDeviceList = MediaDevice.GetDevices() as List<MediaDevice>;

                        //获取设备，注意，该代码的需求是只有一个USB设备
                        var device = mediaDeviceList.First();
                       int hashCode=   device.GetHashCode();
                        device.Disconnect();
                        if (!device.IsConnected)
                            device.Connect( MediaDeviceAccess.Default, MediaDeviceShare.Delete );
                        if (!device.IsConnected)
                            return;

                        device.GetDirectories(@"\内部存储");
                    }
                    //driver.Cancel();
                    //driver.Dispose();
                    //driver.Disconnect();

                    DateTime dateTime2 = DateTime.Now;
                    string toals = (dateTime2 - dateTime1).TotalMilliseconds.ToString();
                    MessageBox.Show(toals, "提示");
                });
                thread.Start();
            }
            catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var node = treeViewAdv1.Nodes.Cast<TreeNodeAdv>() . Where(n=>n.Text=="Node1").FirstOrDefault();
            treeViewAdv1.Nodes.Remove(node);
            treeViewAdv1.Refresh();
        }
    }
}
