using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Utils.Win32;

namespace Zipec
{
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    public partial class MainForm : Form
    {
        private readonly SynchronizationContext originalContext;

        public delegate void OutMessageFunction(string message);

        public MainForm()
        {
            this.InitializeComponent();
            this.originalContext = SynchronizationContext.Current;
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            this.InfoListBox.Items.Clear();
        }

        private async void StartButtonClick(object sender, EventArgs e)
        {
            this.InfoListBox.Items.Clear();
            this.InfoListBox.Items.Add("Start!");

            var folderBrowser = new FolderBrowserDialog();

            if (folderBrowser.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var dirsStack = new Stack<string>();
            dirsStack.Push(folderBrowser.SelectedPath);
            var unzipCount = 0;
            var errorCount = 0;
            
            while (dirsStack.Any())
              {
                  var currentDir = dirsStack.Pop();

                  var filesList = Win32Filesystem.GetFiles(currentDir).ToList(); 
                  var zipFilesList = filesList.Where(c => c.EndsWith(".zip")).ToList();

                  foreach (var fileName in zipFilesList)
                  {
                      var fullName = Path.Combine(currentDir, fileName);
                      var nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                      if (nameWithoutExtension == null)
                      {
                          continue;
                      }

                      var destinationDirectoryName = Path.Combine(currentDir, nameWithoutExtension);
                      try
                      {
                          var result = await this.UnRarExecute(fullName, destinationDirectoryName, this.OutMessage);
                          if (!result)
                          {
                              this.ErrorListBox.Items.Add($"Error WinRar : {fullName}");
                          }
                          result = Win32Filesystem.RemoveFile(fullName);
                          if (!result)
                          {
                              this.ErrorListBox.Items.Add($"Error RemoveFile : {fullName}");
                          }
                      }
                      catch (Exception ex)
                      {
                          this.ErrorLabel.Text = $"{++errorCount}";
                          this.ErrorLabel.Refresh();
                          this.ErrorListBox.Items.Add($"Ошибка! {ex.Message}");
                          this.ErrorListBox.Items.Add($"        {fullName}");
                          this.ErrorListBox.Refresh();
                      }

                      this.ProcessLabel.Text = $"{++unzipCount} {fullName}";
                      this.ProcessLabel.Refresh();
                  }

              var dirList = Win32Filesystem.GetAllDirectories(currentDir).ToList();
              dirList.ForEach(dirsStack.Push);
            }
               
            this.InfoListBox.Items.Add("Finish!");
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            var savefile = new SaveFileDialog
                {
                    FileName = "ZipecLog.txt",
                    Filter = @"Text files (*.txt)|*.txt|All files (*.*)|*.*"
                };

            if (savefile.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            foreach (var item in this.ErrorListBox.Items)
            {
                File.AppendAllText(savefile.FileName, item.ToString());
            }
        }

        private Task<bool> UnRarExecute(string zipFile, string extdir, OutMessageFunction outMessage)
        {
            //http://acritum.com/software/manuals/winrar/
            var cli = $"WinRar.exe x -ibck -o+ \"{zipFile}\" *.* \"{extdir}\\\"";
            outMessage?.Invoke(cli);
            var t = Task.Run(
                () =>
                    {
                        try
                        {
                            var process = new Process
                                {
                                    StartInfo =
                                        {
                                            FileName = "C:\\Program Files (x86)\\WinRAR\\WinRar.exe",
                                            Arguments = $"x -ibck -o+ \"{zipFile}\" *.* \"{extdir}\\\"",
                                            //FileName = "unzip.exe", //Для zip.exe
                                            //Arguments = $"-o \"{zipFile}\" -d \"{extdir}\"" //Для zip.exe
                                            RedirectStandardOutput = true,
                                            RedirectStandardError = true,
                                            UseShellExecute = false,
                                            CreateNoWindow = true
                                        },
                                    EnableRaisingEvents = true
                                };

                            var targetCodePage = Encoding.GetEncoding("CP866").CodePage;
                            process.Start();
                            var outputCodePage = process.StandardOutput.CurrentEncoding.CodePage;

                            var startTime = DateTime.Now;

                            while (true)
                            {
                                var output = process.StandardOutput.ReadLine();
                                if (output != null)
                                {
                                    var convertResult = ConvertCodePage(output, outputCodePage, targetCodePage);
                                    outMessage?.Invoke(convertResult);
                                }

                            if (process.HasExited)
                                {
                                    outMessage?.Invoke($"ExitCode: {process.ExitCode}");
                                    break;
                                }

                                if (DateTime.Now - startTime > TimeSpan.FromSeconds(120))
                                {
                                    outMessage?.Invoke("Exit by timeout!");
                                    break;
                                }
                            }

                            process.Close();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            outMessage?.Invoke("Error!: " + ex.Message);
                            outMessage?.Invoke("Error!: " + ex.StackTrace);
                            return false;
                        }
                    });
            return t;
        }

        private void OutMessage(string message)
        {
            this.originalContext.Post(c => { this.InfoListBox.Items.Add(c.ToString()); }, message);
        }

        private static string ConvertCodePage(string str, int strCodePage, int targetCodePage)
        {
            //Encoding.GetEncoding(targetCodePage).GetString(Encoding.GetEncoding(strCodePage).GetBytes(str));
            var bytes = Encoding.GetEncoding(strCodePage).GetBytes(str);
            var res = Encoding.GetEncoding(targetCodePage).GetString(bytes);
            return res;
        }
    }
}
