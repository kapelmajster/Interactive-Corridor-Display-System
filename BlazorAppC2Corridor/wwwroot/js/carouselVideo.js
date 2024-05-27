window.setupVideoLoop = (videoId) => {
    const videoElement = document.getElementById(videoId);
    if (!videoElement) return;

    videoElement.onended = () => {
        videoElement.currentTime = 0;
        videoElement.play();
    };
};

window.playVideo = function (videoId) {
    const videoElement = document.getElementById(videoId);
    if (videoElement) {
        videoElement.play();
    }
};

window.carouselManager = {
    setupCarousel: function (carousel, durations) {
        const carouselItems = carousel.querySelectorAll('.mud-carousel-item');

        let currentIndex = 0;

        function changeItem() {
            carouselItems[currentIndex].classList.remove('mud-carousel-active');
            currentIndex = (currentIndex + 1) % carouselItems.length;
            carouselItems[currentIndex].classList.add('mud-carousel-active');

            setTimeout(changeItem, durations[currentIndex] * 1000);
        }

        setTimeout(changeItem, durations[0] * 1000);
    }
};