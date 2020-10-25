<template>
    <div class="location-bottom">
        <div class="cards-panel">
            <!-- <MameCards v-bind:cards="cards" class="w-100 h-100" /> -->
            <!-- <MameCards v-bind:cards="cards" /> -->
            <MameCards
                v-bind:cards="cards"
                v-bind:dolls="dolls"
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
    // props: ["cards", "signalRConnection"],
    props: ["cards", "signalRConnection", "dolls"],

    // props: ["cards"],

    data() {
        return {
            // cardsData: cards,
        };
    },
    components: {
        // HelloWorld
        MameCards,
    },
    methods: {
        DisDolls: function (dolls, cardName) {
            this.$emit("BottomDisDolls", dolls, cardName);
        },
        ChooseDolls: function (cardName) {
            this.$emit("BottomChooseDolls", cardName);
        },
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