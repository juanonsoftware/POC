using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace InvokeWebRequest
{
    class Program
    {
        private static readonly string UrlFile = @"D:\Wip\T1-Cloud\Working\MonitoringSites\UrlList.txt";
        private static IDictionary<string, string> Result = new ConcurrentDictionary<string, string>();

        static Thread RunBrowserThread(Uri url)
        {
            var th = new Thread(() =>
            {
                var br = new WebBrowser();
                br.Navigate(url);
                br.ScriptErrorsSuppressed = true;
                br.Visible = false;
                br.DocumentCompleted += browser_DocumentCompleted;

                Application.Run();
            });

            th.SetApartmentState(ApartmentState.STA);
            th.Start();
            return th;
        }

        static void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            var br = (WebBrowser)sender;

            if (br.Url == e.Url)
            {
                Console.WriteLine("Natigated to {0}", e.Url);

                Result.Add(e.Url.ToString(), br.StatusText);

                Application.ExitThread();   // Stops the thread
            }
        }

        static void Main(string[] args)
        {
            try
            {
                var urls = File.ReadAllLines(UrlFile);


                foreach (var url in urls)
                {
                    var rCount = Result.Count;

                    var t = RunBrowserThread(new Uri(url));
                    t.Join();
                }

                var wc = new WebClient();
                wc.Headers.Clear();

                wc.Headers.Add("Cookie", "dtCookie=1$B5DB4D980D43C13468BE524BEA029597");
                wc.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36");
                wc.Headers.Add("Cache-Control", "max-age=0");
                wc.Headers.Add("Cache-Control", "max-age=0");
                wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");

                wc.UseDefaultCredentials = true;

                var s = wc.DownloadString("https://www.traceonenetwork.com/activity");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadLine();
        }
    }
}
