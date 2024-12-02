
using System.Text.Json.Serialization;

namespace BlackJack.Domain.Models
{
    public class ListModel<T>
    {
        [JsonPropertyName("$values")]
        public List<T> Items { get; set; } = new List<T>();

        [JsonPropertyName("currentPage")]
        public int CurrentPage { get; set; } = 1;

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; } = 1;
    }
}
