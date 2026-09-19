import { isPlatformBrowser } from "@angular/common";
import { computed, inject, Injectable,PLATFORM_ID  } from "@angular/core";
@Injectable({providedIn:'root'})
export class AuthStateService{
    // private token=signal<string|null>(null);
    private token:string="accessToken";
    private refToken:string="refreshToken"
    private plateformId=inject(PLATFORM_ID)
    isAuthenticated():boolean{return!!this.getToken();;}

    setToken(token:string,refreshToken:string){
        if(isPlatformBrowser(this.plateformId)){
        localStorage.setItem(this.token,token);}
        localStorage.setItem(this.refToken,refreshToken);
    }
    getToken(){
        if(isPlatformBrowser(this.plateformId)){
        return localStorage.getItem(this.token);}
        return "";
    }
    getRefreshToken(){
        if(isPlatformBrowser(this.plateformId)){
        return localStorage.getItem(this.refToken);}
        return "";
    }
    clearToken(){
        if(isPlatformBrowser(this.plateformId))
        localStorage.removeItem(this.token);
    }
}