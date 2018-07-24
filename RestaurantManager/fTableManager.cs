using RestaurantManager.DAO;
using RestaurantManager.DTO;
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

namespace RestaurantManager
{
    public partial class fTableManager : MetroFramework.Forms.MetroForm
    {
        string UserName;
        string DisplayName;
        string PassWord;
        int Type;
        public fTableManager(string userName,string displayName, string passWord, int type)
        {
            UserName = userName;
            DisplayName = displayName;
            PassWord = passWord;
            Type = type;
            InitializeComponent();
            loadListTable();
            loadCategory();
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin cá nhân ( " + DisplayName + " )";
        }
        #region method
        public void loadListTable()
        {
            flowLayoutPanel1.Controls.Clear();
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
                button.Tag = item;
                if(item.Status=="Trống")
                {
                    button.ForeColor = Color.FromArgb(243, 119, 53);
                    button.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(243, 119, 53);
                }
                if (item.Status == "Có người")
                {
                    button.ForeColor = Color.SeaGreen;
                    button.FlatAppearance.BorderColor = System.Drawing.Color.SeaGreen;
                }
                button.Click += Button_Click;
                flowLayoutPanel1.Controls.Add(button);
                
            }
            cbListTable.DataSource = listTable;
            cbListTable.DisplayMember = "name";
        }
        public void loadTable(int idTable)
        {
            Table table = TableDAO.Instance.loadTable(idTable);
            foreach (Control item in flowLayoutPanel1.Controls)
            {
                if((item.Tag as Table).Id==table.Id)
                {
                    
                    item.Text = table.Name + "\n" + table.Status;
                    if (table.Status == "Trống")
                    {
                        
                        item.ForeColor = Color.FromArgb(243, 119, 53);
                        (item as Button).FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(243, 119, 53);
                    }
                    else
                    {
                        item.ForeColor = Color.SeaGreen;
                        (item as Button).FlatAppearance.BorderColor = System.Drawing.Color.SeaGreen;
                    }
                }
            }
            
        }

