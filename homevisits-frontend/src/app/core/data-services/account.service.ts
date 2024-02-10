import { Injectable } from '@angular/core';
import { OAuthService, UserInfo } from 'angular-oauth2-oidc';
import { Profile } from '../auth/Profile';

@Injectable()
export class AccountService{

    userInfo: UserInfo;
    accessToken: string;
    constructor(private oauthService: OAuthService) {

    }
    // getProfile(){
    //     this.oauthService.loadUserProfile();
    //     let claims = this.oauthService.getIdentityClaims();
    //     if (!claims) { return null; }
    //     return claims.UserName;
    // }
    getUserInfo(){
        return this.userInfo;
    }
    setUserInfo(info: UserInfo){
        this.userInfo = info;
    }
    setAccessToken(token: string){
        this.accessToken = token;
    }
    getAccessToken(){
        return this.accessToken;
    }
}
