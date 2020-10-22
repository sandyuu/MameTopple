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
                            <LocationTop />
                            <!-- #endregion 玩家2區域 -->
                        </div>
                        <div class="game-board">
                            <!-- #region mame-line 區域 -->
                            <MameLine :dolls="dollsTowerData" />

                            <!-- <div class="mame-line">
                                    <div class="mame-tiki mame-tiki-1"></div>
                                    <div class="mame-tiki mame-tiki-2"></div>
                                    <div class="mame-tiki mame-tiki-3"></div>
                                </div> -->
                            <!-- #endregion mame-line 區域 -->
                        </div>

                        <div class="players location-wrap m-auto">
                            <!-- #region 玩家1區域 -->
                            <LocationBottom :cards="cardsData" />
                            <!-- #endregion 玩家1區域 -->

                            <!-- <div class="user-icon user-bottom"></div>
                            <div class="cards-panel">
                                <div class="mame-card">
                                    <div class="mame-card-wrapper">
                                        <div
                                            class="mame-card-side is-active"
                                        ></div>
                                        <div
                                            class="mame-card-side mame-card-side-back"
                                        ></div>
                                    </div>
                                </div>
                            </div> -->
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

// import signalR from "../@microsoft/signalr";
const signalR = require("@microsoft/signalr");
console.log(signalR);

// @ is an alias to /src
// import HelloWorld from '@/components/HelloWorld.vue'

function signalRConnection(vm) {
    var connection = new signalR.HubConnectionBuilder()
        .withUrl("https://localhost:44324/mamehub", {
            skipNegotiation: true,
            transport: signalR.HttpTransportType.WebSockets,
        })
        .build();
    connection
        .start()
        .then(() => {
            connection.invoke("GetDollsTower");

            connection.on("GetDollsTower", function (dollsTower) {
                // console.log(`${JSON.stringify(dollsTower)}`);
                // $data.items = dollsTower;
                // vm.items = dollsTower;
                vm.dollsTowerData = dollsTower;

                // Binding();
                // dollsTower = JSON.stringify(dollsTower);
            });

            connection.invoke("GetCards");

            connection.on("GetCards", function (cards) {
                // console.log(`${JSON.stringify(dollsTower)}`);
                // $data.items = dollsTower;
                // vm.items = dollsTower;
                vm.cardsData = cards;

                // console.log(`${JSON.stringify(cards)}`);

                // Binding();
                // dollsTower = JSON.stringify(dollsTower);
            });
        })
        .catch(function (err) {
            console.error(err.toString());
        });
}

//#endregion signalR
// console.log(`${JSON.stringify(_dollsTower)}`);
// console.log(`${JSON.stringify(_dollsTower)}`);

export default {
    name: "MyGame",
    created() {
        let vm = this;
        // console.log(vm);
        signalRConnection(vm);
    },
    data() {
        return {
            // items: [],
            // dollsTowerData: dollsTower,
            dollsTowerData: [],
            cardsData: [],

            //#region 測試資料
            // dollsTowerData: [
            //     {
            //         id: 4,
            //         name: "Panda-goma",
            //         image: "https://sandy.s-ul.eu/FqRRkn3Y",
            //     },
            // ],

            // items: _dollsTower,
            // items: [
            //     {
            //         id: 4,
            //         name: "Panda-goma",
            //         image: "https://sandy.s-ul.eu/FqRRkn3Y",
            //     },
            // ],
            //#endregion 測試資料
        };
    },
    components: {
        LocationTop,
        LocationBottom,
        MameLine,
    },
    methods: {
        // handleLoginButtonClick() {
        //     this.login();
        // },
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