import { Injectable } from "@angular/core";
import { RegisterReq } from "../Models/RegisterReq";
import { Observable } from "rxjs";
import { HttpClient } from "@angular/common/http";
import { LoginReq } from "../Models/LoginReq";
import { AuthRes } from "../Models/AuthRes";
import { Failure } from "../../../Global/Failure";

@Injectable({providedIn:'root'})
export class AuthService {
url:string="https://localhost:7010/api/Identity";

constructor(private http:HttpClient) {}
SignUp(req:RegisterReq):Observable<any>{
return this.http.post(`${this.url}/register`,req);
}
SignIn(req:LoginReq):Observable<AuthRes>{
    
    return this.http.post<AuthRes>(`${this.url}/login`,req);
}
}