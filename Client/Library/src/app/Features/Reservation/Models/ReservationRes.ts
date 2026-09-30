import { UserInfo } from "os";
import { CopyRes } from "../../Books/Models/CopyRes";
import { AppUserInfo } from "../../Auth/Models/UserInfo";

export interface ReservationRes{
    id:string,
    userInfo:AppUserInfo,
    copy:CopyRes
}