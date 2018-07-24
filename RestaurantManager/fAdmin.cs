using RestaurantManager.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestaurantManager.DTO;
namespace RestaurantManager
{
    public partial class fAdmin : MetroFramework.Forms.MetroForm
    {
        BindingSource listFood = new BindingSource();
        BindingSource listCategory = new BindingSource();
        BindingSource listTable = new BindingSource();
        BindingSource listAcc = new BindingSource();
        int Type;
        public fAdmin(int type)
        {
            Type = type;
            InitializeComponent();
            load();
        }
        public void load()
        {
            loadDate();
            getListBill(metroDateTimeFromDate.Value, metroDateTimeToDate.Value.AddDays(1));
            //FoodList
            loadFoodList();
            dataGridViewListFood.DataSource = listFood;
            loadCategory();
            addFoodBinding();
            //CategoryList
            loadListCategory();
            dataGridViewListFoodCategory.DataSource = listCategory;
            addCategoryBinding();
            //TableFoodList
            loadListTable();
            dataGridViewListTable.DataSource = listTable;
            addTableBinding();
            //AccList
            loadListAcc();
            dataGridViewListAccount.DataSource = listAcc;
            loadTypeAcc();
            addAccBinding();

        }
        public void loadDate()
        {
            DateTime now = DateTime.Now;
            metroDateTimeFromDate.Value = new DateTime(now.Year, now.Month, 1);
            metroDateTimeToDate.Value = metroDateTimeFromDate.Value.AddDays(-1).AddMonths(1);
        }
        public void getListBill(DateTime fromDate,DateTime toDate)
        {
            dataGridViewListBill.DataSource=BillDAO.Instance.getListBillByDate(fromDate, toDate);

        }
        public void loadFoodList()
        {
           listFood.DataSource= FoodDAO.Instance.loadFoodList();// chú ý gán BindingSource
        }
        
