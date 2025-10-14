using System.Text.Json.Serialization;

namespace ManagementModule.DTOs.Roles
{
    public class PermissionDto
    {
        [JsonPropertyName("id")]
        public int PermissionId { get; init; }

        [JsonPropertyName("text")]
        public string PermissionName { get; init; } = string.Empty;

        [JsonPropertyName("checked")]
        public bool IsChecked { get; init; } = false;
    }
}
