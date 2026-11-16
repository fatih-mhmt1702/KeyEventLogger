using System.Text;
using KeyEventLogger.Models;

namespace KeyEventLogger.Services;

/// <summary>
/// Klavye olaylarını yöneten, bellekte tutan, istatistik çıkaran ve
/// dosya işlemlerini (StreamWriter) gerçekleştiren servis sınıfı.
/// </summary>
public class KeyEventManager
{
    private readonly List<KeyEventModel> _events = new();
    private readonly Dictionary<string, int> _keyFrequencies = new(StringComparer.OrdinalIgnoreCase);
    private int _nextId = 1;

    /// <summary>
    /// Kayıt durumunun aktif olup olmadığını belirtir.
    /// </summary>
    public bool IsLoggingActive { get; private set; }

    /// <summary>
    /// Toplam kaydedilen olay sayısı.
    /// </summary>
    public int TotalEventsCount => _events.Count;

    /// <summary>
    /// Sadece tuş vuruşu (KeyDown) sayısı.
    /// </summary>
    public int TotalKeyDownCount { get; private set; }

    /// <summary>
    /// Yeni bir olay eklendiğinde tetiklenen bildirim.
    /// </summary>
    public event Action<KeyEventModel>? OnKeyEventLogged;

    /// <summary>
    /// Kayıt durumu (Başlat/Durdur) değiştiğinde tetiklenir.
    /// </summary>
    public event Action<bool>? OnLoggingStateChanged;

    /// <summary>
    /// Tüm kayıtlar temizlendiğinde tetiklenir.
    /// </summary>
    public event Action? OnLogsCleared;

    /// <summary>
    /// Dinlemeyi başlatır.
    /// </summary>
    public void StartLogging()
    {
        if (!IsLoggingActive)
        {
            IsLoggingActive = true;
            OnLoggingStateChanged?.Invoke(true);
        }
    }

    /// <summary>
    /// Dinlemeyi durdurur.
    /// </summary>
    public void StopLogging()
    {
        if (IsLoggingActive)
        {
            IsLoggingActive = false;
            OnLoggingStateChanged?.Invoke(false);
        }
    }

    /// <summary>
    /// KeyDown olayını kaydeder.
    /// </summary>
    public void RecordKeyDown(KeyEventArgs e)
    {
        if (!IsLoggingActive) return;

        string modifiers = FormatModifiers(e.Modifiers);
        var model = new KeyEventModel
        {
            Id = _nextId++,
            Timestamp = DateTime.Now,
            EventType = "KeyDown",
            KeyName = e.KeyCode.ToString(),
            KeyValue = e.KeyValue,
            KeyChar = null,
            Modifiers = modifiers
        };

        AddEvent(model);
        TotalKeyDownCount++;

        // İstatistik güncelle
        string keyName = model.KeyName;
        if (_keyFrequencies.ContainsKey(keyName))
            _keyFrequencies[keyName]++;
        else
            _keyFrequencies[keyName] = 1;
    }

    /// <summary>
    /// KeyUp olayını kaydeder.
    /// </summary>
    public void RecordKeyUp(KeyEventArgs e)
    {
        if (!IsLoggingActive) return;

        string modifiers = FormatModifiers(e.Modifiers);
        var model = new KeyEventModel
        {
            Id = _nextId++,
            Timestamp = DateTime.Now,
            EventType = "KeyUp",
            KeyName = e.KeyCode.ToString(),
            KeyValue = e.KeyValue,
            KeyChar = null,
            Modifiers = modifiers
        };

        AddEvent(model);
    }

    /// <summary>
    /// KeyPress olayını kaydeder.
    /// </summary>
    public void RecordKeyPress(KeyPressEventArgs e)
    {
        if (!IsLoggingActive) return;

        // Kontrol karakterlerini okunabilir göster
        string charRepresentation = char.IsControl(e.KeyChar)
            ? $"Ctrl(0x{(int)e.KeyChar:X2})"
            : e.KeyChar.ToString();

        var model = new KeyEventModel
        {
            Id = _nextId++,
            Timestamp = DateTime.Now,
            EventType = "KeyPress",
            KeyName = charRepresentation,
            KeyValue = (int)e.KeyChar,
            KeyChar = charRepresentation,
            Modifiers = "Yok"
        };

        AddEvent(model);
    }

    private void AddEvent(KeyEventModel model)
    {
        _events.Add(model);
        OnKeyEventLogged?.Invoke(model);
    }

    /// <summary>
    /// Tüm kayıtları ve istatistikleri sıfırlar.
    /// </summary>
    public void ClearLogs()
    {
        _events.Clear();
        _keyFrequencies.Clear();
        TotalKeyDownCount = 0;
        _nextId = 1;
        OnLogsCleared?.Invoke();
    }

