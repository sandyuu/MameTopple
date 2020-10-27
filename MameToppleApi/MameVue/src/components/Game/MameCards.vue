<template>
    <!-- <div class="test">{{ cards }}</div> -->
    <div class="mame-card">
        <div class="w-100 h-100">
            <div class="mame-card-wrapper">
                <div class="mame-card-side is-active">
                    <div class="mission-card">
                        <div
                            v-for="(item, index) in MissionDolls"
                            v-bind:key="item.id"
                            class="mission-doll-block"
                        >
                            <div class="row w-100 h-100">
                                <div
                                    class="col-4 col-md-4 p-0 d-flex text-center align-items-center"
                                >
                                    <div
                                        class="mission-doll-icon w-100 h-100"
                                        :style="{
                                            'background-image':
                                                'url(' + item.image + ')',
                                        }"
                                    ></div>
                                </div>
                                <div class="col-8 col-md-8 p-0">
                                    <div
                                        class="mission-doll-score-content w-100 h-100"
                                    >
                                        <div class="mission-doll-name">
                                            {{ item.name }}
                                        </div>
                                        <div
                                            class="mission-doll-score d-flex w-100"
                                        >
                                            <div
                                                v-if="index == 0"
                                                class="score-icon w-100"
                                                :style="{
                                                    'background-image':
                                                        'url(' +
                                                        require('../../assets/images/mission-icons/mission-beans.png') +
                                                        ')',
                                                }"
                                            ></div>
                                            <div
                                                v-if="index == 1"
                                                class="score-icon w-100"
                                                :style="{
                                                    'background-image':
                                                        'url(' +
                                                        require('../../assets/images/mission-icons/mission-candy.png') +
                                                        ')',
                                                }"
                                            ></div>
                                            <div
                                                v-if="index == 2"
                                                class="score-icon w-100"
                                                :style="{
                                                    'background-image':
                                                        'url(' +
                                                        require('../../assets/images/mission-icons/mission-squid.png') +
                                                        ')',
                                                }"
                                            ></div>
                                            <div
                                                v-if="index == 0"
                                                class="score-point w-100 h-100"
                                            >
                                                9pt
                                            </div>
                                            <div
                                                v-if="index == 1"
                                                class="score-point w-100 h-100"
                                            >
                                                5pt
                                            </div>
                                            <div
                                                v-if="index == 2"
                                                class="score-point w-100 h-100"
                                            >
                                                2pt
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div
                    class="mame-card-side mame-card-side-back mission-card-back"
                ></div>
            </div>
        </div>
        <!-- <div class="mission-card">{{ MissionDolls }}</div> -->
        <div v-for="item in cards" v-bind:key="item.id" class="w-100 h-100">
            <div class="mame-card-wrapper" v-on:click="selectCard(item)">
                <div
                    class="mame-card-side is-active"
                    v-bind:style="{
                        'background-image': 'url(' + item.image + ')',
                    }"
                ></div>
                <div class="mame-card-side mame-card-side-back"></div>
            </div>
        </div>
    </div>
</template>

<script>
// @ is an alias to /src
// import HelloWorld from '@/components/HelloWorld.vue'
let _newdolls = null;
function getDolls(dolls) {
    if (dolls == null) {
        return;
    }
    var str = "";
    var counts = 0;
    for (let prop in dolls) {
        if (typeof dolls[prop] === "object") {
            for (let current_prop in dolls[prop]) {
                if (current_prop == "name") {
                    str += `${dolls[prop][current_prop]}, `;
                    counts++;
                }
                // console.log(`${current_prop}: ${dolls[prop][current_prop]}`);
            }
        } else {
            console.log(`${prop}: ${dolls[prop]}`); //person[prop] 相當於 person['Name']
        }
    }
    console.log(`${str} | 一共 ${counts} 只 goma`);
}

