using RestaurantManager.DAO;
using RestaurantManager.DTO;
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
    public partial class fTableManager : MetroFramework.Forms.MetroForm
    {
        public fTableManager()
        {
            InitializeComponent();
            loadListTable();
        }
        #region method
        public void loadListTable()
        {
            List<Table> listTable= TableDAO.Instance.loadListTable();
            foreach (Table item in listTable)
            {
                Button button = new Button();
                button.Text = item.Name + "\n" + item.Status;
                button.Width = 80;
                button.Height = 80;
                button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                button.Font= new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
                button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
                button.BackColor = Color.FromArgb(17, 17, 17);
                button.Tag = item.Id;
                if(item.Status=="Trống")
                {
                    button.ForeColor = Color.FromArgb(243, 119, 53);
                    button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(243, 119, 53);
                }
                else
                {
                    button.ForeColor = Color.SeaGreen;
                    button.FlatAppearance.BorderColor = System.Drawing.Color.SeaGreen;
                }
                button.Click += Button_Click;
                flowLayoutPanel1.Controls.Add(button);
            }
        }
        float finalTotalPrice = 0;
        public void loadMenuByIdTable(int idTable)
        {
           List<DTO.Menu> menu= MenuDAO.Instance.loadMenuByIdTable(idTable);
            foreach (DTO.Menu item in menu)
            {
                ListViewItem lsvItem = new ListViewItem(item.Name);
                
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                float totalPrice = item.Count * item.Price;
                finalTotalPrice += totalPrice;
                lsvItem.SubItems.Add(totalPrice.ToString());
                listViewMenu.Items.Add(lsvItem);
            }
            
        }
        private void Button_Click(object sender, EventArgs e)
        {
            listViewMenu.Items.Clear();
            int id =(int) (sender as Button).Tag;
            loadMenuByIdTable(id);
            txbTotalPrice.Text = finalTotalPrice.ToString();
            finalTotalPrice = 0;
        }
        #endregion
        #region event
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.ShowDialog();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile();
            f.ShowDialog();
          
        }
#endregion
    }
}
