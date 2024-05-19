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
            console.error('dotNetHelper is not set');
            return;
        }
        await Helpers.dotNetHelper.invokeMethodAsync('GetScreenSize')
            .then(data => {
                console.log('Screen size updated successfully');
            })
            .catch(error => {
                console.error('Error updating screen size:', error);
            });
    }
    static getScreenWidth() {
        return window.innerWidth;
    }
    static async checkSwiper() {
        console.log("checking size :: swiper is ::", swiper);
        if (window.innerWidth >= 420 && swiper) {
            swiper.destroy(true, true);
            swiper = null; //undefined
            document.querySelectorAll('.swiper-slide').forEach(slide => {
                slide.style.display = 'block';
            });

        } else if (window.innerWidth < 420 && !swiper) {
            console.log("Initializing Swiper");
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
    console.log("ininting swiper");
    console.log("Number of slides:", document.querySelectorAll('.swiper-slide').length);
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
        console.log("Swiper initialized:", swiper);
    } else {
        console.log("Swiper already initialized. Updating...");
        swiper.update();
    }
}

function getScreenWidth() {
    return window.innerWidth;
}

window.Helpers = Helpers;
