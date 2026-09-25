using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.GridLines = true;

            if (listView1.Columns.Count == 0)
            {
                listView1.Columns.Add("Last Name", 100);
                listView1.Columns.Add("First Name", 110);
                listView1.Columns.Add("Phone", 110);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text) || string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin Họ và Tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ListViewItem item = new ListViewItem(txtLastName.Text.Trim());
            item.SubItems.Add(txtFirstName.Text.Trim());
            item.SubItems.Add(txtPhone.Text.Trim());

            listView1.Items.Add(item);
            ClearInputs();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listView1.SelectedItems[0];
                txtLastName.Text = selectedItem.Text;
                txtFirstName.Text = selectedItem.SubItems.Count > 1 ? selectedItem.SubItems[1].Text : "";
                txtPhone.Text = selectedItem.SubItems.Count > 2 ? selectedItem.SubItems[2].Text : "";
            }
            else
            {
                ClearInputs();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                if (string.IsNullOrWhiteSpace(txtLastName.Text) || string.IsNullOrWhiteSpace(txtFirstName.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ListViewItem selectedItem = listView1.SelectedItems[0];
                selectedItem.Text = txtLastName.Text.Trim();

                if (selectedItem.SubItems.Count > 1)
                    selectedItem.SubItems[1].Text = txtFirstName.Text.Trim();
                else
                    selectedItem.SubItems.Add(txtFirstName.Text.Trim());

                if (selectedItem.SubItems.Count > 2)
                    selectedItem.SubItems[2].Text = txtPhone.Text.Trim();
                else
                    selectedItem.SubItems.Add(txtPhone.Text.Trim());

                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                DialogResult dialogResult = MessageBox.Show(
                    "Bạn có chắc chắn muốn xóa dòng đã chọn?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (dialogResult == DialogResult.Yes)
                {
                    listView1.Items.Remove(listView1.SelectedItems[0]);
                    ClearInputs();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearInputs()
        {
            txtLastName.Clear();
            txtFirstName.Clear();
            txtPhone.Clear();
            txtLastName.Focus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát không?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}