<template>
    <div class="cards-panel cards-panel-ud"></div>
    <div class="user-icon user-top"></div>
</template>

<script>
// @ is an alias to /src
// import HelloWorld from '@/components/HelloWorld.vue'

export default {
    data() {
        return {
            userName: "",
        };
    },
    components: {
        // HelloWorld
    },
    methods: {
        handleLoginButtonClick() {
            this.login();
        },
    },
};
</script>

<style></style>

<style lang="scss">
.location-top {
    width: 100%;
    flex-grow: 1;
    display: flex;
    align-items: center;

    justify-content: center;
}
.user-icon {
    width: 178px;
    height: 150px;
}
.user-top {
    background-image: url("../../assets/images/user-icon-top.png");
}
</style>