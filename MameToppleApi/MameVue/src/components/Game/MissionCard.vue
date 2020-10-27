<template>
    <!-- <div class="test">{{ cards }}</div> -->
    <div class="mame-card">
        <!-- <div class="w-100 h-100">
            <div class="mame-card-wrapper">
                <div class="mame-card-side is-active">123</div>
                <div class="mame-card-side mame-card-side-back"></div>
            </div>
        </div> -->
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
    props: ["MissionDolls", "signalRConnection"],
    created() {
        var vm = this;
        setTimeout(function () {
            vm.GetMissionDolls();
        }, 1000);
    },
    mounted() {
        // var vm = this;
        // window.onload = function () {
        //     vm.GetMissionDolls();
        // };
        // vm.GetMissionDolls();
    },
    updated() {
        // this.GetMissionDolls();
    },

    data() {
        return {};
    },

    components: {},
    methods: {
        GetMissionDolls: function () {
            let vm = this;
            console.log("mission test");
            // var myconnection = JSON.parse(
            //     JSON.stringify(this.signalRConnection)
            // );
            // console.log(myconnection);

            // console.log(this);

            // console.log(this.signalRConnection);
            // console.log("mission end");

            // console.log(`mission conn ${this.signalRConnection}`);
            this.signalRConnection.invoke("GetMission");
            this.signalRConnection.on("GetMission", function (mission_dolls) {
                getDolls(mission_dolls);
                this.off("GetMission", null);
                vm.$emit("GetMissionDolls", mission_dolls);
            });
        },
    },
};
</script>


<style lang="scss" scoped>
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