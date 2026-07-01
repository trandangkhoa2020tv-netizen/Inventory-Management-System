using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhoHang.Forms
{
    internal static class UiTheme
    {
        private static readonly Color PageBackColor = Color.FromArgb(248, 250, 252);
        private static readonly Color SurfaceColor = Color.White;
        private static readonly Color BorderColor = Color.FromArgb(226, 232, 240);
        private static readonly Color InputBorderColor = Color.FromArgb(203, 213, 225);
        private static readonly Color TextColor = Color.FromArgb(15, 23, 42);
        private static readonly Color MutedTextColor = Color.FromArgb(71, 85, 105);
        private static readonly Color TitleColor = Color.FromArgb(30, 58, 138);
        private static readonly Color HeaderBackColor = Color.FromArgb(239, 246, 255);
        private static readonly Color SelectionBackColor = Color.FromArgb(37, 99, 235);

        public static void Apply(Form form)
        {
            form.BackColor = PageBackColor;
            form.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            form.Padding = new Padding(16);

            foreach (Control control in form.Controls)
            {
                ApplyControl(control);
            }
        }

        private static void ApplyControl(Control control)
        {
            switch (control)
            {
                case Panel panel:
                    ApplyPanel(panel);
                    break;

                case GroupBox groupBox:
                    ApplyGroupBox(groupBox);
                    break;

                case Label label:
                    ApplyLabel(label);
                    break;

                case TextBox textBox:
                    ApplyTextBox(textBox);
                    break;

                case ComboBox comboBox:
                    ApplyComboBox(comboBox);
                    break;

                case Button button:
                    ApplyButton(button);
                    break;

                case DataGridView grid:
                    ApplyGrid(grid);
                    break;
            }

            foreach (Control child in control.Controls)
            {
                ApplyControl(child);
            }
        }

        private static void ApplyPanel(Panel panel)
        {
            panel.BackColor = SurfaceColor;

            if (panel.Name == "pnlTopControls" || panel.Name == "pnlTop" || panel.Name == "pnlBottom")
            {
                panel.BorderStyle = BorderStyle.FixedSingle;
                panel.Padding = new Padding(18);
            }
        }

        private static void ApplyGroupBox(GroupBox groupBox)
        {
            groupBox.ForeColor = TextColor;
            groupBox.BackColor = SurfaceColor;
            groupBox.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBox.Padding = new Padding(12, 10, 12, 12);
        }

        private static void ApplyLabel(Label label)
        {
            label.ForeColor = TextColor;
            label.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);

            if (label.Name == "lblTitle")
            {
                label.ForeColor = TitleColor;
                label.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
            }
            else if (label.Name == "lblTimKiem")
            {
                label.Text = "Tìm kiếm:";
                label.ForeColor = TitleColor;
                label.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            }
            else if (label.Name == "lblLichSuTitle")
            {
                label.ForeColor = TitleColor;
                label.BackColor = HeaderBackColor;
                label.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                label.Padding = new Padding(12, 0, 0, 0);
            }
            else if (label.Name == "lblTongTien")
            {
                label.ForeColor = Color.FromArgb(185, 28, 28);
                label.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            }
        }

        private static void ApplyTextBox(TextBox textBox)
        {
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Color.White;
            textBox.ForeColor = TextColor;
            textBox.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
        }

        private static void ApplyComboBox(ComboBox comboBox)
        {
            comboBox.FlatStyle = FlatStyle.Standard;
            comboBox.BackColor = Color.White;
            comboBox.ForeColor = TextColor;
            comboBox.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
        }

        private static void ApplyButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = InputBorderColor;
            button.Cursor = Cursors.Hand;
            button.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);

            switch (button.Name)
            {
                case "btnThem":
                    button.Text = "+ Thêm mới";
                    SetSolidButton(button, Color.FromArgb(37, 99, 235));
                    break;

                case "btnSua":
                    SetSolidButton(button, Color.FromArgb(245, 158, 11));
                    break;

                case "btnXoa":
                    SetSolidButton(button, Color.FromArgb(220, 38, 38));
                    break;

                case "btnLamMoi":
                    button.Text = "Làm mới";
                    button.BackColor = Color.White;
                    button.ForeColor = Color.FromArgb(51, 65, 85);
                    button.FlatAppearance.BorderSize = 1;
                    break;

                case "btnThemMon":
                    SetSolidButton(button, Color.FromArgb(37, 99, 235));
                    break;

                case "btnLuuPhieu":
                    SetSolidButton(button, Color.FromArgb(22, 163, 74));
                    break;

                case "btnExcel":
                    SetSolidButton(button, Color.FromArgb(21, 128, 61));
                    break;

                case "btnPdf":
                    SetSolidButton(button, Color.FromArgb(185, 28, 28));
                    break;

                default:
                    if (button.BackColor == SystemColors.Control || button.BackColor == Color.Empty)
                    {
                        button.BackColor = Color.White;
                        button.ForeColor = TextColor;
                        button.FlatAppearance.BorderSize = 1;
                    }
                    break;
            }
        }

        private static void SetSolidButton(Button button, Color backColor)
        {
            button.BackColor = backColor;
            button.ForeColor = Color.White;
            button.FlatAppearance.BorderSize = 0;
        }

        private static void ApplyGrid(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            grid.GridColor = Color.FromArgb(229, 231, 235);
            grid.RowHeadersVisible = false;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.EnableHeadersVisualStyles = false;
            grid.RowTemplate.Height = 34;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToResizeRows = false;
            grid.ReadOnly = true;

            grid.ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColor;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = TextColor;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = HeaderBackColor;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = TextColor;
            grid.ColumnHeadersHeight = 38;

            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = TextColor;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            grid.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
            grid.DefaultCellStyle.Padding = new Padding(4, 0, 4, 0);

            grid.AlternatingRowsDefaultCellStyle.BackColor = PageBackColor;
            grid.AlternatingRowsDefaultCellStyle.ForeColor = TextColor;
            grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = SelectionBackColor;
            grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
            grid.AlternatingRowsDefaultCellStyle.Padding = new Padding(4, 0, 4, 0);
        }
    }
}
