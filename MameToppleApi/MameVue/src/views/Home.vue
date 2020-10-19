<template>
  <div class="home">
    <div class="title">MAME TOPPLE</div>
    <div class="userName">
      Hello,{{userName}}
    </div>
    <div class="content text-center">
      <button type="button" class="btn btn-primary start" v-show="toggle">
        START
      </button>
    </div>
    <div>
      <b-modal
        ref="my-modal"
        hide-footer
        centered
        hide-header
        no-close-on-backdrop
      >
        <!-- no-close-on-backdrop -->
        <div class="d-block text-center">
          <!-- <b-card no-body>
          <b-tabs content-class="mt-3" align="center" pills card >
            <b-tab title="First" active><p>I'm the first tab</p></b-tab>
            <b-tab title="Second"><p>I'm the second tab</p></b-tab>
          </b-tabs>
           </b-card> -->
          <b-tabs
            active-nav-item-class="font-weight-bold text-uppercase text-danger"
            active-tab-class="font-weight-bold text-success"
            content-class="mt-3"
          >
            <b-tab title="登入" active>
              <!-- <label for=""> 帳號：</label> -->
              <b-row class="my-3 mx-3">
                <b-form-input
                  v-model="account"
                  placeholder="Enter your email"
                ></b-form-input
              ></b-row>
              <!-- <input type="text" v-model="account" />  -->
              <!-- <label for=""> 密碼：</label> -->
              <b-row class="my-3 mx-3">
                <b-form-input
                  v-model="password"
                  type="password"
                  placeholder="Enter your password"
                ></b-form-input>
                <!-- 這邊是要復原的 -->
                <!-- <b-form-checkbox
                  id="checkbox-1"
                  v-model="status"
                  name="checkbox-1"
                  value="accepted"
                  unchecked-value="not_accepted"
                  class="text-center"
                >
                  Remember Me
                </b-form-checkbox> -->
                <!-- 這邊是要復原的 -->
              </b-row>
              <b-row class="text-center my-3">
                <b-col>
                  <b-button variant="info" @click="handleLoginButtonClick"
                    >登入</b-button
                  >
                </b-col>
              </b-row>
              <!-- 密碼：<input type="text" v-model="password"/> -->
            </b-tab>
            <b-tab title="註冊">
              <!-- 這邊是要復原的 -->
              <b-row class="my-3 mx-3">
                <b-form-input
                  v-model="registeraccount"
                  placeholder="Enter your email"
                ></b-form-input
              ></b-row>
              <b-row class="my-3 mx-3">
                <b-form-input
                  v-model="registerpassword"
                  placeholder="Enter your password"
                ></b-form-input
              ></b-row>
              <b-row class="my-3 mx-3">
                <b-form-input
                  v-model="registernickname"
                  placeholder="Enter your nickname"
                ></b-form-input
              ></b-row>
              <b-row class="text-center my-3">
                <b-col>
                  <b-button variant="info" @click="btnregister">註冊</b-button>
                </b-col>
              </b-row>
              <!-- 這邊是要復原的 -->
            </b-tab>
          </b-tabs>
          <!-- <h3>Hello From My Modal!</h3> -->
        </div>

        <!-- <b-button class="mt-3" variant="outline-danger" block @click="hideModal"
          >Close Me</b-button
        > -->
      </b-modal>
    </div>

    <!-- <button @click="login">登入</button>
    <div>
      <b-button>Button</b-button>
      <b-button variant="danger">Button</b-button>
      <b-button variant="success">Button</b-button>
      <b-button variant="outline-primary">Button</b-button>
    </div> -->
  </div>
</template>

<script>
// @ is an alias to /src
// import HelloWorld from '@/components/HelloWorld.vue'
    import { LoginPageService } from "../services/LoginPageService";


export default {
  name: "Home",
  data() {
    return {
      userName:"",
      account: "string",
      password: "string",
      registeraccount: "",
      registerpassword: "",
      registernickname: "",
      toggle: false,
    };
  },
  components: {
    // HelloWorld
  },
  methods: {
    handleLoginButtonClick() {
      this.login();
    },
    async login() {
      var payload = {
        account: this.account,
        password: this.password,
      };

      try {
        var loginRes = await LoginPageService.login1(payload);//回傳token
        console.log("loginRes");
        console.log(loginRes);
        sessionStorage['token']=loginRes//用session儲存token值

        this.toggle = !this.toggle;//開始按鈕的顯示//如果登入成功開始按鈕就會顯示
        this.$refs["my-modal"].hide();

        var getUserNameRes = await LoginPageService.getUserName();
        console.log("getUserNameRes");
        console.log(getUserNameRes);
        console.log(getUserNameRes.data);//.data是取帳號的值
        this.userName=getUserNameRes.data
      } catch(exception) {
        console.log("你這個大白癡");
        console.log(exception);
      }
    },
    async btnregister() {
      var payload = {
        account: this.registeraccount,
        password: this.registerpassword,
        nickname: this.registernickname,
        avatar: "avatar123",
        win: 0,
        lose: 0,
      };
      var registerRes = await LoginPageService.register(payload);
      console.log("registerRes");
      console.log(registerRes);
    },
    showModal() {
      this.$refs["my-modal"].show();
    },
    // hideModal() {
    //   this.$refs["my-modal"].hide();
    // },
  },
  mounted() {
    this.showModal();
  },
};
</script>
<style scoped >
@import url("https://fonts.googleapis.com/css2?family=Comic+Neue:wght@700&display=swap");
.title,
.start {
  font-family: "Comic Neue", cursive;
  font-size: 120px;
  color: cornflowerblue;
}
.userName{
  font-size: 40px;
}
.start {
  /* border-radius: 20px; */
  margin-top: 200px;
  padding: 15px 25px;
  font-size: 48px;
  text-align: center;
  cursor: pointer;
  outline: none;
  color: #fff;
  background-color: dodgerblue;
  border: none;
  border-radius: 15px;
  box-shadow: 0 9px #999;
}

.start:hover {
  background-color: royalblue;
}

/* .start:active {
  background-color: royalblue;
  box-shadow: 0 5px #666;
  transform: translateY(4px);
} */
.home {
  background-image: url("https://i.imgur.com/6Md6g05.jpg");
  background-repeat: no-repeat;
  background-attachment: fixed;
  background-position: center;
  background-size: cover;
  height: 100vh;
}
</style>