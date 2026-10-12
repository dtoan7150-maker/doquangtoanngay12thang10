
namespace BaiTap464748;

public class Form47Child : Form
{
    public Form47Child()
    {
        Text = "Bài 4.7 - Danh sách sinh viên";
        Width = 600;
        Height = 350;

        var students = new List<Student47>
        {
            new Student47 { MaSV = "MDI01", HoTen = "Pham Van A", Diem = 8.5 },
            new Student47 { MaSV = "MDI02", HoTen = "Nguyen Thi B", Diem = 7.5 },
            new Student47 { MaSV = "MDI03", HoTen = "Le Van C", Diem = 9.0 }
        };

        var grid = new DataGridView
        {
            Dock = DockStyle.Fill,
            ReadOnly = true,
            AutoGenerateColumns = true,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            DataSource = students
        };

        Controls.Add(grid);
    }
}
