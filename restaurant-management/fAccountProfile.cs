using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestaurantManager.DAO;
using RestaurantManager.DTO;

namespace RestaurantManager
{
    public partial class fAccountProfile : MetroFramework.Forms.MetroForm
    {
        string UserName;
        string DisplayName;
        string PassWord;
        int Type;
        public fAccountProfile(string userName, string displayName, string passWord, int type)
        {
            UserName = userName;
            DisplayName = displayName;
            PassWord = passWord;
            Type = type;
            InitializeComponent();
            txbUserName.Text = UserName;
            txbDisplayName.Text = DisplayName;
        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void btnUpdateAccount_Click(object sender, EventArgs e)
        {
            if (AccountDAO.Instance.login(txbUserName.Text,txbPassWord.Text)==false)
            {
                MessageBox.Show("Mật khẩu không chính xác, vui lòng nhập lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            { 
                if (txbNewPass.Text == "" && txbReNewPass.Text == "")
                {
                    if (AccountDAO.Instance.changeAccountOnlyDisplayName(txbUserName.Text, txbDisplayName.Text))
                    {
                        MessageBox.Show("Cập nhập tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                else
                {

                    if (txbNewPass.Text == txbReNewPass.Text)
                    {
                        if (AccountDAO.Instance.changeAccount(txbUserName.Text, txbDisplayName.Text, txbNewPass.Text))
                            MessageBox.Show("Cập nhập tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else MessageBox.Show("Vui lòng nhập lại mật khẩu mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        
    }
}
