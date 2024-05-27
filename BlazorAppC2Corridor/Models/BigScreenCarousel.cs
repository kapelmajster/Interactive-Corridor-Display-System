namespace BlazorAppC2Corridor.Models
{
    public class BigScreenCarousel
    {
        public int BigScreenId { get; set; }
        public BigScreen BigScreen { get; set; }
        public int CarouselContentId { get; set; }
        public CarouselContent CarouselContent { get; set; }
    }
}
