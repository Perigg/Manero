class Helpers {
    static dotNetHelper;
    static async setDotNetHelper(value) {
        Helpers.dotNetHelper = value;
        window.addEventListener('load', Helpers.checkSwiper);
        window.addEventListener('resize', Helpers.checkSwiper);
        Helpers.checkSwiper();
    }
    static async updateSwiperState() {
        if (Helpers.dotNetHelper === undefined) {
            return;
        }
        await Helpers.dotNetHelper.invokeMethodAsync('GetScreenSize')
    }
    static getScreenWidth() {
        return window.innerWidth;
    }
    static async checkSwiper() {
        if (window.innerWidth >= 420 && swiper) {
            swiper.destroy(true, true);
            swiper = null;
            document.querySelectorAll('.swiper-slide').forEach(slide => {
                slide.style.display = 'block';
            });

        } else if (window.innerWidth < 420 && !swiper) {
            initSwiper();
        }
        if (swiper) {
            swiper.update();
        }
        Helpers.updateSwiperState();
    }

}
let swiper;

function initSwiper() {
    if (true) {
        swiper = new Swiper('.swiper-container', {
            direction: 'horizontal',
            loop: true,
            pagination: {
                el: '.swiper-pagination',
            },
            navigation: {
                nextEl: '.swiper-button-next',
                prevEl: '.swiper-button-prev',
            },
        });
    } else {
        swiper.update();
    }
}

function getScreenWidth() {
    return window.innerWidth;
}

window.Helpers = Helpers;
