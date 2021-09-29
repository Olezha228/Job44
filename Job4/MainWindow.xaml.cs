using Squirrel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Job4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UpdateManager updateManager;

        public MainWindow()
        {
            InitializeComponent();

            GetVersion();

            Loaded += Load;
        }

        public void GetVersion()
        {
            Assembly assemly = Assembly.GetExecutingAssembly();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assemly.Location);

            Dispatcher.Invoke(() =>
            {
                this.Title = $"v{fileVersionInfo.FileVersion}";
                MessageBox.Show($"v{fileVersionInfo.FileVersion}");
            });
        }

        private void Load(object sender, RoutedEventArgs e)
        {
            if (Internet.IsConnected)
            {
                CheckForUpdates();
                return;
            }

            MessageBox.Show("No internet!");
        }

        private async void CheckForUpdates()
        {
            try
            {
                using (var mgr = await UpdateManager.GitHubUpdateManager("https://github.com/Olezha228/Job44"))
                {
                    MessageBox.Show("There is an update!");
                    updateManager = mgr;
                    var release = await mgr.UpdateApp();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message + Environment.NewLine;
                if (ex.InnerException != null)
                    message += ex.InnerException.Message;
                MessageBox.Show(message);
            }
        }
    }
}
