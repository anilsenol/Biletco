using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace biletcoo.Models
{

    [Table("Tickets")]
    public class Ticket
    {
        public int Id { get; set; }

        public int ticketNumber { get; set; }
        public String userId { get; set; }
        public int eventId { get; set; }

        public users User { get; set; }
        public Event MyEvent { get; set; }
    }
}
