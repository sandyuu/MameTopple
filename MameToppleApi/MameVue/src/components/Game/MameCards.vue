<template>
    <!-- <div class="test">{{ cards }}</div> -->
    <div class="mame-card">
        <!-- <div>{{ signalRConnection }}</div> -->
        <div v-for="item in cards" v-bind:key="item.image" class="w-100 h-100">
            <div class="mame-card-wrapper" @click="selectCard(item)">
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
    props: ["cards", "signalRConnection", "dolls"],
    created() {
        // let vm = this;
        this.newdolls = this.dolls;
        console.log("建立");
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

            // console.log(`現在使用 ${cardName} 卡片`);

            this.signalRConnection.invoke("UseCard", _newdolls, cardName);
            this.signalRConnection.off("UseCard", null);
            this.signalRConnection.on("UseCard", function (new_dolls) {
                if (cardName == "Discard") {
                    _newdolls = new_dolls;

                    vm.$emit("CardDisDolls", _newdolls, cardName);
                }
                if (cardName == "DropDown") {
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