namespace RESTWithSwagger.Filters
{
    using System;

    public class WeatherFilter
    {
        public int RegionId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
