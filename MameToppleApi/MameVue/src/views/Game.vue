<template>
    <div class="container-fluid game-bg">
        <div class="row text-center">
            <div class="col-12 col-md-12 game-content text-center">
                <div class="row">
                    <div class="col-2 col-md-2 players location-left">
                        <div class="user-icon user-left"></div>
                        <div class="cards-panel cards-panel-lr"></div>
                    </div>
                    <div class="col-8 col-md-8 center-content">
                        <div class="players location-top m-auto">
                            <LocationTop />
                        </div>
                        <div class="game-board">
                            <!-- #region mame-line 區域 -->
                            <div class="mame-line">
                                <!-- <div v-for="item in items" class="w-100 h-100"> -->
                                <div
                                    class="item w-100 h-100"
                                    v-for="(item, index) in items"
                                    :key="index"
                                >
                                    <div class="mame-tiki mame-tiki-1">
                                        <img
                                            v-bind:src="item.image"
                                            v-bind:alt="item.name"
                                        />
                                    </div>
                                </div>
                            </div>

                            <!-- <div class="mame-line">
                                    <div class="mame-tiki mame-tiki-1"></div>
                                    <div class="mame-tiki mame-tiki-2"></div>
                                    <div class="mame-tiki mame-tiki-3"></div>
                                </div> -->
                            <!-- #endregion mame-line 區域 -->
                        </div>
                        <div class="players location-bottom m-auto">
                            <div class="user-icon user-bottom"></div>
                            <div class="cards-panel cards-panel-ud">
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
                            </div>
                        </div>
                    </div>
                    <div class="col-2 col-md-2 players location-right">
                        <div class="cards-panel cards-panel-lr"></div>
                        <div class="user-icon user-right"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import LocationTop from "../components/Game/LocationTop.vue";

// @ is an alias to /src
// import HelloWorld from '@/components/HelloWorld.vue'

var connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:5001/mamehub", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
    })
    .build();
let _dollsTower = null;
connection
    .start()
    .then(() => {
        connection.invoke("GetDollsTower");

        connection.on("GetDollsTower", function (dollsTower) {
            console.log(`${JSON.stringify(dollsTower)}`);
            _dollsTower = dollsTower;
            Binding();
            // dollsTower = JSON.stringify(dollsTower);
        });
    })
    .catch(function (err) {
        console.error(err.toString());
    });
//#endregion signalR
// console.log(`${JSON.stringify(_dollsTower)}`);

export default {
    name: "MyGame",
    data() {
        return {
            items: [
                { name: "sakura-goma" },
                { image: "https://sandy.s-ul.eu/NHghYux2" },
            ],
        };
    },
    components: {
        LocationTop,
    },
    methods: {
        handleLoginButtonClick() {
            this.login();
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
</style>