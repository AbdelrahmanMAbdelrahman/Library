import { BookRes } from "./BookRes";

export interface CopyRes{
    id:string,
    book:BookRes,
    isAvailable:boolean
}