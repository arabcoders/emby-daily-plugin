using System.Text.Json;

namespace DailyExtender.Helpers
{
    /// <summary>
    /// Object Wrapper for filename metadata.
    /// </summary>
    public class DTO
    {
        public bool Parsed { get; set; } = false;
        public string Season { get; set; }
        public string Episode { get; set; }
        public string Series { get; set; }
        public string Date { get; set; }
        public string Year { get; set; }
        public string Title { get; set; }
        public string File { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
