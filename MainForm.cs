using KeyEventLogger.Models;
using KeyEventLogger.Services;

namespace KeyEventLogger;

public partial class MainForm : Form
{
    private readonly KeyEventManager _manager = new();
    private bool _isFilterApplied = false;

    public MainForm()
    {
        InitializeComponent();
        SetupCustomBehavior();
    }

    private void SetupCustomBehavior()
    {
        // Varsayılan filtre seçimi
        cmbEventTypeFilter.SelectedIndex = 0;

        // Olay yöneticisi bildirimlerini bağla
        _manager.OnKeyEventLogged += HandleKeyEventLogged;
        _manager.OnLoggingStateChanged += HandleLoggingStateChanged;
        _manager.OnLogsCleared += HandleLogsCleared;

        // Klavye olaylarını dinle (KeyPreview = true olduğu için form seviyesinde yakalanır)
        this.KeyDown += MainForm_KeyDown;
        this.KeyUp += MainForm_KeyUp;
        this.KeyPress += MainForm_KeyPress;

        // Çift arabelleğe alma (ListView titremesini önlemek için)
        SetDoubleBuffered(lvEvents);
    }

    #region Klavye Olay Dinleyicileri (KeyPreview = true)

    private void MainForm_KeyDown(object? sender, KeyEventArgs e)
    {
        // Sadece arama kutusundayken filtre araması için Enter tuşuna basıldığında filtreyi uygula
        if (txtSearchKey.Focused && e.KeyCode == Keys.Enter)
        {
            btnApplyFilter.PerformClick();
            e.Handled = true;
            return;
        }

        _manager.RecordKeyDown(e);
    }

    private void MainForm_KeyUp(object? sender, KeyEventArgs e)
    {
        _manager.RecordKeyUp(e);
    }

    private void MainForm_KeyPress(object? sender, KeyPressEventArgs e)
    {
        _manager.RecordKeyPress(e);
    }

    #endregion

    #region Olay Yöneticisi Dinleyicileri (Manager Events)

    private void HandleKeyEventLogged(KeyEventModel model)
    {
        // UI thread güvenliği (gerekirse invoke)
        if (InvokeRequired)
        {
            Invoke(new Action<KeyEventModel>(HandleKeyEventLogged), model);
            return;
        }

        // Eğer aktif bir filtre varsa ve gelen olay filtreye uymuyorsa arayüz listesine ekleme
        if (_isFilterApplied)
        {
            UpdateStatsUI();
            return;
        }

        AddEventToListView(model);
        AppendEventToRichTextBox(model);
        UpdateStatsUI();

        // Durum çubuğu güncelle
        statusLabelLastKey.Text = $"Son Tuş: {model.KeyName} ({model.EventType})";
        statusLabelTotalEvents.Text = $"Toplam Olay: {_manager.TotalEventsCount}";
    }

