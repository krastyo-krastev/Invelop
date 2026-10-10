import { ContactSummary } from "../models/contact.model";
import { PaginationDto } from "./pagination.dto";

export interface GetContactsSummaryResponse{
    data: ContactSummary[];
    pagination: PaginationDto;
}