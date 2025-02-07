using System.ComponentModel.DataAnnotations;

namespace FalconOne.Models.Entities
{
    public class Meeting
    {
        [Key]
        public Guid Id { get; set; }
        public string MeetingId { get; set; }
    }
}
