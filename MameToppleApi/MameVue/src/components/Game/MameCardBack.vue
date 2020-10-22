<template>
    <!-- <div class="test">{{ cards }}</div> -->
    <div class="mame-card">
        <div class="mame-card-wrapper">
            <div class="mame-card-side is-active"></div>
            <div class="mame-card-side mame-card-side-back"></div>
        </div>
        <!-- <div v-for="item in cards" class="w-100 h-100">
            <div class="mame-card-wrapper">
                <div class="mame-card-side mame-card-side-back"></div>
            </div>
        </div> -->
    </div>
</template>

<script>
// @ is an alias to /src
// import HelloWorld from '@/components/HelloWorld.vue'

export default {
    created() {},
    data() {
        return {
            // userName: "",
            items: [],
        };
    },
    props: ["cards"],
    components: {
        // HelloWorld
    },
    methods: {
        // handleLoginButtonClick() {
        //     this.login();
        // },
    },
};
</script>


<style lang="scss" >
$card-transition-time: 1s;
$card-transition-delay-time: 1s;

@keyframes rotate {
    0% {
        transform: rotateY(0);
    }
    70% {
        transform: rotateY(200deg);
    }
    100% {
        transform: rotateY(180deg);
    }
}

.mame-card {
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: space-between;
    perspective: 600px;
    // position: relative;
    position: absolute;
    bottom: 0;
    &.is-switched {
        .mame-card-wrapper {
            animation: rotate $card-transition-time linear both;
        }
    }
    .mame-card-wrapper {
        width: 120px;
        height: 180px;
        transform-style: preserve-3d;
        margin-left: 10px;
        position: absolute;
        bottom: 6px;
        .mame-card-side {
            backface-visibility: hidden;
            position: absolute;
            bottom: 0;
            width: 100%;
            height: 100%;

            content: "";
            display: block;

            border-radius: 10px;

            width: 100%;
            height: 100%;
            background-image: url("../../assets/images/mame_card_back.png");

            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
            filter: drop-shadow(8px 8px 5px rgba(0, 0, 0, 0.3));

            transition: 0.3s;

            &.is-active {
                position: static;
            }

            &:hover {
                position: absolute;

                border: 1px solid #ffef91;
                // box-shadow: 0 0 0 5px #ffee8b9d;
                box-shadow: 0 0 5px 5px #ffee8bbb;
                filter: drop-shadow(6px 7px 4px rgba(0, 0, 0, 0.3));

                width: 110%;
                height: 110%;
                top: -30px;
            }
        }

        .mame-card-side-back {
            transform: rotateY(180deg);
            width: 100%;
            height: 100%;
            &:before {
                content: "";
                display: block;
                position: absolute;
                bottom: 0;
                background-image: url("../../assets/images/mame_card_back.png");
                border-radius: 10px;

                width: 100%;
                height: 100%;
                background-position: center;
                background-repeat: no-repeat;
                background-size: cover;
                filter: drop-shadow(8px 8px 5px rgba(0, 0, 0, 0.3));

                transition: 0.3s;
            }
        }
    }
}
</style>