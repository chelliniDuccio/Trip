﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Trip.Models.Extra;

namespace Trip.Models
{
    public class Country : BaseModel
    {
        [Required]
        [MaxLength(60)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2)]
        public string Iso { get; set; }

        [Required]
        [MaxLength(3)]
        public string Iso3 { get; set; }

        [Required]
        [MaxLength(5)]
        public string Dial { get; set; }

        [MaxLength(3)]
        public string? Currency { get; set; }

        [MaxLength(60)]
        public string CurrencyName { get; set; }

        [MaxLength(10)]
        public string? CurrencySymbol { get; set; }

        [JsonPropertyName("flag")] // Forza la serializzazione
        public string Flag => $"https://flagcdn.com/{Iso.ToLower()}.svg";
    }
}
