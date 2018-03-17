using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashRegister {
    public class Watcher {
        private static string pFolderPath = @"C:\tmp\";
        public static string folderpath {
            get { return pFolderPath; }
            set { if (!string.IsNullOrWhiteSpace(value)) pFolderPath = value; }
        }
        public static void Main() {
            Run();
        }
        //[PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Run() {
            string[] args = System.Environment.GetCommandLineArgs();
            if (args.Length > 0) {
                string argpath = args[1];
                if (argpath.Length > 1 && Directory.Exists(argpath))
                    folderpath = argpath;
            }
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = pFolderPath;
            // Only watch text files after writen to folder
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.txt";
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;

            // Wait for the user to quit the program.
            Console.WriteLine("Press \'q\' to quit the sample.");
            while (Console.Read() != 'q') ;
        }
        // Define the event handlers.
        private static void OnChanged(object source, FileSystemEventArgs e) {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
        }
    }
}
