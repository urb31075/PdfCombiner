// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="urb31075">
// All Right Reserved  
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace PdfCombinerDebug
{
    using System;
    using System.Diagnostics;
    using System.ServiceModel;
    using System.ServiceModel.Description;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;

    using PdfCombinerDebug.MyPdfCombineService;

    using PdfCombinerWcfServiceLibrary;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// The util file name.
        /// </summary>
        private const string UtilFileName = @"C:\Windows\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe";

        /// <summary>
        /// The util argument.
        /// </summary>
        private const string UtilArgument = @"C:\GPIREPO\Lotsia\PdfCombiner\PdfCombinerWindowsService\bin\Debug\PdfCombinerWindowsService.exe";


        private const string HostUri = @"http://localhost:47523/PdfCombineService";

        private ServiceHost host;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private static string ConvertCodePage(string str, int strCodePage, int targetCodePage)
        {
            //Encoding.GetEncoding(targetCodePage).GetString(Encoding.GetEncoding(strCodePage).GetBytes(str));
            var bytes = Encoding.GetEncoding(strCodePage).GetBytes(str);
            var res = Encoding.GetEncoding(targetCodePage).GetString(bytes);
            return res; 
        }

        private static string ExecuteOperation(bool unistall)
        {
            try
            {
                var setuper = new Process
                    {
                        StartInfo =
                            {
                                 FileName = UtilFileName,
                                 Arguments = unistall ? @"/u " + UtilArgument : UtilArgument,
                                 RedirectStandardOutput = true,
                                 RedirectStandardError = true,
                                 UseShellExecute = false,
                                 CreateNoWindow = true
                            },
                        EnableRaisingEvents = true
                    };
                    setuper.Start();
                    var output = setuper.StandardOutput.ReadToEnd();
                    setuper.WaitForExit();
                    var outputCodePage = setuper.StandardOutput.CurrentEncoding.CodePage;
                    var targetCodePage = Encoding.GetEncoding("CP866").CodePage;
                    var convertResult = ConvertCodePage(output, outputCodePage, targetCodePage);
                    return convertResult;
            }
            catch (Exception ex)
            {
                return "Error!: " + ex.Message;
            }
        }

        private async Task<bool> ExecuteOperationAsync(bool unistall)
        {
            try
            {
                var convertResult = string.Empty;
                await Task.Run(() => 
                      {
                          var setuper = new Process
                          {
                              StartInfo =
                               {
                                    FileName = UtilFileName,
                                    Arguments = unistall ? @"/u " + UtilArgument : UtilArgument,
                                    RedirectStandardOutput = true,
                                    RedirectStandardError = true,
                                    UseShellExecute = false,
                                    CreateNoWindow = true
                               },
                              EnableRaisingEvents = true
                          };
                          setuper.Start();
                          var output = setuper.StandardOutput.ReadToEnd();
                          setuper.WaitForExit();
                          var outputCodePage = setuper.StandardOutput.CurrentEncoding.CodePage;
                          var targetCodePage = Encoding.GetEncoding("CP866").CodePage;
                          convertResult = ConvertCodePage(output, outputCodePage, targetCodePage);
                      });

                this.InfoListBox.Items.Add(convertResult);

                return true;
            }
            catch (Exception ex)
            {
                this.InfoListBox.Items.Add("Error!: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// The install button click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private async void InstallButtonClick(object sender, RoutedEventArgs e)
        {
            this.InstallButton.IsEnabled = false;
            this.InfoListBox.Items.Add("Start installing! wait...");
            var result = await this.ExecuteOperationAsync(false);
            this.InfoListBox.Items.Add($"Done! Operation result: {result}");
            this.InstallButton.IsEnabled = true;
        }

        /// <summary>
        /// The un install button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private async void UnInstallButtonClick(object sender, RoutedEventArgs e)
        {
            this.UnInstallButton.IsEnabled = false;
            this.InfoListBox.Items.Add("Start uninstalling! wait...");
            var result = await this.ExecuteOperationAsync(true);
            this.InfoListBox.Items.Add($"Operation result {result}");
            this.UnInstallButton.IsEnabled = true;
        }

        private void WsdlButtonClick(object sender, RoutedEventArgs e)
        {
            Process.Start(@"http://localhost:47523/PdfCombineService");
        }

        private void WcfTestClientButtonClick(object sender, RoutedEventArgs e)
        {
            const string WcfTestClient = @"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\WcfTestClient.exe";
            Process.Start(WcfTestClient, HostUri);
        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            this.RunButton.IsEnabled = false;
            this.InfoListBox.Items.Add("Start installing! wait...");
            var result = string.Empty;
            var task = Task.Run(() =>
                     {
                         result = ExecuteOperation(true);
                     });
            task.Wait(20000);
            this.InfoListBox.Items.Add($"Done! Operation result: {result}");
            this.RunButton.IsEnabled = true;
        }

        private void SetHostbuttonClick(object sender, RoutedEventArgs e)
        {
            if (this.host != null)
            {
                return;
            }

            var baseAddress = new Uri(HostUri);
            this.host = new ServiceHost(typeof(PdfCombineService), baseAddress);
             
            var smb = new ServiceMetadataBehavior
                          {
                              HttpGetEnabled = true,
                              MetadataExporter = { PolicyVersion = PolicyVersion.Policy15 }
                          };
            this.host.Description.Behaviors.Add(smb);
            this.host.Open();
             
            this.InfoListBox.Items.Add($"The service is ready at {HostUri}");
        }

        private void ClearHostButtonClick(object sender, RoutedEventArgs e)
        {
            if (this.host == null)
            {
                return;
            }

            this.host.Close();
            this.InfoListBox.Items.Add($"The service is closed at {HostUri}");
        }

        private async void TestServiceButtonClick(object sender, RoutedEventArgs e)
        {
            this.TestServiceButton.IsEnabled = false;
            var client = new PdfCombineServiceClient();
            this.InfoListBox.Items.Add(@"Send reqest");
            var data = await client.GetDataLongTimeAsync(25);
            this.InfoListBox.Items.Add($"Reciwe data {data}");
            client.Close();
            this.TestServiceButton.IsEnabled = true;
        }
    }
}
