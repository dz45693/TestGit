using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Security;
using System.Runtime.InteropServices;
using System.Threading;
using System.Configuration;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] urls = ConfigurationManager.AppSettings["url"].Trim().Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
            Console.Title = "重复发送";
            while (true)
            {
                foreach (var item in urls)
                {
                    SendRequest(item);
                }
            }
            Console.ReadLine();

        }
        static bool SendRequest(string url)
        {
            try
            {

                Process p = new Process();
                p.StartInfo.Arguments = url;
                p.StartInfo.FileName = @"C:\Program Files\Internet Explorer\iexplore.exe";
                p.Start();
                Thread.Sleep(1000 * 10);
                p.CloseMainWindow();
                //  p.Close();          
                TerminateProcess(p.Id, 0);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                return false;
            }
        }
        private static void CloseIE()
        {
            Process[] ps = Process.GetProcessesByName("iexplore");
            foreach (Process item in ps)
            {
                try
                {
                    item.CloseMainWindow();
                    item.Close();
                    TerminateProcess(item.Id, 0);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            } Thread.Sleep(1000);
        }
        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        public static extern long TerminateProcess(int handle, int exitCode);
    }
}