    /// <summary>
    /// Mevcut tüm kayıtların kopyasını döndürür.
    /// </summary>
    public IReadOnlyList<KeyEventModel> GetAllEvents() => _events.AsReadOnly();

    /// <summary>
    /// En çok basılan tuşları ve sayılarını getirir.
    /// </summary>
    public List<KeyValuePair<string, int>> GetTopKeys(int topCount = 5)
    {
        return _keyFrequencies
            .OrderByDescending(kv => kv.Value)
            .Take(topCount)
            .ToList();
    }

    /// <summary>
    /// Belirtilen kriterlere göre filtreleme yapar.
    /// </summary>
    public List<KeyEventModel> FilterEvents(DateTime? startDate, DateTime? endDate, string? eventType, string? searchKey)
    {
        var query = _events.AsEnumerable();

        if (startDate.HasValue)
            query = query.Where(x => x.Timestamp >= startDate.Value);

        if (endDate.HasValue)
            query = query.Where(x => x.Timestamp <= endDate.Value);

        if (!string.IsNullOrWhiteSpace(eventType) && eventType != "Tümü")
            query = query.Where(x => string.Equals(x.EventType, eventType, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(searchKey))
        {
            string search = searchKey.Trim();
            query = query.Where(x =>
                x.KeyName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                (x.KeyChar != null && x.KeyChar.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                x.Modifiers.Contains(search, StringComparison.OrdinalIgnoreCase)
            );
        }

        return query.ToList();
    }

    /// <summary>
    /// Kayıtları StreamWriter kullanarak bir .txt dosyasına güvenle kaydeder.
    /// </summary>
    public (bool Success, string Message) ExportToTextFile(string filePath, IEnumerable<KeyEventModel>? customList = null)
    {
        var recordsToExport = (customList ?? _events).ToList();

        if (recordsToExport.Count == 0)
        {
            return (false, "Kaydedilecek herhangi bir klavye olayı bulunmuyor.");
        }

        try
        {
            using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                writer.WriteLine("==========================================================================================");
                writer.WriteLine("                      KEY EVENT LOGGER - GÜNLÜK RAPORU                                   ");
                writer.WriteLine("  Eğitsel Amaçlı Klavye Olay Takip Kayıtları (Yalnızca Form Odaklı)                      ");
                writer.WriteLine($"  Oluşturulma Tarihi : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                writer.WriteLine($"  Toplam Olay Sayısı : {recordsToExport.Count}");
                writer.WriteLine("==========================================================================================");
                writer.WriteLine();
                writer.WriteLine($"{"ZAMAN",-23}\t{"OLAY TÜRÜ",-10}\t{"TUŞ ADI",-14}\t{"SANAL KOD",-10}\t{"KARAKTER",-12}\t{"DEĞİŞTİRİCİ"}");
                writer.WriteLine(new string('-', 95));

                foreach (var item in recordsToExport)
                {
                    writer.WriteLine(item.ToExportLine());
                }

                writer.WriteLine();
                writer.WriteLine("==========================================================================================");
                writer.WriteLine("                                EN ÇOK BASILAN TUŞLAR                                     ");
                writer.WriteLine("==========================================================================================");
                var topKeys = GetTopKeys(10);
                if (topKeys.Count > 0)
                {
                    foreach (var kvp in topKeys)
                    {
                        writer.WriteLine($"  {kvp.Key,-15} : {kvp.Value} kez basıldı");
                    }
                }
                else
                {
                    writer.WriteLine("  İstatistik kaydı bulunamadı.");
                }

                writer.WriteLine("==========================================================================================");
                writer.WriteLine("  Rapor sonu.");
            }

            return (true, $"Kayıtlar başarıyla kaydedildi:\n{filePath}\n(Toplam {recordsToExport.Count} satır)");
        }
        catch (IOException ioEx)
        {
            return (false, $"Dosya yazma hatası (G/Ç): {ioEx.Message}");
        }
        catch (UnauthorizedAccessException authEx)
        {
            return (false, $"Erişim izni hatası: {authEx.Message}");
        }
        catch (Exception ex)
        {
            return (false, $"Beklenmeyen bir hata oluştu: {ex.Message}");
        }
    }

    private static string FormatModifiers(Keys modifiers)
    {
        var list = new List<string>();
        if ((modifiers & Keys.Control) == Keys.Control) list.Add("Ctrl");
        if ((modifiers & Keys.Shift) == Keys.Shift) list.Add("Shift");
        if ((modifiers & Keys.Alt) == Keys.Alt) list.Add("Alt");

        return list.Count > 0 ? string.Join(" + ", list) : "Yok";
    }
}
