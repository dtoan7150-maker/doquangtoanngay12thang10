
namespace BaiTap464748;

public class FormMain : Form
{
    public FormMain()
    {
        Text = "CHƯƠNG TRÌNH BÀI TẬP 4.6 - 4.7 - 4.8";
        Width = 800;
        Height = 500;
        StartPosition = FormStartPosition.CenterScreen;

        var menu = new MenuStrip();
        var mBai = new ToolStripMenuItem("Chọn bài");

        mBai.DropDownItems.Add(
            "Bài 4.6 - Quản lý sinh viên",
            null,
            (_, _) => MoForm<Form46>());

        mBai.DropDownItems.Add(
            "Bài 4.7 - Giao diện MDI",
            null,
            (_, _) => MoForm<Form47>());

        mBai.DropDownItems.Add(
            "Bài 4.8 - Async/Await",
            null,
            (_, _) => MoForm<Form48>());

        mBai.DropDownItems.Add(new ToolStripSeparator());

        mBai.DropDownItems.Add(
            "Thoát",
            null,
            (_, _) => Close());

        menu.Items.Add(mBai);
        Controls.Add(menu);
        MainMenuStrip = menu;

        var title = new Label
        {
            Text = "VUI LÒNG CHỌN BÀI MUỐN THỰC HIỆN",
            Dock = DockStyle.Fill,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Arial", 18, FontStyle.Bold)
        };

        Controls.Add(title);
        title.BringToFront();
        menu.BringToFront();
    }

    private void MoForm<T>() where T : Form, new()
    {
        foreach (Form f in OwnedForms)
        {
            if (f is T && !f.IsDisposed)
            {
                f.Activate();
                if (f.WindowState == FormWindowState.Minimized)
                    f.WindowState = FormWindowState.Normal;
                return;
            }
        }

        var form = new T();
        form.StartPosition = FormStartPosition.CenterScreen;
        AddOwnedForm(form);
        form.Show();
    }
}
