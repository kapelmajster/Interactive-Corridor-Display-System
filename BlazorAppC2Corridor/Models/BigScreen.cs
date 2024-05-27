using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAppC2Corridor.Models
{
    public class BigScreen
    {
        public int Id { get; set; }
        [Required]
        public string Location { get; set; }
        public IList<BigScreenCarousel> BigScreenCarousels { get; set; } = new List<BigScreenCarousel>();

        [NotMapped]
        public List<int> SelectedCarouselContentIds { get; set; } = new List<int>();
    }
}
