<template>
    <div class="mame-line">
        <div v-for="item in dolls" v-bind:key="item.image" class="w-100 h-100">
            <!-- <div
                                    class="item w-100 h-100"
                                    v-for="(item, index) in items"
                                    :key="index"
                                > -->
            <div class="mame-tiki" v-on:click="selectDoll(item)">
                <div
                    class="mame-tiki-img w-100 h-100"
                    v-bind:style="{
                        'background-image': 'url(' + item.image + ')',
                    }"
                ></div>
            </div>
        </div>
    </div>
</template>

<script>
// @ is an alias to /src
// import HelloWorld from '@/components/HelloWorld.vue'

function getDolls(dolls) {
    var str = "";
    var counts = 0;
    // console.log(dolls);
    for (let prop of dolls) {
        // console.log(`${prop}: ${dolls[prop]}`);
        str += `${prop.id}, `;
    }
    console.log(str);
}

export default {
    created() {},
    data() {
        return {
            // userName: "",
            items: [],
        };
    },
    props: ["dolls", "cardName", "signalRConnection"],
    components: {
        // HelloWorld
    },
    methods: {
        selectDoll: function (item) {
            if (this.cardName == "") {
                return;
            }
            let vm = this;
            let selected_doll = item;
            // console.log(selected_doll);

            if (this.cardName == "DropDown") {
                console.log(
                    `準備DropDown，我想移動編號 ${selected_doll.id} 到最右邊`
                );
                console.log("before");
                getDolls(this.dolls);
                // console.log(`現在使用 ${this.cardName} 卡片`);

                this.signalRConnection.invoke("DollChosen", selected_doll);

                // this.signalRConnection.off("DollChosen", null);
                this.signalRConnection.on("DollChosen", function (new_dolls) {
                    console.log("_after");
                    getDolls(new_dolls);
                    console.log(`準備回傳卡片名 ${vm.cardName}`);
                    this.off("DollChosen", null);

                    vm.$emit("MameLineDropDownDolls", new_dolls, vm.cardName);
                });
            }
        },
    },
};
</script>


<style lang="scss" >
.mame-line {
    width: 100%;
    height: 60px;
    background-color: rgba(255, 220, 175, 0.315);

    display: flex;
    align-items: center;
    justify-content: flex-end;
    .mame-tiki {
        // width: calc(100%/8);
        height: 100%;
        border-radius: 3px;
        margin: 0;
        display: flex;
        align-items: center;
        justify-content: center;
        .mame-tiki-img {
            background-position: center;
            background-repeat: no-repeat;
            background-size: auto 95%;
        }
        &:hover {
            border: 1px solid #ffef91;
            box-shadow: 0 0 5px 5px #ffee8bbb;
        }
        &:active {
            border: 1px solid #91ffbb;
            box-shadow: 0 0 5px 5px #91ffbbbb;
        }
    }
    & :nth-child(99) {
        margin: 0;
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
    & :nth-child(2) {
        border-radius: 8px;
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
    & :nth-child(3) {
        // background-color: #99ffa69f;
        border-radius: 8px;

        background: -webkit-linear-gradient(
            to bottom,
            #bfffc89f,
            #99ffa69f
        ); /* Chrome 10-25, Safari 5.1-6 */
        background: linear-gradient(
            to bottom,
            #bfffc89f,
            #99ffa69f
        ); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
    }
    & :nth-child(4) {
        margin: 0;
        border-radius: 8px;

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
    & :nth-child(5) {
        margin: 0;
        border-radius: 8px;

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
    & :nth-child(6) {
        margin: 0;
        border-radius: 8px;

        background: -webkit-linear-gradient(
            to bottom,
            #ffc493,
            #ffb77b
        ); /* Chrome 10-25, Safari 5.1-6 */
        background: linear-gradient(
            to bottom,
            #ffc493,
            #ffb77b
        ); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
    }
    & :nth-child(7) {
        margin: 0;
        border-radius: 8px;

        background: -webkit-linear-gradient(
            to bottom,
            #93f6ff,
            #6cf3ff
        ); /* Chrome 10-25, Safari 5.1-6 */
        background: linear-gradient(
            to bottom,
            #93f6ff,
            #6cf3ff
        ); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
    }
    & :nth-child(8) {
        margin: 0;
        border-radius: 8px;

        background: -webkit-linear-gradient(
            to bottom,
            #ffa0a0,
            #ff8080
        ); /* Chrome 10-25, Safari 5.1-6 */
        background: linear-gradient(
            to bottom,
            #ffa0a0,
            #ff8080
        ); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
    }
}
</style>