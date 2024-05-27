using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAppC2Corridor.Models
{
    public class Timetable
    {
        public int Id { get; set; }
        [Required]
        public string ModuleCode { get; set; }
        [Required]
        public string ModuleName { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        [Required]
        public string Lecturer { get; set; }
        public ICollection<TimetableSmallScreen> TimetableSmallScreens { get; set; }
        [NotMapped]
        public bool IsSelected { get; set; } = false;
    }
}
