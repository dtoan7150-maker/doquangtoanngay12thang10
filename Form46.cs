
using System.ComponentModel;
using System.Text;

namespace BaiTap464748;

public class Form46 : Form
{
    private readonly BindingList<Student46> students = new()
    {
        new Student46 { MaSV = "SV001", HoTen = "Nguyen Van An", Tuoi = 20, GioiTinh = "Nam" },
        new Student46 { MaSV = "SV002", HoTen = "Tran Thi Binh", Tuoi = 21, GioiTinh = "Nu" }
    };

    private readonly BindingSource source = new();
    private readonly DataGridView grid = new();
    private readonly TextBox txtMa = new();
    private readonly TextBox txtTen = new();
    private readonly NumericUpDown numTuoi = new()
    {
        Minimum = 1,
        Maximum = 120,
        Value = 18
    };
    private readonly ComboBox cboGioiTinh = new()
    {
        DropDownStyle = ComboBoxStyle.DropDownList
    };
    private readonly PictureBox picAnh = new()
    {
        Width = 100,
        Height = 100,
        SizeMode = PictureBoxSizeMode.Zoom,
        BorderStyle = BorderStyle.FixedSingle
    };

    private string duongDanAnh = "";

    public Form46()
    {
        Text = "Bài 4.6 - Quản lý sinh viên";
        Width = 1000;
        Height = 600;

        source.DataSource = students;
        grid.DataSource = source;
        grid.Dock = DockStyle.Fill;
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nu", "Khac" });
        cboGioiTinh.SelectedIndex = 0;

        var panel = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 180,
            ColumnCount = 4,
            RowCount = 3,
            Padding = new Padding(8)
        };

        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60));

        AddField(panel, "Mã SV:", txtMa, 0, 0);
        AddField(panel, "Họ tên:", txtTen, 0, 1);
        AddField(panel, "Tuổi:", numTuoi, 2, 0);
        AddField(panel, "Giới tính:", cboGioiTinh, 2, 1);

        var photoPanel = new FlowLayoutPanel { Dock = DockStyle.Fill };
        var btnAnh = new Button { Text = "Chọn ảnh" };
        btnAnh.Click += (_, _) => ChonAnh();
        photoPanel.Controls.Add(picAnh);
        photoPanel.Controls.Add(btnAnh);
        panel.Controls.Add(photoPanel, 3, 2);

        var buttons = new FlowLayoutPanel { Dock = DockStyle.Fill };
        var btnThem = new Button { Text = "Thêm" };
        var btnSua = new Button { Text = "Sửa" };
        var btnXoa = new Button { Text = "Xóa" };
        var btnMoi = new Button { Text = "Làm mới" };
        var btnCsv = new Button { Text = "Xuất CSV" };

        buttons.Controls.AddRange(new Control[]
        {
            btnThem, btnSua, btnXoa, btnMoi, btnCsv
        });

        panel.Controls.Add(buttons, 0, 2);
        panel.SetColumnSpan(buttons, 3);

        Controls.Add(grid);
        Controls.Add(panel);

        btnThem.Click += (_, _) => Them();
        btnSua.Click += (_, _) => Sua();
        btnXoa.Click += (_, _) => Xoa();
        btnMoi.Click += (_, _) => LamMoi();
        btnCsv.Click += (_, _) => XuatCsv();

        grid.SelectionChanged += (_, _) => NapDuLieuLenForm();

        var context = new ContextMenuStrip();
        context.Items.Add("Xóa sinh viên", null, (_, _) => Xoa());
        context.Items.Add("Xuất CSV", null, (_, _) => XuatCsv());
        grid.ContextMenuStrip = context;
    }

    private static void AddField(
        TableLayoutPanel panel, string label,
        Control control, int column, int row)
    {
        panel.Controls.Add(new Label
        {
            Text = label,
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleLeft
        }, column, row);

        control.Dock = DockStyle.Fill;
        panel.Controls.Add(control, column + 1, row);
    }

    private bool KiemTra(out string ma, out string ten)
    {
        ma = txtMa.Text.Trim();
        ten = txtTen.Text.Trim();

        if (ma == "" || ten == "")
        {
            MessageBox.Show("Vui lòng nhập mã sinh viên và họ tên.");
            return false;
        }

        return true;
    }

    private void Them()
    {
        if (!KiemTra(out string ma, out string ten))
            return;

        if (students.Any(s =>
            s.MaSV.Equals(ma, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show("Mã sinh viên đã tồn tại.");
            return;
        }

        students.Add(new Student46
        {
            MaSV = ma,
            HoTen = ten,
            Tuoi = (int)numTuoi.Value,
            GioiTinh = cboGioiTinh.Text,
            AnhDaiDien = duongDanAnh
        });

        source.ResetBindings(false);
        LamMoi();
    }

    private void Sua()
    {
        if (grid.CurrentRow?.DataBoundItem is not Student46 s)
        {
            MessageBox.Show("Hãy chọn sinh viên cần sửa.");
            return;
        }

        if (!KiemTra(out string ma, out string ten))
            return;

        if (students.Any(x => x != s &&
            x.MaSV.Equals(ma, StringComparison.OrdinalIgnoreCase)))
        {
            MessageBox.Show("Mã sinh viên đã tồn tại.");
            return;
        }

        s.MaSV = ma;
        s.HoTen = ten;
        s.Tuoi = (int)numTuoi.Value;
        s.GioiTinh = cboGioiTinh.Text;
        s.AnhDaiDien = duongDanAnh;

        source.ResetBindings(false);
    }

    private void Xoa()
    {
        if (grid.CurrentRow?.DataBoundItem is not Student46 s)
        {
            MessageBox.Show("Hãy chọn sinh viên cần xóa.");
            return;
        }

        if (MessageBox.Show(
            $"Xóa sinh viên {s.HoTen}?",
            "Xác nhận",
            MessageBoxButtons.YesNo) != DialogResult.Yes)
            return;

        students.Remove(s);
        source.ResetBindings(false);
        LamMoi();
    }

    private void NapDuLieuLenForm()
    {
        if (grid.CurrentRow?.DataBoundItem is not Student46 s)
            return;

        txtMa.Text = s.MaSV;
        txtTen.Text = s.HoTen;
        numTuoi.Value = Math.Clamp(s.Tuoi, 1, 120);
        cboGioiTinh.SelectedItem = s.GioiTinh;
        duongDanAnh = s.AnhDaiDien;

        HienThiAnh();
    }

    private void ChonAnh()
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "Ảnh|*.jpg;*.jpeg;*.png;*.bmp"
        };

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        duongDanAnh = dialog.FileName;
        HienThiAnh();
    }

    private void HienThiAnh()
    {
        if (picAnh.Image != null)
        {
            var old = picAnh.Image;
            picAnh.Image = null;
            old.Dispose();
        }

        if (!File.Exists(duongDanAnh))
            return;

        using var stream = File.OpenRead(duongDanAnh);
        using var image = Image.FromStream(stream);
        picAnh.Image = new Bitmap(image);
    }

    private void LamMoi()
    {
        txtMa.Clear();
        txtTen.Clear();
        numTuoi.Value = 18;
        cboGioiTinh.SelectedIndex = 0;
        duongDanAnh = "";

        if (picAnh.Image != null)
        {
            var old = picAnh.Image;
            picAnh.Image = null;
            old.Dispose();
        }

        grid.ClearSelection();
        txtMa.Focus();
    }

    private void XuatCsv()
    {
        using var dialog = new SaveFileDialog
        {
            Filter = "CSV (*.csv)|*.csv",
            FileName = "SinhVien_Bai46.csv"
        };

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        var sb = new StringBuilder();
        sb.AppendLine("MaSV,HoTen,Tuoi,GioiTinh,AnhDaiDien");

        foreach (var s in students)
        {
            sb.AppendLine(
                $"{Csv(s.MaSV)},{Csv(s.HoTen)},{s.Tuoi}," +
                $"{Csv(s.GioiTinh)},{Csv(s.AnhDaiDien)}");
        }

        File.WriteAllText(
            dialog.FileName, sb.ToString(), new UTF8Encoding(true));

        MessageBox.Show("Đã xuất file CSV.");
    }

    private static string Csv(string value) =>
        "\"" + value.Replace("\"", "\"\"") + "\"";

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (picAnh.Image != null)
            picAnh.Image.Dispose();

        base.OnFormClosed(e);
    }
}
