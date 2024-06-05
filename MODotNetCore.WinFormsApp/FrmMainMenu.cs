namespace MODotNetCore.WinFormsApp;

public partial class FrmMainMenu : Form
{
    public FrmMainMenu()
    {
        InitializeComponent();
    }

    private void newBlogToolStripMenuItem_Click(object sender, EventArgs e)
    {
        FrmBlog frm = new FrmBlog();
        //frm.Show();
        frm.ShowDialog();
    }

    private void blogsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        FrmBlogList frm = new FrmBlogList();
        frm.ShowDialog();
    }
}
