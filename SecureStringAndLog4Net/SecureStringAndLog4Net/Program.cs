using System;
using System.IO;
using System.Net;
using System.Security;

namespace SecureStringAndLog4Net
{
    public class Program
    {
        private static string m_LogFile = @"C:\Users\10056517\Downloads\log\" + "LogFile" + "-" + DateTime.Now.ToString("yyyy-MM-dd", null) + ".log";
        private static int m_LogLevel = 5;
        private static string nullString = "";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static string m_configFilePath4NET = @"C:\Users\10056517\Downloads\Git\RnD\SecureStringAndLog4Net\SecureStringAndLog4Net\log4net.config";

        public enum LOG
        {
            DEBUG = 5,
            INFORMATION = 4,
            WARNING = 3,
            ERROR = 2,
            CATASTROPHIE = 1
        }

        static Program()
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(m_configFilePath4NET));
        }

        static void Main(string[] args)
        {
            WriteToLog(LOG.INFORMATION, "Application Started");
            string test = null;
            Console.Write("Do you want to take input string from user (Y/N):");
            
            if (Console.ReadLine() == "Y")
            {
                WriteToLog(LOG.DEBUG, "User wants to Enter String");
                Console.WriteLine("Enter the string:");
                test = Console.ReadLine();
                Console.WriteLine($"Entered string length is: {test.Length}");
                WriteToLog(LOG.DEBUG, $"Entered string length is: {test.Length}");
            }
            else
            {
                WriteToLog(LOG.WARNING, "You are procedding with null String");
            }

            Console.WriteLine($"String Entered is: {test}");
            WriteToLog(LOG.DEBUG, $"String Entered is: {test}");
            SecureString secureString = new NetworkCredential(string.Empty, test).SecurePassword;
            Console.WriteLine($"secureString string length is: {secureString.Length}");
            WriteToLog(LOG.DEBUG, $"secureString string length is: {secureString.Length}");
            Console.ReadKey();
        }

        public static void WriteToLog(LOG LogLevel, string Message)
        {
            try
            {
                if ((int)m_LogLevel >= (int)LogLevel)
                {
                    lock (nullString)
                    {
                        using (StreamWriter s = File.AppendText(m_LogFile))
                        {

                            string Level = "";
                            if (LogLevel == LOG.CATASTROPHIE)
                            {
                                Level = "CATASTROPHIE";
                                log.Fatal(Message);
                            }
                            else if (LogLevel == LOG.ERROR)
                            {
                                Level = "ERROR";
                                log.Error(Message);
                            }
                            else if (LogLevel == LOG.WARNING)
                            {
                                Level = "WARNING";
                                log.Warn(Message);
                            }
                            else if (LogLevel == LOG.INFORMATION)
                            {
                                Level = "INFORMATION";
                                log.Info(Message);
                            }
                            else if (LogLevel == LOG.DEBUG)
                            {
                                Level = "DEBUG";
                                log.Info(Message);
                            }
                            s.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd:HH:mm:ss", null) + "\t" + Level + "\t" + Message);
                            s.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occured: {ex.Message}");
            }
        }
    }
}
