using System;

namespace SingletonDemo
{
    public sealed class AppSettings
    {
        private static AppSettings? _instance;
        private static readonly object _lock = new object();

        private AppSettings()
        {
            Console.WriteLine("AppSettings instance created.");
        }

        public static AppSettings Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new AppSettings();
                    }

                    return _instance;
                }
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            AppSettings first = AppSettings.Instance;
            AppSettings second = AppSettings.Instance;

            Console.WriteLine(
                ReferenceEquals(first, second)
            ); // True
        }
    }
}