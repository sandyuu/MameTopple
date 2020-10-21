<template>
    <div class="mame-line">
        <div v-for="item in dolls" class="w-100 h-100">
            <!-- <div
                                    class="item w-100 h-100"
                                    v-for="(item, index) in items"
                                    :key="index"
                                > -->
            <div class="mame-tiki mame-tiki-1">
                <img v-bind:src="item.image" v-bind:alt="item.name" />
            </div>
        </div>
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
    props: ["dolls"],
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
    }
    .mame-tiki-1 {
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
    .mame-tiki-2 {
        background-color: #ffc4769f;
        background: #ede574; /* fallback for old browsers */
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
    .mame-tiki-3 {
        // background-color: #99ffa69f;
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
}
</style>