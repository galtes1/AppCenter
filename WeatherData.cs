﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCenter
{

    public class Location
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Current
    {
        [JsonProperty("temp_c")]
        public double TempC { get; set; }

        [JsonProperty("condition")]
        public Condition? Condition { get; set; }

        [JsonProperty("wind_kph")]
        public double WindKph { get; set; }

        [JsonProperty("humidity")]
        public double Humidity { get; set; }
    }

    public class Condition
    {
        [JsonProperty("text")]
        public string? Text { get; set; }

        [JsonProperty("icon")]
        public string? Icon { get; set; }
    }
    class WeatherData
    {
        [JsonProperty("location")]
        public Location? Location { get; set; }

        [JsonProperty("current")]
        public Current? Current { get; set; }
    }
}
