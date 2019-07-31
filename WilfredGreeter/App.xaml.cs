using System;
using System.Windows;

namespace WilfredGreeter
{
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            MainWindow mainWindow = e.Args.Length == 1 
                ? new MainWindow(e.Args[0]) 
                : new MainWindow();

            mainWindow.Show();
        }
    }

}
