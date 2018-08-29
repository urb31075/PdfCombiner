namespace PdfCombiner
{
    using System.Configuration.Install;
    using System.ServiceProcess;

    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// The pdf combiner service process installer.
        /// </summary>
        private System.ServiceProcess.ServiceProcessInstaller PdfCombinerServiceProcessInstaller;

        /// <summary>
        /// The pdf conbiner service installer.
        /// </summary>
        private System.ServiceProcess.ServiceInstaller PdfConbinerServiceInstaller;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PdfCombinerServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.PdfConbinerServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // PdfCombinerServiceProcessInstaller
            // 
            this.PdfCombinerServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.PdfCombinerServiceProcessInstaller.Password = null;
            this.PdfCombinerServiceProcessInstaller.Username = null;
            // 
            // PdfConbinerServiceInstaller
            // 
            this.PdfConbinerServiceInstaller.Description = "Сервис формирования интерактивного PDF";
            this.PdfConbinerServiceInstaller.DisplayName = "PdfCombiner v1.0";
            this.PdfConbinerServiceInstaller.ServiceName = "PdfCombiner";
            this.PdfConbinerServiceInstaller.StartType = ServiceStartMode.Automatic;
            this.PdfConbinerServiceInstaller.AfterInstall += ServiceInstaller_AfterInstall;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new Installer[] 
            {
                this.PdfCombinerServiceProcessInstaller,
                this.PdfConbinerServiceInstaller
            });
        }

        private void ServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            ServiceController sc = new ServiceController("PdfCombiner");
            sc.Start();
        }

        #endregion
    }
}