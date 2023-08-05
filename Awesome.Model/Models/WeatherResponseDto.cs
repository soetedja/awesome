using System;

namespace Awesome.Model
{
    public class WeatherResponseDto
    {
        public string Location { get; set; }

        public DateTime Time { get; set; }

        public double WindSpeed { get; set; }

        public int WindDegree { get; set; }

        public int Visibility { get; set; }

        public string SkyConditions { get; set; }

        public double TemperatureCelsius { get; set; }

        public double TemperatureFahrenheit { get; set; }

        public double DewPoint { get; set; }

        public int RelativeHumidity { get; set; }

        public double Pressure { get; set; }
    }
}
