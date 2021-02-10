namespace Models.DataModels
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Weathers")]
    public class Weather
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime DateAndTime { get; set; }

        public int Degrees { get; set; }

        public int RegionId { get; set; }

        public virtual Region Region { get; set; }
    }
}