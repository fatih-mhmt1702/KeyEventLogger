using System.Drawing;
using System.Drawing.Imaging;

namespace KeyEventLogger;

static class Program
{
    /// <summary>
    /// Uygulamanın ana giriş noktası.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        ApplicationConfiguration.Initialize();

        if (args.Length > 0 && args[0] == "--generate-screenshots")
        {
            GenerateScreenshots();
            return;
        }

        Application.Run(new MainForm());
    }

    private static void GenerateScreenshots()
    {
        string dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "screenshots");
        Directory.CreateDirectory(dir);

        using var form = new MainForm();
        form.Show();
        Application.DoEvents();

        // 1. Ekran Görüntüsü: Aktif Dinleme ve Tablo Görünümü
        form.SimulateDemoData();
        Application.DoEvents();

        using (var bmp1 = new Bitmap(form.Width, form.Height))
        {
            form.DrawToBitmap(bmp1, new Rectangle(0, 0, form.Width, form.Height));
            bmp1.Save(Path.Combine(dir, "01_tablo_ve_aktif_kayit.png"), ImageFormat.Png);
        }

        // 2. Ekran Görüntüsü: Metin Günlüğü (RichTextBox) ve İstatistikler
        form.SwitchToRichTextTab();
        Application.DoEvents();

        using (var bmp2 = new Bitmap(form.Width, form.Height))
        {
            form.DrawToBitmap(bmp2, new Rectangle(0, 0, form.Width, form.Height));
            bmp2.Save(Path.Combine(dir, "02_metin_gunlugu_ve_istatistik.png"), ImageFormat.Png);
        }

        form.Close();
    }
}
