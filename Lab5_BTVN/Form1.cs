using Lab5_BTVN.Model;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace Lab5_BTVN
{
    public partial class Form1 : Form
    {
        private Model1 context;

        public Form1()
        {
            InitializeComponent();
            context = new Model1();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                LoadFaculty();
                LoadStudents();
                SetupDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load dữ liệu: " + ex.Message);
            }
        }

        // ========== LOAD KHOA ==========
        private void LoadFaculty()
        {
            try
            {
                var faculties = context.Faculties.ToList();

                cmbFaculty.DataSource = null;
                cmbFaculty.DataSource = faculties;
                cmbFaculty.DisplayMember = "FacultyName";
                cmbFaculty.ValueMember = "FacultyID";

                if (cmbFaculty.Items.Count > 0)
                    cmbFaculty.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load khoa: " + ex.Message);
            }
        }

        // ========== LOAD SINH VIÊN ==========
        private void LoadStudents()
        {
            try
            {
                var students = context.Students
                    .Include("Faculty")
                    .ToList();

                // Tạo DataTable để hiển thị
                DataTable dt = new DataTable();
                dt.Columns.Add("StudentID", typeof(string));
                dt.Columns.Add("FullName", typeof(string));
                dt.Columns.Add("AverageScore", typeof(double));
                dt.Columns.Add("FacultyName", typeof(string));

                // Thêm dữ liệu vào DataTable
                foreach (var student in students)
                {
                    dt.Rows.Add(
                        student.StudentID,
                        student.FullName,
                        student.AverageScore,
                        student.Faculty?.FacultyName ?? "N/A"  // Lấy tên khoa từ Faculty
                    );
                }

                dgvStudent.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi load sinh viên: " + ex.Message);
            }
        }

        // ========== CẤU HÌNH DGV ==========
        private void SetupDataGridView()
        {
            dgvStudent.AutoGenerateColumns = false;
            dgvStudent.Columns.Clear();

            dgvStudent.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "StudentID",
                HeaderText = "MSSV",
                Width = 100,
                Name = "colStudentID"
            });

            dgvStudent.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "Họ Tên",
                Width = 150,
                Name = "colFullName"
            });

            dgvStudent.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AverageScore",
                HeaderText = "ĐTB",
                Width = 80,
                Name = "colAverageScore"
            });

            // Thay đổi DataPropertyName từ "Faculty.FacultyName" → "FacultyName"
            dgvStudent.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FacultyName",  // ← ĐỂ ĐÂY
                HeaderText = "Chuyên Ngành",
                Width = 150,
                Name = "colFacultyName"
            });
        }

        // ========== CLEAR FORM ==========
        private void ClearForm()
        {
            txtMSSV.Clear();
            txtHoTen.Clear();
            txtDTB.Clear();
            if (cmbFaculty.Items.Count > 0)
                cmbFaculty.SelectedIndex = 0;
            txtMSSV.Focus();
        }

        // ========== KIỂM TRA DỮ LIỆU ==========
        private bool ValidateInput(out string studentID, out string fullName, out double avgScore)
        {
            studentID = txtMSSV.Text.Trim();
            fullName = txtHoTen.Text.Trim();
            avgScore = 0;

            if (string.IsNullOrEmpty(studentID) ||
                string.IsNullOrEmpty(fullName) ||
                string.IsNullOrEmpty(txtDTB.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return false;
            }

            if (studentID.Length != 10)
            {
                MessageBox.Show("Mã số sinh viên phải có 10 kí tự!");
                return false;
            }

            if (!double.TryParse(txtDTB.Text.Trim(), out avgScore))
            {
                MessageBox.Show("Điểm trung bình phải là số!");
                return false;
            }

            if (avgScore < 0 || avgScore > 10)
            {
                MessageBox.Show("Điểm trung bình phải từ 0 đến 10!");
                return false;
            }

            return true;
        }

        // ========== CLICK DÒNG DGV (DÙNG CellClick, KHÔNG PHẢI CellContentClick) ==========
        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.RowIndex < dgvStudent.Rows.Count)
                {
                    string studentID = dgvStudent.Rows[e.RowIndex]
                        .Cells["colStudentID"].Value?.ToString() ?? "";

                    if (!string.IsNullOrEmpty(studentID))
                    {
                        var student = context.Students
                            .Include("Faculty")
                            .FirstOrDefault(s => s.StudentID == studentID);

                        if (student != null)
                        {
                            txtMSSV.Text = student.StudentID ?? "";
                            txtHoTen.Text = student.FullName ?? "";
                            txtDTB.Text = student.AverageScore.ToString();

                            // Hiện chuyên ngành
                            if (student.Faculty != null)
                                cmbFaculty.SelectedValue = student.FacultyID;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // ========== NÚT THÊM / SỬA ==========
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput(out string studentID, out string fullName, out double avgScore))
                    return;

                int facultyID = (int)cmbFaculty.SelectedValue;

                var existingStudent = context.Students
                    .FirstOrDefault(s => s.StudentID == studentID);

                if (existingStudent != null)
                {
                    existingStudent.FullName = fullName;
                    existingStudent.AverageScore = avgScore;
                    existingStudent.FacultyID = facultyID;

                    context.SaveChanges();
                    MessageBox.Show("Cập nhật dữ liệu thành công!");
                }
                else
                {
                    var newStudent = new Student
                    {
                        StudentID = studentID,
                        FullName = fullName,
                        AverageScore = avgScore,
                        FacultyID = facultyID
                    };

                    context.Students.Add(newStudent);
                    context.SaveChanges();
                    MessageBox.Show("Thêm mới dữ liệu thành công!");
                }

                LoadStudents();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        // ========== NÚT XÓA ==========
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string studentID = txtMSSV.Text.Trim();

                if (string.IsNullOrEmpty(studentID))
                {
                    MessageBox.Show("Vui lòng nhập MSSV cần xóa!");
                    txtMSSV.Focus();
                    return;
                }

                if (studentID.Length != 10)
                {
                    MessageBox.Show("Mã số sinh viên phải có 10 kí tự!");
                    return;
                }

                var student = context.Students
                    .FirstOrDefault(s => s.StudentID == studentID);

                if (student == null)
                {
                    MessageBox.Show("Không tìm thấy MSSV cần xóa!");
                    return;
                }

                DialogResult dr = MessageBox.Show(
                    $"Bạn có chắc muốn xóa sinh viên: {student.FullName}?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    context.Students.Remove(student);
                    context.SaveChanges();

                    MessageBox.Show("Xóa sinh viên thành công!");
                    LoadStudents();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message);
            }
        }
    }
}