        public void loadMenuByIdTable(int idTable)
        {
            CultureInfo culture = new CultureInfo("vi-VN");
            listViewMenu.Items.Clear();
            List<DTO.Menu> menu= MenuDAO.Instance.loadMenuByIdTable(idTable);
            foreach (DTO.Menu item in menu)
            {
                ListViewItem lsvItem = new ListViewItem(item.Name);
                
                lsvItem.SubItems.Add(item.Count.ToString());
                lsvItem.SubItems.Add(item.Price.ToString("c",culture));
                float totalPrice = item.Count * item.Price;

                lsvItem.SubItems.Add(totalPrice.ToString("c", culture));
                listViewMenu.Items.Add(lsvItem);
            }
            
        }
        public void loadFinalTotalPrice(int idTable)
        {
            float finalTotalPrice = 0;
            List<DTO.Menu> menu = MenuDAO.Instance.loadMenuByIdTable(idTable);
            foreach (DTO.Menu item in menu)
            {
                float totalPrice = item.Count * item.Price;
                finalTotalPrice += totalPrice;
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            txbTotalPrice.Text = finalTotalPrice.ToString("c", culture);
            
        }
        public void loadCategory()
        {
            List<Category> listCate = CategoryDAO.Instance.loadCategory();
            cbCategory.DataSource = listCate;
            cbCategory.DisplayMember = "Name";
        }
        public void loadFoodListbyCategory(int id)
        {
            List<Food> listFood = FoodDAO.Instance.loadFoodListByIdCategory(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "Name";
            
        }
       
        #endregion
        #region event
        private void Button_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.FlatAppearance.MouseOverBackColor= Color.FromArgb(217, 235, 249);
            btn.BackColor = Color.FromArgb(217, 235, 249);
            foreach (Control item in flowLayoutPanel1.Controls)
            {
                if (item != btn)
                {
                    (item as Button).BackColor = System.Drawing.Color.FromArgb(17,17,17);
                    
                }
            }
            Table table= btn.Tag as Table;
            listViewMenu.Tag = btn.Tag;
            loadMenuByIdTable(table.Id);
            loadFinalTotalPrice(table.Id);
        }
      
       
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Type !=0)
            {
                this.Hide();
                fAdmin f = new fAdmin(Type);
                f.ShowDialog();
                this.Show();
                loadCategory();
               
                if (listViewMenu.Tag as Table != null)
                {
                    int idTable = (listViewMenu.Tag as Table).Id;
                    loadListTable();
                    loadMenuByIdTable(idTable);
                }
            }
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            fAccountProfile f = new fAccountProfile(UserName,DisplayName,PassWord,Type);
            f.ShowDialog();
            this.Show();
             string Name = AccountDAO.Instance.getDisplayNameByUserName(UserName).DisplayName;
             thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin cá nhân ( " + Name + " )";
            DisplayName = Name;
        }

       

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            Category cate = cb.SelectedValue as Category;
           loadFoodListbyCategory(cate.Id);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
          
            
            if (listViewMenu.Tag as Table == null)
            {
                MessageBox.Show("Vui lòng chọn bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbFood.SelectedValue == null) return;
            int idTable = (listViewMenu.Tag as Table).Id;
            int idBill = BillDAO.Instance.getUncheckBillByIdTable(idTable);
            int idFood = (int)(cbFood.SelectedValue as Food).Id;
            int count = (int)numericUpDownCountFood.Value;
            if (idBill==-1)
            {
                BillDAO.Instance.insertBill(idTable);
                BillInfoDAO.Instance.insertBillInfo(BillDAO.Instance.getMaxIdBill(), idFood,count);
            }
            else
            {
                BillInfoDAO.Instance.insertBillInfo(idBill,idFood,count);
            }
            numericUpDownCountFood.Value = 1;
            loadFinalTotalPrice(idTable);
            loadMenuByIdTable(idTable);
            //loadListTable();
            loadTable(idTable);
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            Table table = listViewMenu.Tag as Table;
            int idBill = BillDAO.Instance.getUncheckBillByIdTable(table.Id);
            BillDAO.Instance.discount(idBill, (int)numericUpDownDiscount.Value);
            if (idBill != -1)
            {
                if(MessageBox.Show("Bạn có chắc thanh toán cho "+table.Name+" không?","Thanh toán",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.OK)
                {
                    BillDAO.Instance.checkOut(idBill);
                    if (numericUpDownDiscount.Value != 0)
                    {
                        double totalPrice =Convert.ToDouble(txbTotalPrice.Text.Split(',')[0])*1000;
                        double final_price =totalPrice-(totalPrice/100)*(int)numericUpDownDiscount.Value;
                        string text = "Số tiền bạn cần thanh toán sau khi giảm giá "+numericUpDownDiscount.Value+"% là:\n"+totalPrice.ToString()+" - "+totalPrice.ToString()+" * "+numericUpDownDiscount.Value+"% ="+final_price.ToString()+" VND";
                        MessageBox.Show(text, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //SẼ PHÁT TRIỂN THÊM IN HÓA ĐƠN FILE EXCEL
                    }
                    loadMenuByIdTable(table.Id);
                    CultureInfo culture = new CultureInfo("vi-VN");
                    int finalPrice = 0;
                    txbTotalPrice.Text = finalPrice.ToString("c", culture);
                }
                
            }
            loadFinalTotalPrice(table.Id);
            loadTable(table.Id);
            //loadListTable();
            numericUpDownDiscount.Value = 0;
        }
        

        private void btnSwichTable_Click(object sender, EventArgs e)
        {
            if ((listViewMenu.Tag as Table).Name != cbListTable.Text)
            {
                if (MessageBox.Show("Bạn có chắc chuyển "+ (listViewMenu.Tag as Table).Name + " cho " + cbListTable.Text + " không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    int id = (cbListTable.SelectedValue as Table).Id;
                    TableDAO.Instance.switchTable((listViewMenu.Tag as Table).Id, id);
                    //loadListTable();
                    loadTable((listViewMenu.Tag as Table).Id);
                    loadTable(id);
                    loadMenuByIdTable((listViewMenu.Tag as Table).Id);
                }
            }
        }

        private void lịchSửToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fHistoryPay f = new fHistoryPay();
            f.ShowDialog();
        }

        #endregion

        private void thêmMónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFood_Click(this, new EventArgs());
        }

        private void chuyểnBànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnSwichTable_Click(this, new EventArgs());
        }

        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnPay_Click(this, new EventArgs());
        }

        private void trợGiúpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/ndc07.it");
        }

        private void listViewMenu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
