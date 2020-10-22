<template>
    <div class="location-top">
        <div class="cards-panel">
            <MameCardBack :cards="cards" />
        </div>
        <div class="user-avatar"></div>
    </div>
</template>

<script>
// @ is an alias to /src
// import HelloWorld from '@/components/HelloWorld.vue'
import MameCardBack from "../Game/MameCardBack.vue";

export default {
    prop: ["cards"],
    data() {
        return {
            userName: "",
        };
    },
    components: {
        MameCardBack,
    },

    methods: {
        handleLoginButtonClick() {
            this.login();
        },
    },
};
</script>


<style lang="scss" scoped>
.location-top {
    display: flex;
    width: 100%;
}
.cards-panel {
    background-color: rgba(255, 205, 255, 0.5);
    border-radius: 10px;
    width: 100%;
    height: 150px;
    position: relative;
}

.user-avatar {
    width: 178px;
    height: 150px;
    background-image: url("../../assets/images/user-icon-left.png");
}
</style>