        public void addFoodBinding()
        {
            txbFoodID.DataBindings.Add(new Binding("Text",dataGridViewListFood.DataSource,"ID",true,DataSourceUpdateMode.Never));
            txbFoodName.DataBindings.Add(new Binding("Text", dataGridViewListFood.DataSource, "Tên món ăn", true, DataSourceUpdateMode.Never));
           numericUpDownPrice.DataBindings.Add(new Binding("Value", dataGridViewListFood.DataSource, "Giá", true, DataSourceUpdateMode.Never));
            cbFoodCategory.DataBindings.Add(new Binding("Text", dataGridViewListFood.DataSource, "Danh mục", true, DataSourceUpdateMode.Never));
        }
        public void loadCategory()
        {
            cbFoodCategory.DataSource = CategoryDAO.Instance.loadCategory();
            cbFoodCategory.DisplayMember = "Name";
            
        }
        public void loadListCategory()
        {
            listCategory.DataSource = CategoryDAO.Instance.loadCategory2();
        }
        public void addCategoryBinding()
        {
            txbCateID.DataBindings.Add(new Binding("Text", dataGridViewListFoodCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbCateName.DataBindings.Add(new Binding("Text", dataGridViewListFoodCategory.DataSource, "Tên danh mục", true, DataSourceUpdateMode.Never));
        }
        public void loadListTable()
        {
            listTable.DataSource = TableDAO.Instance.loadListTable2();
        }

        public void addTableBinding()
        {
            txbTableID.DataBindings.Add(new Binding("Text", dataGridViewListTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txbTableName.DataBindings.Add(new Binding("Text", dataGridViewListTable.DataSource, "Tên bàn", true, DataSourceUpdateMode.Never));
            cbTableStatus.DataBindings.Add(new Binding("Text", dataGridViewListTable.DataSource, "Trạng thái", true, DataSourceUpdateMode.Never));
        }
        public void loadListAcc()
        {
            if (Type == 1) listAcc.DataSource = AccountDAO.Instance.loadListAccForAdmin();
            else listAcc.DataSource = AccountDAO.Instance.loadListAccForRoot();
        }
        public void loadTypeAcc()
        {
            if (Type == 1) cbAccType.Items.Add(0);
            else
            {
                cbAccType.Items.Add(0);
                cbAccType.Items.Add(1);
            }
        }
        public void addAccBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dataGridViewListAccount.DataSource, "Tên đăng nhập", true, DataSourceUpdateMode.Never));
            txtDisplayName.DataBindings.Add(new Binding("Text", dataGridViewListAccount.DataSource, "Tên hiển thị", true, DataSourceUpdateMode.Never));
            cbAccType.DataBindings.Add(new Binding("Text", dataGridViewListAccount.DataSource, "Loại tài khoản", true, DataSourceUpdateMode.Never));
        }
        private void btnPay_Click(object sender, EventArgs e)
        {
            DateTime toDate = metroDateTimeToDate.Value.AddDays(1);
            getListBill(metroDateTimeFromDate.Value,toDate );
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            loadFoodList();
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            int idCate = (cbFoodCategory.SelectedValue as Category).Id;
            if(FoodDAO.Instance.insertFood(txbFoodName.Text,idCate,(float)numericUpDownPrice.Value))
            {
                MessageBox.Show("Thêm món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Thêm món ăn thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadFoodList();
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            int idCate = (cbFoodCategory.SelectedValue as Category).Id;
            if (FoodDAO.Instance.updateFood(txbFoodName.Text, idCate, (float)numericUpDownPrice.Value,Convert.ToInt32(txbFoodID.Text)))
            {
                MessageBox.Show("Sửa món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Sửa món ăn thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadFoodList();
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            BillInfoDAO.Instance.deleteBillInfoByIdFood(Convert.ToInt32(txbFoodID.Text));
            if (FoodDAO.Instance.deleteFood(Convert.ToInt32(txbFoodID.Text)))
            {
                MessageBox.Show("Xóa món ăn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Xóa món ăn thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadFoodList();
        }

     

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDownPrice_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cbFoodCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnSerarchFood_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbFoodName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            listFood.DataSource = FoodDAO.Instance.getListFoodByName(txbSerarchFood.Text);
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id= Convert.ToInt32(txbTableID.Text);
            if (TableDAO.Instance.deleteTable(id))
            {
                MessageBox.Show("Xóa bàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Xóa bàn thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadListTable();
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txbTableName_TextChanged(object sender, EventArgs e)
        {

        }

        private void cbTableStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnTableID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

    

        private void tabControlAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPagePay_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridViewListBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void metroDateTimeToDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void metroDateTimeFromDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tabPageFood_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridViewListFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPageCategory_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewListFoodCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txbCateName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbCateID_TextChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnEditCate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCateID.Text);
            if (CategoryDAO.Instance.updateCate(txbCateName.Text, id))
            {
                MessageBox.Show("Sửa danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Sửa danh mục thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadListCategory();
            loadFoodList();
            loadCategory();
        }

        private void btnDeleteCate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbCateID.Text);
            if (CategoryDAO.Instance.deleteCate(id))
            {
                MessageBox.Show("Xóa danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Xóa danh mục thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadListCategory();
            loadFoodList();
            loadCategory();
        }

        private void btnAddCate_Click(object sender, EventArgs e)
        {
            if (CategoryDAO.Instance.insertCate(txbCateName.Text))
            {
                MessageBox.Show("Thêm danh mục thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Thêm danh mục thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadListCategory();
            loadFoodList();
            loadCategory();
            
        }

        private void tabPageTable_Click(object sender, EventArgs e)
        {

        }

        private void btnEditTable_Click(object sender, EventArgs e)
        {
            if (TableDAO.Instance.updateTable(Convert.ToInt32(txbTableID.Text), txbTableName.Text, cbTableStatus.Text))
            {
                MessageBox.Show("Sửa bàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Sửa bàn thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadListTable();
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            if (cbTableStatus.Text == "") MessageBox.Show("Vui lòng chọn trạng thái của bàn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if (TableDAO.Instance.insertTable(txbTableName.Text, cbTableStatus.Text))
                {
                    MessageBox.Show("Thêm bàn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else MessageBox.Show("Thêm bàn thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadListTable();
            }
        }

        private void tabPageAccount_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewListAccount_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnResetPassWord_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txbTypeAccount_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void txtDisplayName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnEditAcc_Click(object sender, EventArgs e)
        {

            if (AccountDAO.Instance.updateAcc(txbUserName.Text, txtDisplayName.Text,Convert.ToInt32(cbAccType.Text)))
            {
                MessageBox.Show("Sửa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Sửa tài khoản thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadListAcc();
        }

        private void btnDeleteAcc_Click(object sender, EventArgs e)
        {
            if (AccountDAO.Instance.deleteAcc(txbUserName.Text))
            {
                MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else MessageBox.Show("Xóa tài khoản thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            loadListAcc();
        }

        private void btnAddAcc_Click(object sender, EventArgs e)
        {
            if (txbPass.Text == "") MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                if(AccountDAO.Instance.checkUserName(txbUserName.Text)==1)
                    MessageBox.Show("Tên đăng nhập bị trùng, vui lòng nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    if (AccountDAO.Instance.insertAcc(txbUserName.Text, txtDisplayName.Text, txbPass.Text, Convert.ToInt32(cbAccType.Text)))
                    {
                        MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else MessageBox.Show("Thêm tài khoản thất bại, vui lòng thử lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadListAcc();
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fReport f = new fReport(metroDateTimeFromDate.Value,metroDateTimeToDate.Value);
            f.ShowDialog();
        }
    }
}
