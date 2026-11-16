namespace KeyEventLogger;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;

    // Üst Panel Bileşenleri
    private Panel panelHeader;
    private Label lblTitle;
    private Label lblSubtitle;
    private Label lblSecurityNotice;

    // Kontrol Araç Çubuğu
    private Panel panelControls;
    private Button btnStart;
    private Button btnStop;
    private Button btnClear;
    private Button btnExport;
    private Panel panelStatusBadge;
    private Label lblStatusDot;
    private Label lblStatusText;
    private CheckBox chkAutoScroll;

    // Filtreleme ve İstatistik Paneli
    private GroupBox grpStatsAndFilter;
    private Label lblStatsTotal;
    private Label lblStatsKeyCount;
    private Label lblStatsTopKeys;
    private ComboBox cmbEventTypeFilter;
    private TextBox txtSearchKey;
    private DateTimePicker dtpStart;
    private DateTimePicker dtpEnd;
    private Button btnApplyFilter;
    private Button btnResetFilter;
    private Label lblFilterEvent;
    private Label lblFilterSearch;
    private Label lblFilterDateRange;

    // Sekmeli Görünüm (ListView ve RichTextBox)
    private TabControl tabDisplay;
    private TabPage tabListView;
    private TabPage tabRichText;
    private ListView lvEvents;
    private ColumnHeader colId;
    private ColumnHeader colTime;
    private ColumnHeader colType;
    private ColumnHeader colKeyName;
    private ColumnHeader colKeyValue;
    private ColumnHeader colChar;
    private ColumnHeader colModifiers;
    private RichTextBox rtbLog;

    // Alt Durum Çubuğu
    private StatusStrip statusStrip;
    private ToolStripStatusLabel statusLabelGeneral;
    private ToolStripStatusLabel statusLabelLastKey;
    private ToolStripStatusLabel statusLabelTotalEvents;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        panelHeader = new Panel();
        lblTitle = new Label();
        lblSubtitle = new Label();
        lblSecurityNotice = new Label();

        panelControls = new Panel();
        btnStart = new Button();
        btnStop = new Button();
        btnClear = new Button();
        btnExport = new Button();
        panelStatusBadge = new Panel();
        lblStatusDot = new Label();
        lblStatusText = new Label();
        chkAutoScroll = new CheckBox();

        grpStatsAndFilter = new GroupBox();
        lblStatsTotal = new Label();
        lblStatsKeyCount = new Label();
        lblStatsTopKeys = new Label();
        lblFilterEvent = new Label();
        cmbEventTypeFilter = new ComboBox();
        lblFilterSearch = new Label();
        txtSearchKey = new TextBox();
        lblFilterDateRange = new Label();
        dtpStart = new DateTimePicker();
        dtpEnd = new DateTimePicker();
        btnApplyFilter = new Button();
        btnResetFilter = new Button();

        tabDisplay = new TabControl();
        tabListView = new TabPage();
        lvEvents = new ListView();
        colId = new ColumnHeader();
        colTime = new ColumnHeader();
        colType = new ColumnHeader();
        colKeyName = new ColumnHeader();
        colKeyValue = new ColumnHeader();
        colChar = new ColumnHeader();
        colModifiers = new ColumnHeader();

        tabRichText = new TabPage();
        rtbLog = new RichTextBox();

        statusStrip = new StatusStrip();
        statusLabelGeneral = new ToolStripStatusLabel();
        statusLabelLastKey = new ToolStripStatusLabel();
        statusLabelTotalEvents = new ToolStripStatusLabel();

        panelHeader.SuspendLayout();
        panelControls.SuspendLayout();
        panelStatusBadge.SuspendLayout();
        grpStatsAndFilter.SuspendLayout();
        tabDisplay.SuspendLayout();
        tabListView.SuspendLayout();
        tabRichText.SuspendLayout();
        statusStrip.SuspendLayout();
        SuspendLayout();

        // 
        // panelHeader
        // 
        panelHeader.BackColor = Color.FromArgb(30, 41, 59);
        panelHeader.Controls.Add(lblTitle);
        panelHeader.Controls.Add(lblSubtitle);
        panelHeader.Controls.Add(lblSecurityNotice);
        panelHeader.Dock = DockStyle.Top;
        panelHeader.Location = new Point(0, 0);
        panelHeader.Name = "panelHeader";
        panelHeader.Padding = new Padding(16, 12, 16, 12);
        panelHeader.Size = new Size(1060, 75);
        panelHeader.TabIndex = 0;

        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(14, 10);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(330, 25);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "⌨️ Key Event Logger (Eğitsel Takip)";

        // 
        // lblSubtitle
        // 
        lblSubtitle.AutoSize = true;
        lblSubtitle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        lblSubtitle.ForeColor = Color.FromArgb(203, 213, 225);
        lblSubtitle.Location = new Point(16, 38);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(465, 15);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "KeyDown, KeyUp ve KeyPress Olaylarının İşlenmesi ve Dosya Kaydı (StreamWriter)";

        // 
        // lblSecurityNotice
        // 
        lblSecurityNotice.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblSecurityNotice.BackColor = Color.FromArgb(15, 23, 42);
        lblSecurityNotice.BorderStyle = BorderStyle.FixedSingle;
        lblSecurityNotice.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
        lblSecurityNotice.ForeColor = Color.FromArgb(56, 189, 248);
        lblSecurityNotice.Location = new Point(730, 15);
        lblSecurityNotice.Name = "lblSecurityNotice";
        lblSecurityNotice.Padding = new Padding(8, 4, 8, 4);
        lblSecurityNotice.Size = new Size(315, 42);
        lblSecurityNotice.TabIndex = 2;
        lblSecurityNotice.Text = "🛡️ Güvenlik Kapsamı: Yalnızca bu form odaktayken çalışır.\r\nSistem geneli (global hook) izleme yapılmaz.";
        lblSecurityNotice.TextAlign = ContentAlignment.MiddleCenter;

        // 
        // panelControls
        // 
        panelControls.BackColor = Color.FromArgb(248, 250, 252);
        panelControls.Controls.Add(btnStart);
        panelControls.Controls.Add(btnStop);
        panelControls.Controls.Add(btnClear);
        panelControls.Controls.Add(btnExport);
        panelControls.Controls.Add(panelStatusBadge);
        panelControls.Controls.Add(chkAutoScroll);
        panelControls.Dock = DockStyle.Top;
        panelControls.Location = new Point(0, 75);
        panelControls.Name = "panelControls";
        panelControls.Padding = new Padding(12);
        panelControls.Size = new Size(1060, 56);
        panelControls.TabIndex = 1;

        // 
        // btnStart
        // 
        btnStart.BackColor = Color.FromArgb(22, 163, 74);
        btnStart.Cursor = Cursors.Hand;
        btnStart.FlatAppearance.BorderSize = 0;
        btnStart.FlatStyle = FlatStyle.Flat;
        btnStart.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        btnStart.ForeColor = Color.White;
        btnStart.Location = new Point(14, 10);
        btnStart.Name = "btnStart";
        btnStart.Size = new Size(105, 34);
        btnStart.TabIndex = 0;
        btnStart.Text = "▶ Başlat";
        btnStart.UseVisualStyleBackColor = false;
        btnStart.Click += btnStart_Click;

        // 
        // btnStop
        // 
        btnStop.BackColor = Color.FromArgb(220, 38, 38);
        btnStop.Cursor = Cursors.Hand;
        btnStop.Enabled = false;
        btnStop.FlatAppearance.BorderSize = 0;
        btnStop.FlatStyle = FlatStyle.Flat;
        btnStop.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        btnStop.ForeColor = Color.White;
        btnStop.Location = new Point(127, 10);
        btnStop.Name = "btnStop";
        btnStop.Size = new Size(105, 34);
        btnStop.TabIndex = 1;
        btnStop.Text = "⏹ Durdur";
        btnStop.UseVisualStyleBackColor = false;
        btnStop.Click += btnStop_Click;

        // 
        // btnClear
        // 
        btnClear.BackColor = Color.FromArgb(71, 85, 105);
        btnClear.Cursor = Cursors.Hand;
        btnClear.FlatAppearance.BorderSize = 0;
        btnClear.FlatStyle = FlatStyle.Flat;
        btnClear.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        btnClear.ForeColor = Color.White;
        btnClear.Location = new Point(240, 10);
        btnClear.Name = "btnClear";
        btnClear.Size = new Size(105, 34);
        btnClear.TabIndex = 2;
        btnClear.Text = "🗑️ Temizle";
        btnClear.UseVisualStyleBackColor = false;
        btnClear.Click += btnClear_Click;

        // 
        // btnExport
        // 
        btnExport.BackColor = Color.FromArgb(37, 99, 235);
        btnExport.Cursor = Cursors.Hand;
        btnExport.FlatAppearance.BorderSize = 0;
        btnExport.FlatStyle = FlatStyle.Flat;
        btnExport.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        btnExport.ForeColor = Color.White;
        btnExport.Location = new Point(353, 10);
        btnExport.Name = "btnExport";
        btnExport.Size = new Size(140, 34);
        btnExport.TabIndex = 3;
        btnExport.Text = "💾 Dosyaya Kaydet";
        btnExport.UseVisualStyleBackColor = false;
        btnExport.Click += btnExport_Click;

        // 
        // panelStatusBadge
        // 
        panelStatusBadge.BackColor = Color.FromArgb(254, 226, 226);
        panelStatusBadge.BorderStyle = BorderStyle.FixedSingle;
        panelStatusBadge.Controls.Add(lblStatusDot);
        panelStatusBadge.Controls.Add(lblStatusText);
        panelStatusBadge.Location = new Point(510, 10);
        panelStatusBadge.Name = "panelStatusBadge";
        panelStatusBadge.Size = new Size(200, 34);
        panelStatusBadge.TabIndex = 4;

        // 
        // lblStatusDot
        // 
        lblStatusDot.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
        lblStatusDot.ForeColor = Color.FromArgb(220, 38, 38);
        lblStatusDot.Location = new Point(6, 4);
        lblStatusDot.Name = "lblStatusDot";
        lblStatusDot.Size = new Size(22, 22);
        lblStatusDot.TabIndex = 0;
        lblStatusDot.Text = "●";
        lblStatusDot.TextAlign = ContentAlignment.MiddleCenter;

        // 
        // lblStatusText
        // 
        lblStatusText.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
        lblStatusText.ForeColor = Color.FromArgb(185, 28, 28);
        lblStatusText.Location = new Point(28, 4);
        lblStatusText.Name = "lblStatusText";
        lblStatusText.Size = new Size(160, 22);
        lblStatusText.TabIndex = 1;
        lblStatusText.Text = "DURDURULDU (PASİF)";
        lblStatusText.TextAlign = ContentAlignment.MiddleLeft;

        // 
        // chkAutoScroll
        // 
        chkAutoScroll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        chkAutoScroll.AutoSize = true;
        chkAutoScroll.Checked = true;
        chkAutoScroll.CheckState = CheckState.Checked;
        chkAutoScroll.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        chkAutoScroll.Location = new Point(915, 18);
        chkAutoScroll.Name = "chkAutoScroll";
        chkAutoScroll.Size = new Size(130, 19);
        chkAutoScroll.TabIndex = 5;
        chkAutoScroll.Text = "Otomatik Aşağı Kaydır";
        chkAutoScroll.UseVisualStyleBackColor = true;

        // 
        // grpStatsAndFilter
        // 
        grpStatsAndFilter.BackColor = Color.White;
        grpStatsAndFilter.Controls.Add(lblStatsTotal);
        grpStatsAndFilter.Controls.Add(lblStatsKeyCount);
        grpStatsAndFilter.Controls.Add(lblStatsTopKeys);
        grpStatsAndFilter.Controls.Add(lblFilterEvent);
        grpStatsAndFilter.Controls.Add(cmbEventTypeFilter);
        grpStatsAndFilter.Controls.Add(lblFilterSearch);
        grpStatsAndFilter.Controls.Add(txtSearchKey);
        grpStatsAndFilter.Controls.Add(lblFilterDateRange);
        grpStatsAndFilter.Controls.Add(dtpStart);
        grpStatsAndFilter.Controls.Add(dtpEnd);
        grpStatsAndFilter.Controls.Add(btnApplyFilter);
        grpStatsAndFilter.Controls.Add(btnResetFilter);
        grpStatsAndFilter.Dock = DockStyle.Top;
        grpStatsAndFilter.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        grpStatsAndFilter.ForeColor = Color.FromArgb(51, 65, 85);
        grpStatsAndFilter.Location = new Point(0, 131);
        grpStatsAndFilter.Name = "grpStatsAndFilter";
        grpStatsAndFilter.Padding = new Padding(12, 8, 12, 8);
        grpStatsAndFilter.Size = new Size(1060, 95);
        grpStatsAndFilter.TabIndex = 2;
        grpStatsAndFilter.TabStop = false;
        grpStatsAndFilter.Text = "📊 İstatistikler ve Filtreleme (İsteğe Bağlı Özellikler)";

        // 
        // lblStatsTotal
        // 
        lblStatsTotal.AutoSize = true;
        lblStatsTotal.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        lblStatsTotal.ForeColor = Color.FromArgb(30, 41, 59);
        lblStatsTotal.Location = new Point(14, 25);
        lblStatsTotal.Name = "lblStatsTotal";
        lblStatsTotal.Size = new Size(116, 15);
        lblStatsTotal.TabIndex = 0;
        lblStatsTotal.Text = "Toplam Olay Kaydı: 0";

        // 
        // lblStatsKeyCount
        // 
        lblStatsKeyCount.AutoSize = true;
        lblStatsKeyCount.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        lblStatsKeyCount.ForeColor = Color.FromArgb(30, 41, 59);
        lblStatsKeyCount.Location = new Point(170, 25);
        lblStatsKeyCount.Name = "lblStatsKeyCount";
        lblStatsKeyCount.Size = new Size(130, 15);
        lblStatsKeyCount.TabIndex = 1;
        lblStatsKeyCount.Text = "Toplam Tuş Vuruşu: 0";

        // 
        // lblStatsTopKeys
        // 
        lblStatsTopKeys.AutoSize = true;
        lblStatsTopKeys.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        lblStatsTopKeys.ForeColor = Color.FromArgb(30, 41, 59);
        lblStatsTopKeys.Location = new Point(340, 25);
        lblStatsTopKeys.Name = "lblStatsTopKeys";
        lblStatsTopKeys.Size = new Size(205, 15);
        lblStatsTopKeys.TabIndex = 2;
        lblStatsTopKeys.Text = "En Çok Basılanlar: (Henüz veri yok)";

        // 
        // lblFilterEvent
        // 
        lblFilterEvent.AutoSize = true;
        lblFilterEvent.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
        lblFilterEvent.Location = new Point(14, 60);
        lblFilterEvent.Name = "lblFilterEvent";
        lblFilterEvent.Size = new Size(59, 13);
        lblFilterEvent.TabIndex = 3;
        lblFilterEvent.Text = "Olay Türü:";

        // 
        // cmbEventTypeFilter
        // 
        cmbEventTypeFilter.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbEventTypeFilter.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
        cmbEventTypeFilter.FormattingEnabled = true;
        cmbEventTypeFilter.Items.AddRange(new object[] { "Tümü", "KeyDown", "KeyUp", "KeyPress" });
        cmbEventTypeFilter.Location = new Point(78, 56);
        cmbEventTypeFilter.Name = "cmbEventTypeFilter";
        cmbEventTypeFilter.Size = new Size(95, 21);
        cmbEventTypeFilter.TabIndex = 4;

        // 
        // lblFilterSearch
        // 
        lblFilterSearch.AutoSize = true;
        lblFilterSearch.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
        lblFilterSearch.Location = new Point(185, 60);
        lblFilterSearch.Name = "lblFilterSearch";
        lblFilterSearch.Size = new Size(61, 13);
        lblFilterSearch.TabIndex = 5;
        lblFilterSearch.Text = "Tuş Arama:";

        // 
        // txtSearchKey
        // 
        txtSearchKey.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
        txtSearchKey.Location = new Point(250, 56);
        txtSearchKey.Name = "txtSearchKey";
        txtSearchKey.PlaceholderText = "Örn: Enter, Shift, A...";
        txtSearchKey.Size = new Size(120, 22);
        txtSearchKey.TabIndex = 6;

        // 
        // lblFilterDateRange
        // 
        lblFilterDateRange.AutoSize = true;
        lblFilterDateRange.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
        lblFilterDateRange.Location = new Point(385, 60);
        lblFilterDateRange.Name = "lblFilterDateRange";
        lblFilterDateRange.Size = new Size(77, 13);
        lblFilterDateRange.TabIndex = 7;
        lblFilterDateRange.Text = "Tarih Aralığı:";

        // 
        // dtpStart
        // 
        dtpStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        dtpStart.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
        dtpStart.Format = DateTimePickerFormat.Custom;
        dtpStart.Location = new Point(468, 56);
        dtpStart.Name = "dtpStart";
        dtpStart.ShowCheckBox = true;
        dtpStart.Checked = false;
        dtpStart.Size = new Size(155, 22);
        dtpStart.TabIndex = 8;

        // 
        // dtpEnd
        // 
        dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        dtpEnd.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
        dtpEnd.Format = DateTimePickerFormat.Custom;
        dtpEnd.Location = new Point(630, 56);
        dtpEnd.Name = "dtpEnd";
        dtpEnd.ShowCheckBox = true;
        dtpEnd.Checked = false;
        dtpEnd.Size = new Size(155, 22);
        dtpEnd.TabIndex = 9;

        // 
        // btnApplyFilter
        // 
        btnApplyFilter.BackColor = Color.FromArgb(59, 130, 246);
        btnApplyFilter.Cursor = Cursors.Hand;
        btnApplyFilter.FlatAppearance.BorderSize = 0;
        btnApplyFilter.FlatStyle = FlatStyle.Flat;
        btnApplyFilter.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
        btnApplyFilter.ForeColor = Color.White;
        btnApplyFilter.Location = new Point(795, 54);
        btnApplyFilter.Name = "btnApplyFilter";
        btnApplyFilter.Size = new Size(80, 26);
        btnApplyFilter.TabIndex = 10;
        btnApplyFilter.Text = "🔍 Filtrele";
        btnApplyFilter.UseVisualStyleBackColor = false;
        btnApplyFilter.Click += btnApplyFilter_Click;

        // 
        // btnResetFilter
        // 
        btnResetFilter.BackColor = Color.FromArgb(148, 163, 184);
        btnResetFilter.Cursor = Cursors.Hand;
        btnResetFilter.FlatAppearance.BorderSize = 0;
        btnResetFilter.FlatStyle = FlatStyle.Flat;
        btnResetFilter.Font = new Font("Segoe UI", 8.25F, FontStyle.Bold, GraphicsUnit.Point);
        btnResetFilter.ForeColor = Color.White;
        btnResetFilter.Location = new Point(882, 54);
        btnResetFilter.Name = "btnResetFilter";
        btnResetFilter.Size = new Size(80, 26);
        btnResetFilter.TabIndex = 11;
        btnResetFilter.Text = "🔄 Sıfırla";
        btnResetFilter.UseVisualStyleBackColor = false;
        btnResetFilter.Click += btnResetFilter_Click;

        // 
        // tabDisplay
        // 
        tabDisplay.Controls.Add(tabListView);
        tabDisplay.Controls.Add(tabRichText);
        tabDisplay.Dock = DockStyle.Fill;
        tabDisplay.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        tabDisplay.Location = new Point(0, 226);
        tabDisplay.Name = "tabDisplay";
        tabDisplay.SelectedIndex = 0;
        tabDisplay.Size = new Size(1060, 420);
        tabDisplay.TabIndex = 3;

        // 
        // tabListView
        // 
        tabListView.Controls.Add(lvEvents);
        tabListView.Location = new Point(4, 24);
        tabListView.Name = "tabListView";
        tabListView.Padding = new Padding(3);
        tabListView.Size = new Size(1052, 392);
        tabListView.TabIndex = 0;
        tabListView.Text = "📋 Tablo Görünümü (ListView)";
        tabListView.UseVisualStyleBackColor = true;

        // 
        // lvEvents
        // 
        lvEvents.Columns.AddRange(new ColumnHeader[] {
            colId,
            colTime,
            colType,
            colKeyName,
            colKeyValue,
            colChar,
            colModifiers
        });
        lvEvents.Dock = DockStyle.Fill;
        lvEvents.FullRowSelect = true;
        lvEvents.GridLines = true;
        lvEvents.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        lvEvents.Location = new Point(3, 3);
        lvEvents.Name = "lvEvents";
        lvEvents.Size = new Size(1046, 386);
        lvEvents.TabIndex = 0;
        lvEvents.UseCompatibleStateImageBehavior = false;
        lvEvents.View = View.Details;

        // 
        // colId
        // 
        colId.Text = "#";
        colId.Width = 50;

        // 
        // colTime
        // 
        colTime.Text = "Zaman Damgası";
        colTime.Width = 180;

        // 
        // colType
        // 
        colType.Text = "Olay Türü";
        colType.Width = 100;

        // 
        // colKeyName
        // 
        colKeyName.Text = "Tuş Adı (KeyCode)";
        colKeyName.Width = 150;

        // 
        // colKeyValue
        // 
        colKeyValue.Text = "Sanal Kod (KeyValue)";
        colKeyValue.Width = 130;

        // 
        // colChar
        // 
        colChar.Text = "Karakter (KeyChar)";
        colChar.Width = 130;

        // 
        // colModifiers
        // 
        colModifiers.Text = "Değiştiriciler (Modifiers)";
        colModifiers.Width = 170;

        // 
        // tabRichText
        // 
        tabRichText.Controls.Add(rtbLog);
        tabRichText.Location = new Point(4, 24);
        tabRichText.Name = "tabRichText";
        tabRichText.Padding = new Padding(3);
        tabRichText.Size = new Size(1052, 392);
        tabRichText.TabIndex = 1;
        tabRichText.Text = "📄 Metin Günlüğü (RichTextBox)";
        tabRichText.UseVisualStyleBackColor = true;

        // 
        // rtbLog
        // 
        rtbLog.BackColor = Color.FromArgb(15, 23, 42);
        rtbLog.Dock = DockStyle.Fill;
        rtbLog.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
        rtbLog.ForeColor = Color.FromArgb(226, 232, 240);
        rtbLog.Location = new Point(3, 3);
        rtbLog.Name = "rtbLog";
        rtbLog.ReadOnly = true;
        rtbLog.Size = new Size(1046, 386);
        rtbLog.TabIndex = 0;
        rtbLog.Text = "";

        // 
        // statusStrip
        // 
        statusStrip.Items.AddRange(new ToolStripItem[] {
            statusLabelGeneral,
            statusLabelLastKey,
            statusLabelTotalEvents
        });
        statusStrip.Location = new Point(0, 646);
        statusStrip.Name = "statusStrip";
        statusStrip.Size = new Size(1060, 24);
        statusStrip.TabIndex = 4;

        // 
        // statusLabelGeneral
        // 
        statusLabelGeneral.Name = "statusLabelGeneral";
        statusLabelGeneral.Size = new Size(400, 19);
        statusLabelGeneral.Spring = true;
        statusLabelGeneral.Text = "Hazır - Başlat butonuna basarak olay kaydını başlatabilirsiniz.";
        statusLabelGeneral.TextAlign = ContentAlignment.MiddleLeft;

        // 
        // statusLabelLastKey
        // 
        statusLabelLastKey.BorderSides = ToolStripStatusLabelBorderSides.Left;
        statusLabelLastKey.Name = "statusLabelLastKey";
        statusLabelLastKey.Size = new Size(140, 19);
        statusLabelLastKey.Text = "Son Tuş: Yok";

        // 
        // statusLabelTotalEvents
        // 
        statusLabelTotalEvents.BorderSides = ToolStripStatusLabelBorderSides.Left;
        statusLabelTotalEvents.Name = "statusLabelTotalEvents";
        statusLabelTotalEvents.Size = new Size(110, 19);
        statusLabelTotalEvents.Text = "Toplam Olay: 0";

        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1060, 670);
        Controls.Add(tabDisplay);
        Controls.Add(grpStatsAndFilter);
        Controls.Add(panelControls);
        Controls.Add(panelHeader);
        Controls.Add(statusStrip);
        KeyPreview = true;
        MinimumSize = new Size(900, 600);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Key Event Logger — Eğitsel Klavye Olayı Takipçisi";
        panelHeader.ResumeLayout(false);
        panelHeader.PerformLayout();
        panelControls.ResumeLayout(false);
        panelControls.PerformLayout();
        panelStatusBadge.ResumeLayout(false);
        grpStatsAndFilter.ResumeLayout(false);
        grpStatsAndFilter.PerformLayout();
        tabDisplay.ResumeLayout(false);
        tabListView.ResumeLayout(false);
        tabRichText.ResumeLayout(false);
        statusStrip.ResumeLayout(false);
        statusStrip.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }
}
