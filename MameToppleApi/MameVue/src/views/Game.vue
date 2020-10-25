<template>
    <div class="container-fluid game-bg">
        <div class="row text-center">
            <div class="col-12 col-md-12 game-content text-center">
                <div class="row">
                    <div class="col-2 col-md-2 players location-left">
                        <!-- <div class="user-icon user-left"></div>
                        <div class="cards-panel cards-panel-lr"></div> -->
                    </div>
                    <div class="col-8 col-md-8 center-content">
                        <div class="players location-wrap m-auto">
                            <!-- #region 玩家2區域 -->
                            <LocationTop v-bind:cards="cardsData" />
                            <!-- #endregion 玩家2區域 -->
                        </div>
                        <div class="game-board">
                            <!-- #region mame-line 區域 -->
                            <MameLine
                                v-bind:dolls="dollsTowerData"
                                v-bind:cardName="cardName"
                                v-bind:signalRConnection="
                                    signalRConnectionInstance
                                "
                                v-on:MameLineDropDownDolls="DropDownDolls"
                            />
                        </div>

                        <div class="players location-wrap m-auto">
                            <!-- #region 玩家1區域 -->
                            <LocationBottom
                                v-bind:cards="cardsData"
                                v-bind:signalRConnection="
                                    signalRConnectionInstance
                                "
                                v-bind:dolls="dollsTowerData"
                                v-on:BottomDisDolls="DisDolls"
                                v-on:BottomChooseDolls="ChooseDolls"
                            />
                            <!-- #endregion 玩家1區域 -->
                        </div>
                    </div>
                    <div class="col-2 col-md-2 players location-right">
                        <!-- <div class="cards-panel cards-panel-lr"></div>
                        <div class="user-icon user-right"></div> -->
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import LocationTop from "../components/Game/LocationTop.vue";
import LocationBottom from "../components/Game/LocationBottom.vue";
import MameLine from "../components/Game/MameLine.vue";

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
            }
        } else {
            console.log(`${prop}: ${dolls[prop]}`);
        }
    }
    console.log(`${str} | 一共 ${counts} 只 goma`);
}
// import signalR from "../@microsoft/signalr";
const signalR = require("@microsoft/signalr");
// console.log(signalR);

// @ is an alias to /src
// import HelloWorld from '@/components/HelloWorld.vue'

function signalRConnection(vm) {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:44324/mamehub", {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets,
        })
        .build();
    connection.serverTimeoutInMilliseconds = 1200000;

    connection
        .start()
        .then(() => {
            connection.invoke("GetDollsTower");
            connection.on("GetDollsTower", function (dollsTower) {
                vm.dollsTowerData = dollsTower;
            });

            connection.invoke("GetCards");

            connection.on("GetCards", function (cards) {
                vm.cardsData = cards;
            });

            vm.signalRConnectionInstance = connection;
        })
        .catch(function (err) {
            console.error(err.toString());
        });
}
//#endregion signalR

export default {
    name: "MyGame",
    created() {
        let vm = this;
        // console.log(vm);
        signalRConnection(vm);
    },
    data() {
        return {
            dollsTowerData: [],
            cardsData: [],
            cardName: [],
            signalRConnectionInstance: [],
        };
    },
    components: {
        LocationTop,
        LocationBottom,
        MameLine,
    },
    methods: {
        DropDownDolls: function (dolls, cardName) {
            this.dollsTowerData = dolls;
            this.RemoveTheCard(cardName);
            // this.cardName = "";
            this.cardName = "";

            // getDolls(dolls);
        },
        DisDolls: function (dolls, cardName) {
            this.dollsTowerData = dolls;

            this.RemoveTheCard(cardName);

            this.cardName = "";
        },
        ChooseDolls: function (cardName) {
            this.cardName = cardName;
            // console.log(`現在使用 ${this.cardName} 卡片`);
        },
        RemoveTheCard: function (cardName) {
            //移除使用過的卡片
            var str = "";
            var card_index = 0;
            card_index = 0;
            console.log(`card_index: ${card_index}`);

            console.log(this.cardsData);

            for (var item of this.cardsData) {
                str += `${item.name}, `;
                if (item.name == cardName) {
                    console.log(`在${card_index}終於找到 ${cardName}`);

                    break;
                }
                console.log(`在${card_index}沒找到 ${cardName}`);

                card_index++;
            }
            console.log(`目前卡片列表: ${str} `);

            console.log(
                `我要從Card索引值 ${card_index} 開始，刪除一張卡片 ${cardName}`
            );

            this.cardsData.splice(card_index, 1);
        },
    },
};
</script>

<style lang="scss">
.game-bg {
    position: relative;
    &:after {
        content: "";
        display: block;
        position: absolute;
        top: 0;
        left: 0;
        background-image: url("../assets/images/mamegoma-bg.png");
        width: 100%;
        height: 100vh;
        background-position: center;
        background-repeat: no-repeat;
        background-size: cover;
        opacity: 0.1;
        z-index: -1;
    }
}
.center-content {
    display: flex;
    flex-direction: column;

    height: 100vh;
    padding: 0;

    .location-wrap {
        width: 100%;
        flex-grow: 1;
        display: flex;
        margin-top: 20px;
        align-items: center;

        justify-content: center;
    }
    .game-board {
        flex-grow: 2;
        display: flex;
        flex-direction: column;
        justify-content: center;
        padding: 100px 0;
    }
}
</style>