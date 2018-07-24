namespace RestaurantManager
{
    partial class fReport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fReport));
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.QL_NhaHangDataSet = new RestaurantManager.QL_NhaHangDataSet();
            this.USP_GetListBillByEnglishBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.USP_GetListBillByEnglishTableAdapter = new RestaurantManager.QL_NhaHangDataSetTableAdapters.USP_GetListBillByEnglishTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.QL_NhaHangDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.USP_GetListBillByEnglishBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.USP_GetListBillByEnglishBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "RestaurantManager.ReportBill.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(20, 60);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(901, 460);
            this.reportViewer1.TabIndex = 0;
            // 
            // QL_NhaHangDataSet
            // 
            this.QL_NhaHangDataSet.DataSetName = "QL_NhaHangDataSet";
            this.QL_NhaHangDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // USP_GetListBillByEnglishBindingSource
            // 
            this.USP_GetListBillByEnglishBindingSource.DataMember = "USP_GetListBillByEnglish";
            this.USP_GetListBillByEnglishBindingSource.DataSource = this.QL_NhaHangDataSet;
            // 
            // USP_GetListBillByEnglishTableAdapter
            // 
            this.USP_GetListBillByEnglishTableAdapter.ClearBeforeFill = true;
            // 
            // fReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(941, 540);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "fReport";
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Báo cáo";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Load += new System.EventHandler(this.fReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.QL_NhaHangDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.USP_GetListBillByEnglishBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource USP_GetListBillByEnglishBindingSource;
        private QL_NhaHangDataSet QL_NhaHangDataSet;
        private QL_NhaHangDataSetTableAdapters.USP_GetListBillByEnglishTableAdapter USP_GetListBillByEnglishTableAdapter;
    }
}