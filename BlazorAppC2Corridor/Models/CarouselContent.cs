using BlazorAppC2Corridor.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAppC2Corridor.Models
{
    public class CarouselContent
    {
        public int Id { get; set; }
        public ContentType ContentType { get; set; }
        [Required]
        public string ContentName { get; set; }
        public int? Priority { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        public DateTime? EmbargoDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public int? FileId { get; set; }
        public virtual UploadedFile File { get; set; }
        public ICollection<BigScreenCarousel>? BigScreenCarousels { get; set; }
        [Required]
        public Transition TransitionType { get; set; }
        public string? TextContent { get; set; }
        public LayoutType LayoutType { get; set; }
        public FontType FontType { get; set; }
        public float FontSize { get; set; }

        [NotMapped]
        public bool IsSelected { get; set; } = false;
    }
}