export default {
    props: ["cards", "signalRConnection", "MissionDolls", "dolls"],
    created() {
        // let vm = this;
        this.newdolls = this.dolls;
        // console.log(`card conn ${this.signalRConnection}`);
        // console.log("建立");
    },
    data() {
        return {
            // userName: "",
            items: [],
        };
    },

    components: {
        // HelloWorld
    },
    methods: {
        selectCard: function (item) {
            let cardName;
            let vm = this;
            cardName = item.name;
            // console.log(`列出全部 ${JSON.stringify(this.cards)}`);
            // console.log(`before DOLLS`);
            if (this.dolls.length == 0) {
                return;
            }
            if (_newdolls == null) {
                _newdolls = this.dolls;
            }

            // getDolls(_newdolls);

            console.log(`現在使用 ${cardName} 卡片`);
            console.log(this.signalRConnection);
            // console.log(`card conn ${this.signalRConnection}`);
            this.signalRConnection.invoke("UseCard", vm.dolls, cardName);
            // this.signalRConnection.off("UseCard", null);

            this.signalRConnection.on("UseCard", function (new_dolls) {
                if (cardName == "Discard") {
                    _newdolls = new_dolls;
                    this.off("UseCard", null);
                    vm.$emit("CardDisDolls", _newdolls, cardName);
                }
                if (cardName == "DropDown") {
                    this.off("UseCard", null);
                    vm.$emit("CardChooseDolls", cardName);
                }
                if (
                    cardName == "UpOne" ||
                    cardName == "UpTwo" ||
                    cardName == "UpThree"
                ) {
                    this.off("UseCard", null);
                    vm.$emit("CardChooseDolls", cardName);
                }
            });

            // this.$nextTick(function () {
            //     console.log("離開後");
            //     getDolls(null);
            // });
            // getDolls(_newdolls);
        },
    },
};
</script>


<style lang="scss" scoped>
@import url("https://fonts.googleapis.com/css2?family=Luckiest+Guy&display=swap");
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

@keyframes rotate-inverse {
    0% {
        transform: rotateY(180deg);
    }
    70% {
        transform: rotateY(-20deg);
    }
    100% {
        transform: rotateY(0);
    }
}

.mame-card {
    width: 100%;
    height: 100%;
    display: flex;
    // justify-content: space-between;
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
        animation: rotate-inverse $card-transition-time
            $card-transition-delay-time linear both;
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
            &:active {
                border: 1px solid #ff91ff;
                box-shadow: 0 0 5px 5px #ff91ffbe;
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
        .mission-card-back {
            &:before {
                background-image: url("../../assets/images/mission_card_back.png");
            }
        }
    }
    .mission-card {
        height: 100%;
        display: flex;
        flex-direction: column;

        // height: 100vh;
        padding: 0;
        .mission-doll-block {
            margin: 0;
            width: 100%;
            height: 100%;

            flex-grow: 1;
            display: flex;
            margin-top: 20px;
            align-items: center;

            justify-content: center;
            .mission-doll-icon {
                background-size: 75%;
                background-position: center;
                background-repeat: no-repeat;
            }
            .mission-doll-score-content {
                display: flex;
                flex-direction: column;
                // align-items: center;
                justify-content: center;
                .mission-doll-name {
                    font-family: "Luckiest Guy", cursive;
                    // font-weight: bold;
                }
                .mission-doll-score {
                    padding: 0 5px;

                    .score-icon {
                        background-size: 50%;
                        background-position: center;
                        background-repeat: no-repeat;
                    }
                    .score-point {
                        font-family: "Luckiest Guy", cursive;

                        // font-weight: bold;
                    }
                }
            }
        }

        & > :nth-child(1) {
            margin: 0;
            border-top-left-radius: 8px;
            border-top-right-radius: 8px;

            // background-color: rgba(140, 232, 255, 0.623);#E85D2D);
            // background: #ee9ca7;  /* fallback for old browsers */
            background: -webkit-linear-gradient(
                to bottom,
                #ffb8c0,
                #ee9ca7
            ); /* Chrome 10-25, Safari 5.1-6 */
            background: linear-gradient(
                to bottom,
                #ffb8c0,
                #ee9ca7
            ); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
        }
        & > :nth-child(2) {
            margin: 0;

            // border-radius: 8px;
            background: -webkit-linear-gradient(
                to bottom,
                #f0f5c4,
                #fff89c
            ); /* Chrome 10-25, Safari 5.1-6 */
            background: linear-gradient(
                to bottom,
                #f0f5c4,
                #fff89c
            ); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
        }
        & > :nth-child(3) {
            margin: 0;

            // background-color: #99ffa69f;
            border-bottom-left-radius: 8px;
            border-bottom-right-radius: 8px;

            background: -webkit-linear-gradient(
                to bottom,
                #b8c9ff,
                #9bb2ff
            ); /* Chrome 10-25, Safari 5.1-6 */
            background: linear-gradient(
                to bottom,
                #b8c9ff,
                #9bb2ff
            ); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
        }
    }
}
</style>