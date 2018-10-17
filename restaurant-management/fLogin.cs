using RestaurantManager.DAO;
using RestaurantManager.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantManager
{
    public partial class fLogin:MetroFramework.Forms.MetroForm
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát không?","Thông báo",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public bool login(string userName, string passWord)
        {
            return AccountDAO.Instance.login(userName, passWord);
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(login(txbUser.Text,txbPassWord.Text))
            {
                this.Hide();
                Account account = AccountDAO.Instance.getAccount(txbUser.Text, txbPassWord.Text);
                fTableManager f = new fTableManager(account.UserName,account.DisplayName,account.PassWord,account.Type);
                f.ShowDialog();
                txbPassWord.Clear();
                txbUser.Clear();
                txbUser.Focus();
                this.Show();
            }
            else
            {
                MessageBox.Show("Tên đăng nhận không tồn tại hoặc mật khẩu sai","Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
