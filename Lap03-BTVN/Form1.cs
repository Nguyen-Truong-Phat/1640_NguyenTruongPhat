using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lap03_BTVN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string lastName = txtLastName.Text.Trim();
            string firstName = txtFirstName.Text.Trim();
            string phone = txtPhone.Text.Trim();

            if (string.IsNullOrEmpty(lastName) ||
                string.IsNullOrEmpty(firstName) ||
                string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Last Name, First Name và Phone.");
                return;
            }

            // Tạo 1 dòng mới cho ListView
            ListViewItem item = new ListViewItem(lastName);  // cột 1
            item.SubItems.Add(firstName);                    // cột 2
            item.SubItems.Add(phone);                        // cột 3

            // Thêm vào ListView
            lvStudents.Items.Add(item);

            // Xóa textbox sau khi thêm
            txtLastName.Clear();
            txtFirstName.Clear();
            txtPhone.Clear();
            txtLastName.Focus();
        }

        

        private void lvStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvStudents.SelectedItems.Count == 0)
                return;

            ListViewItem item = lvStudents.SelectedItems[0];

            txtLastName.Text = item.SubItems[0].Text;
            txtFirstName.Text = item.SubItems[1].Text;
            txtPhone.Text = item.SubItems[2].Text;
        }

        private void LVdemo_Load(object sender, EventArgs e)
        {
            ListViewItem item1 = new ListViewItem("Ly");   // Last Name
            item1.SubItems.Add("Thi Bong");                // First Name
            item1.SubItems.Add("23456");                   // Phone
            lvStudents.Items.Add(item1);

            // Học sinh 2
            ListViewItem item2 = new ListViewItem("Nguyen");
            item2.SubItems.Add("Van Chinh");
            item2.SubItems.Add("4555");
            lvStudents.Items.Add(item2);

            // Học sinh 3
            ListViewItem item3 = new ListViewItem("Tran");
            item3.SubItems.Add("Chanh Truc");
            item3.SubItems.Add("123456");
            lvStudents.Items.Add(item3);
        }
    }
}
