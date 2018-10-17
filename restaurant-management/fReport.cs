using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManager
{
    public partial class fReport : MetroFramework.Forms.MetroForm
    {
        DateTime ToDate;
        DateTime FromDate;
        public fReport(DateTime fromDate, DateTime toDate)
        {
            ToDate = toDate;
            FromDate = fromDate;
            InitializeComponent();
        }

        private void fReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'QL_NhaHangDataSet.USP_GetListBillByEnglish' table. You can move, or remove it, as needed.
            this.USP_GetListBillByEnglishTableAdapter.Fill(this.QL_NhaHangDataSet.USP_GetListBillByEnglish,FromDate,ToDate);
            // TODO: This line of code loads data into the 'QL_NhaHangDataSet2.USP_getFoodList' table. You can move, or remove it, as needed.
            //this.USP_ndc07TableAdapter.Fill(this.QL_NhaHangDataSet4.USP_ndc07,FromDate,ToDate);

            this.reportViewer1.RefreshReport();
        }


    }
}
