import { UserInfo } from "os";
import { BookRes } from "../../Books/Models/BookRes";
import { CopyRes } from "../../Books/Models/CopyRes";
import { AppUserInfo } from "../../Auth/Models/UserInfo";

export interface BorrowingRecordRes{
     
     id:string,
     borrowingDate:string,
     dueDate:string,
     actualReturnDate:string,
     copy:CopyRes,
     userInfo:AppUserInfo

    
}