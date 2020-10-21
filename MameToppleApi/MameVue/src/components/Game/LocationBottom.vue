<template>
    <div class="location-bottom">
        <div class="cards-panel"></div>
        <div class="user-avatar"></div>
    </div>

    <!-- <div class="alert alert-warning" role="alert">
        A simple warning alert—check it out!
    </div> -->
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


<style lang="scss" scoped>
.location-bottom {
    display: flex;
    width: 100%;
}
.cards-panel {
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