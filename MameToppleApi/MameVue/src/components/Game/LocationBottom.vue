<template>
    <div class="location-bottom">
        <div class="cards-panel">
            <!-- <MameCards :cards="cards" class="w-100 h-100" /> -->
            <MameCards :cards="cards" />
        </div>
        <div class="user-avatar"></div>
    </div>
</template>

<script>
// @ is an alias to /src
// import HelloWorld from '@/components/HelloWorld.vue'
import MameCards from "./MameCards.vue";

export default {
    props: ["cards"],
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