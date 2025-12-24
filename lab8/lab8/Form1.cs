using System;
using System.Data.Entity;
using System.Windows.Forms;
using lab8.model;

namespace lab8
{
    public partial class Form1 : Form
    {
        Model1 context = new Model1();
        BindingSource bs = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            context.Students.Load();
            bs.DataSource = context.Students.Local.ToBindingList();

            txtStudentID.DataBindings.Add("Text", bs, "StudentID", true, DataSourceUpdateMode.OnPropertyChanged);
            txtStudentName.DataBindings.Add("Text", bs, "StudentName", true, DataSourceUpdateMode.OnPropertyChanged);
            txtAge.DataBindings.Add("Text", bs, "Age", true, DataSourceUpdateMode.OnPropertyChanged);
            txtMajor.DataBindings.Add("Text", bs, "Major", true, DataSourceUpdateMode.OnPropertyChanged);

            dgvStudent.DataSource = bs;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            bs.AddNew();
            txtStudentName.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (bs.Current != null)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa sinh viên này?", "Thông báo", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    bs.RemoveCurrent();
                    context.SaveChanges();
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            this.Validate();
            bs.EndEdit();
            context.SaveChanges();
            MessageBox.Show("Lưu dữ liệu thành công!");
        }

        private void btnDau_Click(object sender, EventArgs e)
        {
            bs.MoveFirst();
        }

        private void btnTruoc_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
        }

        private void btnSau_Click(object sender, EventArgs e)
        {
            bs.MoveNext();
        }

        private void btnCuoi_Click(object sender, EventArgs e)
        {
            bs.MoveLast();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}