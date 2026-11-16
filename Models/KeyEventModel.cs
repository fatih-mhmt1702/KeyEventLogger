namespace KeyEventLogger.Models;

/// <summary>
/// Klavyeden yakalanan tek bir olay kaydını temsil eden model sınıfı.
/// </summary>
public class KeyEventModel
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public string EventType { get; set; } = string.Empty; // KeyDown, KeyUp, KeyPress
    public string KeyName { get; set; } = string.Empty;
    public int KeyValue { get; set; }
    public string? KeyChar { get; set; }
    public string Modifiers { get; set; } = "Yok";

    /// <summary>
    /// ListBox veya metin tabanlı günlük ekranı için biçimlendirilmiş satır.
    /// </summary>
    public string ToLogLine()
    {
        string charPart = !string.IsNullOrEmpty(KeyChar) ? $" | Karakter: '{KeyChar}'" : "";
        string modPart = Modifiers != "Yok" ? $" | Değiştirici: [{Modifiers}]" : "";
        return $"[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{EventType,-8}] Tuş: {KeyName,-12} (Sanal Kod: {KeyValue,3}){charPart}{modPart}";
    }

    /// <summary>
    /// Metin dosyasına yazılacak satır biçimi.
    /// </summary>
    public string ToExportLine()
    {
        return $"{Timestamp:yyyy-MM-dd HH:mm:ss.fff}\t{EventType}\t{KeyName}\t{KeyValue}\t{KeyChar ?? "-"}\t{Modifiers}";
    }
}
