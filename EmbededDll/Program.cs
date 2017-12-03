using MyDomain;
using System;
using System.Reflection;

namespace EmbededDll
{
    class Program
    {
        static Program()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        static void Main(string[] args)
        {
            var newKey = new KeyService().GetNewKey();
            Console.WriteLine("New key = " + newKey);
            Console.ReadLine();
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var resName = string.Empty;
            if (args.Name.Contains("MyDomain"))
            {
                resName = "EmbededDll.Libs.MyDomain.dll";
            }

            if (string.IsNullOrWhiteSpace(resName))
            {
                return null;
            }

            var res = typeof(Program).Assembly.GetManifestResourceStream(resName);
            var bytes = new byte[res.Length];
            res.Read(bytes, 0, (int)res.Length);
            return Assembly.Load(bytes);
        }
    }
}
