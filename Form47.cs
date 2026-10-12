
namespace BaiTap464748;

public class Form47 : Form
{
    private readonly MenuStrip menu = new();
    private readonly ToolStrip tool = new();
    private readonly StatusStrip status = new();
    private readonly ToolStripStatusLabel lblMessage =
        new("Sẵn sàng");
    private readonly ToolStripStatusLabel lblClock = new();

    private readonly System.Windows.Forms.Timer timer = new();

    public Form47()
    {
        Text = "Bài 4.7 - Giao diện MDI";
        Width = 1000;
        Height = 650;
        IsMdiContainer = true;

        var mSystem = new ToolStripMenuItem("Hệ thống");
        mSystem.DropDownItems.Add(
            "Mở danh sách sinh viên",
            null,
            (_, _) => MoFormCon());

        mSystem.DropDownItems.Add(
            "Thoát bài 4.7",
            null,
            (_, _) => Close());

        var mWindow = new ToolStripMenuItem("Cửa sổ");
        mWindow.DropDownItems.Add(
            "Xếp tầng",
            null,
            (_, _) => LayoutMdi(MdiLayout.Cascade));

        mWindow.DropDownItems.Add(
            "Xếp ngang",
            null,
            (_, _) => LayoutMdi(MdiLayout.TileHorizontal));

        mWindow.DropDownItems.Add(
            "Xếp dọc",
            null,
            (_, _) => LayoutMdi(MdiLayout.TileVertical));

        menu.Items.AddRange(new ToolStripItem[]
        {
            mSystem, mWindow
        });

        menu.Dock = DockStyle.Top;

        tool.Items.Add(new ToolStripButton(
            "Danh sách sinh viên",
            null,
            (_, _) => MoFormCon()));

        tool.Items.Add(new ToolStripButton(
            "Xếp tầng",
            null,
            (_, _) => LayoutMdi(MdiLayout.Cascade)));

        tool.Dock = DockStyle.Top;

        status.Items.Add(
            new ToolStripStatusLabel("Người dùng: Admin"));

        status.Items.Add(
            new ToolStripStatusLabel { Spring = true });

        status.Items.Add(lblMessage);

        status.Items.Add(
            new ToolStripStatusLabel { Spring = true });

        status.Items.Add(lblClock);
        status.Dock = DockStyle.Bottom;

        Controls.Add(menu);
        Controls.Add(tool);
        Controls.Add(status);
        MainMenuStrip = menu;

        timer.Interval = 1000;
        timer.Tick += (_, _) =>
        {
            lblClock.Text = DateTime.Now.ToString(
                "dd/MM/yyyy HH:mm:ss");
        };

        timer.Start();
    }

    private void MoFormCon()
    {
        foreach (Form f in MdiChildren)
        {
            if (f is Form47Child)
            {
                f.Activate();
                return;
            }
        }

        var child = new Form47Child
        {
            MdiParent = this
        };

        child.Show();
        lblMessage.Text = "Đã mở danh sách sinh viên Bài 4.7";
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        timer.Stop();
        timer.Dispose();
        base.OnFormClosed(e);
    }
}
