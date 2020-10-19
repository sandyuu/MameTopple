import axios from "axios";
import {
    CONFIG
} from "../config";

export class LoginPageService {
    //POST 範例
    static async login1(payload) {
        const {data} = await axios.post(CONFIG.API_URL + `/api/Token/Login`, payload);
        return data;
    }

    //註冊POST
    static async register(payload) {
        const data = await axios.post(CONFIG.API_URL + `/api/User`, payload);
        return data;
    }

    //取得會員名字
    static async getUserName() {
        
        const token = sessionStorage.getItem('token');
        const config = {
            headers: { Authorization: `Bearer ${token}` }
        };
        const data = await axios.get(
            CONFIG.API_URL + "/api/Token/GetUserName", 
            config
        );
        return data;
    }


    // //GET 範例
    // static async getAccountInfo() {
    //
    //     const {data}   = await Request.get(
    //         CONFIG.API_URL + "/api/MemberSystem/getAccountInfo",
    //         {}
    //     );
    //
    //     return data;
    // }
    // //POST 範例
    // static async bookTime(payload) {
    //     const { data } = await Request.post(CONFIG.API_URL + `/api/resv`, payload);
    //     return data.data;
    // }
    //
    // //PUT 範例
    // static async updateAccountInfo(payload) {
    //     const { data } = await Request.put(
    //         CONFIG.API_URL + `/api/customer`,
    //         payload
    //     );
    //     return data.data;
    // }
    //
    // //Put FormData範例
    // static async updateAccountImg(FormData) {
    //     const config = { headers: { "Content-Type": "multipart/form-data" } };
    //     const postUrl = "/api/customer/uplimg";
    //     const { data } = await Request.put(
    //         CONFIG.API_URL + postUrl,
    //         FormData,
    //         config
    //     );
    //     return data.message;
    // }

}