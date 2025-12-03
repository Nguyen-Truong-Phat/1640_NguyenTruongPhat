using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lap04
{
    public partial class lap4 : Form
    {
        public lap4()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbFaculty.SelectedIndex = 0;  // QTKD
            radFemale.Checked = true;
            lblMaleCount.Text = "0";
            lblFemaleCount.Text = "0";
            // Khởi tạo 5 sinh viên mẫu
            dgvStudent.Rows.Add("SV001", "Nguyễn Văn A", "Nam", 8.5, "CNTT");
            dgvStudent.Rows.Add("SV002", "Trần Thị B", "Nữ", 7.0, "QTKD");
            dgvStudent.Rows.Add("SV003", "Lê Văn C", "Nam", 6.5, "NNA");
            dgvStudent.Rows.Add("SV004", "Phạm Thị D", "Nữ", 9.0, "CNTT");
            dgvStudent.Rows.Add("SV005", "Hoàng Văn E", "Nam", 5.5, "QTKD");

            // Cập nhật số lượng Nam, Nữ
            UpdateCount();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nhập liệu
                if (string.IsNullOrEmpty(txtStudentID.Text) ||
                    string.IsNullOrEmpty(txtFullName.Text) ||
                    string.IsNullOrEmpty(txtAverageScore.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi");
                    return;
                }

                string studentID = txtStudentID.Text;
                string fullName = txtFullName.Text;
                string gender = radMale.Checked ? "Nam" : "Nữ";
                float score = float.Parse(txtAverageScore.Text);
                string faculty = cmbFaculty.Text;

                // Kiểm tra xem MSSV có tồn tại không
                int rowIndex = GetRowIndex(studentID);

                if (rowIndex == -1)
                {
                    // Thêm mới
                    dgvStudent.Rows.Add(studentID, fullName, gender, score, faculty);
                    MessageBox.Show("Thêm mới dữ liệu thành công!");
                }
                else
                {
                    // Sửa
                    dgvStudent.Rows[rowIndex].Cells[0].Value = studentID;
                    dgvStudent.Rows[rowIndex].Cells[1].Value = fullName;
                    dgvStudent.Rows[rowIndex].Cells[2].Value = gender;
                    dgvStudent.Rows[rowIndex].Cells[3].Value = score;
                    dgvStudent.Rows[rowIndex].Cells[4].Value = faculty;
                    MessageBox.Show("Cập nhật dữ liệu thành công!");
                }

                UpdateCount();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtStudentID.Text))
                {
                    MessageBox.Show("Vui lòng nhập MSSV cần xóa!", "Lỗi");
                    return;
                }

                int rowIndex = GetRowIndex(txtStudentID.Text);

                if (rowIndex == -1)
                {
                    MessageBox.Show("Không tìm thấy MSSV cần xóa!", "Lỗi");
                }
                else
                {
                    DialogResult dr = MessageBox.Show("Bạn có muốn xóa?", "Xác nhận",
                        MessageBoxButtons.YesNo);

                    if (dr == DialogResult.Yes)
                    {
                        dgvStudent.Rows.RemoveAt(rowIndex);
                        MessageBox.Show("Xóa sinh viên thành công!");
                        UpdateCount();
                        ClearForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }
        private int GetRowIndex(string studentID)
        {
            for (int i = 0; i < dgvStudent.Rows.Count; i++)
            {
                if (dgvStudent.Rows[i].Cells[0].Value != null &&
                    dgvStudent.Rows[i].Cells[0].Value.ToString() == studentID)
                {
                    return i;
                }
            }
            return -1;
        }

        private void ClearForm()
        {
            txtStudentID.Clear();
            txtFullName.Clear();
            txtAverageScore.Clear();
            radFemale.Checked = true;
        }

        private void UpdateCount()
        {
            int maleCount = 0, femaleCount = 0;

            foreach (DataGridViewRow row in dgvStudent.Rows)
            {
                if (row.Cells[2].Value != null)
                {
                    if (row.Cells[2].Value.ToString() == "Nam")
                        maleCount++;
                    else
                        femaleCount++;
                }
            }

            lblMaleCount.Text = maleCount.ToString();
            lblFemaleCount.Text = femaleCount.ToString();
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = dgvStudent.Rows[e.RowIndex];

                    txtStudentID.Text = row.Cells[0].Value?.ToString() ?? "";
                    txtFullName.Text = row.Cells[1].Value?.ToString() ?? "";
                    txtAverageScore.Text = row.Cells[3].Value?.ToString() ?? "";

                    string gender = row.Cells[2].Value?.ToString() ?? "Nữ";
                    radMale.Checked = (gender == "Nam");
                    radFemale.Checked = (gender == "Nữ");

                    cmbFaculty.Text = row.Cells[4].Value?.ToString() ?? "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
