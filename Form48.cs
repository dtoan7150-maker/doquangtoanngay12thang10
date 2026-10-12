
namespace BaiTap464748;

public class Form48 : Form
{
    private readonly Button btnSinhVien =
        new() { Text = "Tải sinh viên" };

    private readonly Button btnSanPham =
        new() { Text = "Tải sản phẩm" };

    private readonly ProgressBar progress = new()
    {
        Minimum = 0,
        Maximum = 100,
        Dock = DockStyle.Fill
    };

    private readonly Label lblStatus =
        new() { Text = "Sẵn sàng", AutoSize = true };

    private readonly DataGridView grid = new()
    {
        Dock = DockStyle.Fill,
        ReadOnly = true,
        AllowUserToAddRows = false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    };

    // Dữ liệu sinh viên riêng của Bài 4.8.
    private readonly List<StudentAsync> students = new()
    {
        new StudentAsync { MaSV = "A801", HoTen = "Hoang Van A", Tuoi = 20 },
        new StudentAsync { MaSV = "A802", HoTen = "Tran Thi B", Tuoi = 21 },
        new StudentAsync { MaSV = "A803", HoTen = "Nguyen Van C", Tuoi = 19 },
        new StudentAsync { MaSV = "A804", HoTen = "Le Thi D", Tuoi = 22 },
        new StudentAsync { MaSV = "A805", HoTen = "Pham Van E", Tuoi = 20 }
    };

    public Form48()
    {
        Text = "Bài 4.8 - Async/Await";
        Width = 850;
        Height = 500;

        var top = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 45,
            Padding = new Padding(5)
        };

        top.Controls.AddRange(new Control[]
        {
            btnSinhVien, btnSanPham
        });

        var bottom = new TableLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 70,
            ColumnCount = 1,
            RowCount = 2,
            Padding = new Padding(8)
        };

        bottom.RowStyles.Add(
            new RowStyle(SizeType.Percent, 60));

        bottom.RowStyles.Add(
            new RowStyle(SizeType.Percent, 40));

        bottom.Controls.Add(progress, 0, 0);
        bottom.Controls.Add(lblStatus, 0, 1);

        Controls.Add(grid);
        Controls.Add(bottom);
        Controls.Add(top);

        btnSinhVien.Click += async (_, _) =>
            await TaiSinhVienAsync();

        btnSanPham.Click += async (_, _) =>
            await TaiSanPhamAsync();
    }

    private async Task TaiSinhVienAsync()
    {
        SetBusy(true, "Đang tải sinh viên...");
        progress.Value = 0;

        try
        {
            var result = new List<StudentAsync>();

            for (int i = 0; i < students.Count; i++)
            {
                await Task.Delay(300);

                result.Add(students[i]);

                progress.Value =
                    (i + 1) * 100 / students.Count;

                lblStatus.Text =
                    $"Đang tải sinh viên {i + 1}/{students.Count}";
            }

            grid.DataSource = null;
            grid.DataSource = result;

            lblStatus.Text =
                $"Đã tải {result.Count} sinh viên.";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi tải sinh viên: " + ex.Message);
        }
        finally
        {
            SetBusy(false, lblStatus.Text);
        }
    }

    private async Task TaiSanPhamAsync()
    {
        SetBusy(true, "Đang tải sản phẩm...");
        progress.Value = 0;

        try
        {
            var products = new List<Product48>();

            for (int i = 1; i <= 10; i++)
            {
                await Task.Delay(200);

                products.Add(new Product48
                {
                    MaSP = i,
                    TenSP = "Sản phẩm " + i,
                    DonGia = i * 25000m,
                    SoLuong = i * 2
                });

                progress.Value = i * 10;

                lblStatus.Text =
                    $"Đang tải sản phẩm {i}/10";
            }

            grid.DataSource = null;
            grid.DataSource = products;

            lblStatus.Text =
                $"Đã tải {products.Count} sản phẩm.";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi tải sản phẩm: " + ex.Message);
        }
        finally
        {
            SetBusy(false, lblStatus.Text);
        }
    }

    private void SetBusy(bool busy, string message)
    {
        btnSinhVien.Enabled = !busy;
        btnSanPham.Enabled = !busy;
        lblStatus.Text = message;
    }

    private class StudentAsync
    {
        public string MaSV { get; set; } = "";
        public string HoTen { get; set; } = "";
        public int Tuoi { get; set; }
    }
}
