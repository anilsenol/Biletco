using System.ComponentModel.DataAnnotations;
using static biletcoo.Models.Enums;

namespace biletcoo.Models
{
    public class Event
    {
        public Event()
        {
        }
        public Event(string name, string location, string description, DateTime date, int capacity, int price, string category, string status, string organizerId)
        {
            Name = name;
            Location = location;
            Description = description;
            this.date = date;
            this.capacity = capacity;
            this.price = price;
            this.category = category;
            Status = status;
            OrganizerId = organizerId;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime date { get; set; }

        public int capacity { get; set; }

        public int price { get; set; }
        public string category { get; set; }

        public string Status { get; set; }

        public string OrganizerId { get; set; }






    }
}
