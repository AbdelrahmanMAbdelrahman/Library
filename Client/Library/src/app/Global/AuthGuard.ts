import { inject, PLATFORM_ID } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { AuthStateService } from "../Features/Auth/Services/AuthStateService";
import { isPlatformBrowser } from "@angular/common";


export const AuthGuard:CanActivateFn=()=>{
let authState=inject(AuthStateService);
let router=inject(Router);
const platformId=inject(PLATFORM_ID);
const authenticated=authState.isAuthenticated();
if(!isPlatformBrowser(platformId))return true;
if(authenticated)return true;
return router.createUrlTree(['/AuthPage','SignIn']);
}