using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestaurantManager.DAO;
using RestaurantManager.DTO;
namespace RestaurantManager
{
    public partial class fHistoryPay : MetroFramework.Forms.MetroForm
    {
        public fHistoryPay()
        {
            
            InitializeComponent();
            loadListPay(metroDateTimeSelectDate.Value);
        }
       public void loadListPay(DateTime date)
        {
            listViewListPay.Items.Clear();
            CultureInfo culture = new CultureInfo("vi-VN");
            List<HistoryPay> pay = HistoryPayDAO.Instance.loadListPay(date);
            foreach (HistoryPay item in pay)
            {
                ListViewItem lsvItem = new ListViewItem(item.TableName);
                lsvItem.SubItems.Add(item.DateCheckIn.ToString());
                lsvItem.SubItems.Add(item.DateCheckOut.ToString());
                lsvItem.SubItems.Add(item.Discount.ToString()+"%");
                lsvItem.SubItems.Add(item.TotalPrice.ToString("c",culture));
                lsvItem.SubItems.Add(item.FinalPrice.ToString("c", culture));
                listViewListPay.Items.Add(lsvItem);
            }
        }

        private void metroDateTimeSelectDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = (sender as MetroFramework.Controls.MetroDateTime).Value;
            loadListPay(date);
        }
    }
}
