using System.ComponentModel.DataAnnotations;
using biletcoo.Models;

namespace biletcoo.ViewModel
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.DateTime)]
        public DateTime date { get; set; }

        [Required(ErrorMessage = "Capacity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Capacity must be at least 1.")]
        public int capacity { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Price must be non-negative.")]
        public int price { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string category { get; set; }

        public List<Event>? Events { get; set; }
        public List<int>? Favorites { get; set; }
    }
}
