using System.Diagnostics;
using System.Windows;

namespace WPABug
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public static void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Debug.WriteLine("stdout " + e.Data);
        }

        public static void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Debug.WriteLine("stderr " + e.Data);
        }
        public MainWindow()
        {
            InitializeComponent();
        }
        public static void StartMe()
        {
            Debug.WriteLine("I was called");
            try
            {
                using (Process myProcess = new Process())
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.StartInfo.FileName = "C:\\Program Files (x86)\\Windows Kits\\10\\Windows Performance Toolkit\\wpa.exe";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.StartInfo.RedirectStandardOutput = true;
                    myProcess.StartInfo.RedirectStandardError = true;
                    myProcess.OutputDataReceived += MainWindow.p_OutputDataReceived;
                    myProcess.ErrorDataReceived += MainWindow.p_ErrorDataReceived;
                    myProcess.StartInfo.ArgumentList.Add("-listplugins");
                    myProcess.Start();
                    myProcess.BeginOutputReadLine();
                    myProcess.BeginErrorReadLine();
                    myProcess.WaitForExit();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
        private void WPACall_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.StartMe();
                Debug.WriteLine("Execution Finished");
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }
        }
    }
}