    private void HandleLoggingStateChanged(bool isActive)
    {
        if (InvokeRequired)
        {
            Invoke(new Action<bool>(HandleLoggingStateChanged), isActive);
            return;
        }

        if (isActive)
        {
            btnStart.Enabled = false;
            btnStop.Enabled = true;

            panelStatusBadge.BackColor = Color.FromArgb(220, 252, 231); // Açık yeşil
            lblStatusDot.ForeColor = Color.FromArgb(22, 163, 74); // Yeşil nokta
            lblStatusText.ForeColor = Color.FromArgb(21, 128, 61);
            lblStatusText.Text = "DİNLENİYOR (AKTİF)";

            statusLabelGeneral.Text = "🟢 Klavye dinleniyor... Tuş vuruşları bu pencere aktifken kaydediliyor.";
        }
        else
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;

            panelStatusBadge.BackColor = Color.FromArgb(254, 226, 226); // Açık kırmızı
            lblStatusDot.ForeColor = Color.FromArgb(220, 38, 38); // Kırmızı nokta
            lblStatusText.ForeColor = Color.FromArgb(185, 28, 28);
            lblStatusText.Text = "DURDURULDU (PASİF)";

            statusLabelGeneral.Text = "🔴 Dinleme durduruldu. Başlatmak için 'Başlat' butonuna tıklayınız.";
        }
    }

    private void HandleLogsCleared()
    {
        if (InvokeRequired)
        {
            Invoke(new Action(HandleLogsCleared));
            return;
        }

        lvEvents.Items.Clear();
        rtbLog.Clear();
        UpdateStatsUI();

        statusLabelLastKey.Text = "Son Tuş: Yok";
        statusLabelTotalEvents.Text = "Toplam Olay: 0";
        statusLabelGeneral.Text = "Tüm kayıtlar temizlendi.";
    }

    #endregion

    #region Arayüz Yardımcı Metotları

    private void AddEventToListView(KeyEventModel model)
    {
        var item = new ListViewItem(model.Id.ToString());
        item.SubItems.Add(model.Timestamp.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        item.SubItems.Add(model.EventType);
        item.SubItems.Add(model.KeyName);
        item.SubItems.Add(model.KeyValue.ToString());
        item.SubItems.Add(model.KeyChar ?? "-");
        item.SubItems.Add(model.Modifiers);

        // Olay türüne göre satır rengi
        switch (model.EventType)
        {
            case "KeyDown":
                item.ForeColor = Color.FromArgb(15, 23, 42); // Koyu lacivert
                break;
            case "KeyUp":
                item.ForeColor = Color.FromArgb(100, 116, 139); // Gri/Slate
                break;
            case "KeyPress":
                item.ForeColor = Color.FromArgb(13, 148, 136); // Teal
                break;
        }

        lvEvents.Items.Add(item);

        if (chkAutoScroll.Checked && lvEvents.Items.Count > 0)
        {
            lvEvents.Items[lvEvents.Items.Count - 1].EnsureVisible();
        }
    }

    private void AppendEventToRichTextBox(KeyEventModel model)
    {
        // Renkli satır ekleme
        Color logColor = model.EventType switch
        {
            "KeyDown" => Color.FromArgb(56, 189, 248),  // Açık mavi
            "KeyUp" => Color.FromArgb(148, 163, 184),    // Gri
            "KeyPress" => Color.FromArgb(74, 222, 128),  // Açık yeşil
            _ => Color.White
        };

        rtbLog.SelectionStart = rtbLog.TextLength;
        rtbLog.SelectionLength = 0;
        rtbLog.SelectionColor = logColor;
        rtbLog.AppendText(model.ToLogLine() + Environment.NewLine);
        rtbLog.SelectionColor = rtbLog.ForeColor;

        if (chkAutoScroll.Checked)
        {
            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.ScrollToCaret();
        }
    }

    private void UpdateStatsUI()
    {
        lblStatsTotal.Text = $"Toplam Olay: {_manager.TotalEventsCount}";
        lblStatsKeyCount.Text = $"Toplam Tuş Vuruşu: {_manager.TotalKeyDownCount}";

        var topKeys = _manager.GetTopKeys(5);
        if (topKeys.Count > 0)
        {
            string topList = string.Join(", ", topKeys.Select(k => $"{k.Key} ({k.Value})"));
            lblStatsTopKeys.Text = $"En Çok Basılanlar: {topList}";
        }
        else
        {
            lblStatsTopKeys.Text = "En Çok Basılanlar: (Henüz veri yok)";
        }
    }

    private static void SetDoubleBuffered(Control control)
    {
        typeof(Control).GetProperty("DoubleBuffered",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.SetValue(control, true, null);
    }

    #endregion

    #region Buton Olayları

    private void btnStart_Click(object? sender, EventArgs e)
    {
        _manager.StartLogging();
        this.Focus(); // Tuş yakalamanın başlaması için forma odaklan
    }

    private void btnStop_Click(object? sender, EventArgs e)
    {
        _manager.StopLogging();
    }

    private void btnClear_Click(object? sender, EventArgs e)
    {
        if (_manager.TotalEventsCount == 0)
        {
            MessageBox.Show("Silinecek herhangi bir kayıt bulunmamaktadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var dialogResult = MessageBox.Show(
            "Ekrandaki ve bellekteki tüm klavye olay kayıtları temizlensin mi?",
            "Kayıtları Temizle",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (dialogResult == DialogResult.Yes)
        {
            _manager.ClearLogs();
            _isFilterApplied = false;
        }
    }

    private void btnExport_Click(object? sender, EventArgs e)
    {
        var recordsToExport = _isFilterApplied
            ? GetCurrentFilteredList()
            : _manager.GetAllEvents().ToList();

        if (recordsToExport.Count == 0)
        {
            MessageBox.Show("Dışa aktarılacak herhangi bir klavye olayı bulunmamaktadır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        using var saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Metin Dosyası (*.txt)|*.txt|Tüm Dosyalar (*.*)|*.*";
        saveFileDialog.Title = "Klavye Günlüğünü Kaydet";
        saveFileDialog.FileName = $"KeyEventLog_{DateTime.Now:yyyyMMdd_HHmmss}.txt";

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            var result = _manager.ExportToTextFile(saveFileDialog.FileName, recordsToExport);
            if (result.Success)
            {
                MessageBox.Show(result.Message, "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                statusLabelGeneral.Text = $"Dosya kaydedildi: {Path.GetFileName(saveFileDialog.FileName)}";
            }
            else
            {
                MessageBox.Show(result.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private void btnApplyFilter_Click(object? sender, EventArgs e)
    {
        DateTime? start = dtpStart.Checked ? dtpStart.Value : null;
        DateTime? end = dtpEnd.Checked ? dtpEnd.Value : null;
        string? eventType = cmbEventTypeFilter.SelectedItem?.ToString();
        string? search = txtSearchKey.Text;

        var filtered = _manager.FilterEvents(start, end, eventType, search);
        _isFilterApplied = true;

        PopulateList(filtered);
        statusLabelGeneral.Text = $"🔍 Filtre uygulandı: {filtered.Count} kayıt bulundu.";
    }

    private void btnResetFilter_Click(object? sender, EventArgs e)
    {
        dtpStart.Checked = false;
        dtpEnd.Checked = false;
        cmbEventTypeFilter.SelectedIndex = 0;
        txtSearchKey.Clear();
        _isFilterApplied = false;

        PopulateList(_manager.GetAllEvents());
        statusLabelGeneral.Text = "Filtre sıfırlandı. Tüm kayıtlar gösteriliyor.";
    }

    private List<KeyEventModel> GetCurrentFilteredList()
    {
        DateTime? start = dtpStart.Checked ? dtpStart.Value : null;
        DateTime? end = dtpEnd.Checked ? dtpEnd.Value : null;
        string? eventType = cmbEventTypeFilter.SelectedItem?.ToString();
        string? search = txtSearchKey.Text;

        return _manager.FilterEvents(start, end, eventType, search);
    }

    private void PopulateList(IEnumerable<KeyEventModel> list)
    {
        lvEvents.BeginUpdate();
        lvEvents.Items.Clear();
        rtbLog.Clear();

        foreach (var item in list)
        {
            AddEventToListView(item);
            AppendEventToRichTextBox(item);
        }

        lvEvents.EndUpdate();
    }

    #endregion

    #region Demo ve Ekran Görüntüsü Üretimi

    public KeyEventManager EventManager => _manager;

    public void SimulateDemoData()
    {
        _manager.StartLogging();

        var demoKeys = new (Keys Key, char? Char, Keys Modifiers)[]
        {
            (Keys.H, 'H', Keys.Shift),
            (Keys.E, 'e', Keys.None),
            (Keys.L, 'l', Keys.None),
            (Keys.L, 'l', Keys.None),
            (Keys.O, 'o', Keys.None),
            (Keys.Space, ' ', Keys.None),
            (Keys.K, 'K', Keys.Shift),
            (Keys.E, 'e', Keys.None),
            (Keys.Y, 'y', Keys.None),
            (Keys.Enter, '\r', Keys.None),
            (Keys.S, 's', Keys.Control),
            (Keys.ControlKey, null, Keys.Control),
            (Keys.A, 'a', Keys.Control),
            (Keys.C, 'c', Keys.Control),
            (Keys.F5, null, Keys.None),
            (Keys.Escape, null, Keys.None)
        };

        foreach (var (key, ch, mod) in demoKeys)
        {
            var downArgs = new KeyEventArgs(key | mod);
            _manager.RecordKeyDown(downArgs);

            if (ch.HasValue)
            {
                var pressArgs = new KeyPressEventArgs(ch.Value);
                _manager.RecordKeyPress(pressArgs);
            }

            var upArgs = new KeyEventArgs(key | mod);
            _manager.RecordKeyUp(upArgs);
        }
    }

    public void SwitchToRichTextTab()
    {
        tabDisplay.SelectedTab = tabRichText;
    }

    public void SwitchToListViewTab()
    {
        tabDisplay.SelectedTab = tabListView;
    }

    #endregion
}
