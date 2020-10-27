<template>
    <div class="location-bottom d-flex">
        <!-- <div class="mame-card-wrapper btn" v-on:click="GetMissionDolls()">
            {{ signalRConnection }}
        </div> -->

        <div class="cards-panel">
            <MissionCard
                v-on:GetMissionDolls="SetMissionDolls"
                v-bind:MissionDolls="MissionDolls"
                v-bind:signalRConnection="signalRConnection"
            />
            <MameCards
                v-bind:cards="cards"
                v-bind:dolls="dolls"
                v-bind:MissionDolls="MissionDolls"
                v-bind:signalRConnection="signalRConnection"
                v-on:CardDisDolls="DisDolls"
                v-on:CardChooseDolls="ChooseDolls"
            />
        </div>
        <div class="user-avatar"></div>
    </div>
</template>

<script>
// @ is an alias to /src
// import HelloWorld from '@/components/HelloWorld.vue'
import MissionCard from "./MissionCard.vue";
import MameCards from "./MameCards.vue";

function getDolls(dolls) {
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
    mounted() {
        let vm = this;
        // console.log(`connection ${this.signalRConnection}`);
        // console.dir(`${this.signalRConnection}`);

        // console.dir(vm);
        // this.GetMissionDolls();
    },
    props: ["cards", "signalRConnection", "dolls"],

    data() {
        return {
            MissionDolls: [{ id: "1" }],
        };
    },
    components: {
        MissionCard,
        // HelloWorld
        MameCards,
    },
    methods: {
        SetMissionDolls: function (MissionDolls) {
            this.MissionDolls = MissionDolls;

            // console.log(`現在使用 ${this.cardName} 卡片`);
        },
        DisDolls: function (dolls, cardName) {
            this.$emit("BottomDisDolls", dolls, cardName);
        },
        ChooseDolls: function (cardName) {
            this.$emit("BottomChooseDolls", cardName);
        },
        // GetMissionDolls: function () {
        //     // console.log("1233");

        //     let vm = this;
        //     console.log(this.SignalRConnectionData);

        //     console.dir(vm);

        //     this.SignalRConnectionData.invoke("GetMission");
        //     this.SignalRConnectionData.on("GetMission", function (
        //         mission_dolls
        //     ) {
        //         vm.MissionDolls = mission_dolls;
        //         getDolls(vm.MissionDolls);
        //         this.off("GetMission", null);
        //     });
        // },
    },
};
</script>


<style lang="scss" scoped>
.location-bottom {
    display: flex;
    width: 100%;
}
.cards-panel {
    position: relative;
    background-color: rgba(255, 205, 255, 0.5);
    border-radius: 10px;
    width: 100%;
    height: 150px;
}

.user-avatar {
    width: 178px;
    height: 150px;
    background-image: url("../../assets/images/user-icon-bottom.png");
}
</style>