using System.Drawing;
using System.Windows.Forms;

namespace QuanLyKhoHang.Forms
{
    internal static class UiTheme
    {
        private static readonly Color PageBackColor = Color.FromArgb(245, 247, 250);
        private static readonly Color SurfaceColor = Color.FromArgb(248, 250, 252);
        private static readonly Color BorderColor = Color.FromArgb(178, 190, 205);
        private static readonly Color TextColor = Color.FromArgb(33, 37, 41);
        private static readonly Color MutedTextColor = Color.FromArgb(73, 80, 87);
        private static readonly Color HeaderBackColor = Color.FromArgb(229, 237, 246);
        private static readonly Color SelectionBackColor = Color.FromArgb(25, 118, 210);

        public static void Apply(Form form)
        {
            form.BackColor = PageBackColor;
            form.Font = new Font("Segoe UI", 9F, FontStyle.Regular);

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
                    panel.BackColor = SurfaceColor;
                    break;

                case GroupBox groupBox:
                    groupBox.ForeColor = MutedTextColor;
                    groupBox.BackColor = SurfaceColor;
                    break;

                case Label label:
                    label.ForeColor = label.ForeColor == Color.DarkBlue
                        ? Color.FromArgb(0, 86, 179)
                        : TextColor;
                    break;

                case TextBox textBox:
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                    textBox.BackColor = Color.White;
                    textBox.ForeColor = TextColor;
                    break;

                case ComboBox comboBox:
                    comboBox.FlatStyle = FlatStyle.Standard;
                    comboBox.BackColor = Color.White;
                    comboBox.ForeColor = TextColor;
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

        private static void ApplyButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = BorderColor;
            button.Cursor = Cursors.Hand;

            if (button.BackColor == SystemColors.Control || button.BackColor == Color.Empty)
            {
                button.BackColor = Color.White;
                button.ForeColor = TextColor;
            }
        }

        private static void ApplyGrid(DataGridView grid)
        {
            grid.BackgroundColor = Color.FromArgb(238, 241, 245);
            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            grid.GridColor = BorderColor;
            grid.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            grid.EnableHeadersVisualStyles = false;
            grid.RowHeadersWidth = 36;
            grid.RowTemplate.Height = 30;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            grid.ColumnHeadersDefaultCellStyle.BackColor = HeaderBackColor;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = TextColor;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = HeaderBackColor;
            grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = TextColor;

            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = TextColor;
            grid.DefaultCellStyle.SelectionBackColor = SelectionBackColor;
            grid.DefaultCellStyle.SelectionForeColor = Color.White;

            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 252, 254);
            grid.AlternatingRowsDefaultCellStyle.ForeColor = TextColor;
            grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = SelectionBackColor;
            grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
        }
    }
}
