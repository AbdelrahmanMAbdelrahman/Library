import { BorrowingRecordRes } from "../../BorrowingRecords/Models/BorrowingRecordRes"
import { PaymentStatus } from "../Enum/PaymentStatus"

export interface FineRes{
    id:string,
    borrowingRecordDto:BorrowingRecordRes,
    borrowingRecordId:string,
    numberOfLateDays:number,
    fineAmount:number,
    paymentStatus:PaymentStatus
}