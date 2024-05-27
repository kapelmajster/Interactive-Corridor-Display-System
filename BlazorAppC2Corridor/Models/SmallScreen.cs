using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAppC2Corridor.Models
{
    public class SmallScreen
    {
        public int Id { get; set; }
        [Required]
        public string RoomNumber { get; set; }
        [Required]
        public string RoomName { get; set; }
        [Required]
        public string Bibliography { get; set; }
        public bool IsStaffRoom { get; set; }
        public string? StaffName { get; set; }
        public ICollection<TimetableSmallScreen> TimetableSmallScreens { get; set; }
        [NotMapped]
        public bool IsSelected { get; set; } = false;
    }
}
