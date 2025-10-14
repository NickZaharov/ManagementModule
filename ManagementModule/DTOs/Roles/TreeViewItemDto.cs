using System.Text.Json.Serialization;

namespace ManagementModule.DTOs.Roles
{
    public class TreeViewItemDto
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("checked")]
        public bool? Checked { get; set; } = false;

        [JsonPropertyName("expanded")]
        public bool? Expanded { get; set; } = false;

        [JsonPropertyName("items")]
        public List<TreeViewItemDto> Items { get; set; } = new();
    }
}
