using System;
using System.Text.Json.Serialization;

namespace BlackJack.Domain.Models
{
    public class ResponseData<T>
    {
        [JsonPropertyName("data")]
        public T? Data { get; set; }

        [JsonPropertyName("successfull")]
        public bool Successfull { get; set; } = true;

        [JsonPropertyName("errorMessage")]
        public string? ErrorMessage { get; set; }

        public static ResponseData<T> Success(T data)
        {
            return new ResponseData<T> { Data = data };
        }

        public static ResponseData<T> Error(string message, T? data = default)
        {
            return new ResponseData<T>
            {
                ErrorMessage = message,
                Successfull = false,
                Data = data
            };
        }
    }
}