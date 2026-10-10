import { ContactSummary } from "../models/contact.model";
import { PhoneNumber } from "../models/phone-number.model";
import { PaginationDto } from "./pagination.dto";

export interface GetContactsSummaryResponse{
    data: ContactSummary[];
    pagination: PaginationDto;
}

export interface GetContactResponse {
    id: number;
    firstName: string;
    surname: string;
    dateOfBirth: string;
    country: string;
    city: string;
    street: string;
    iban: string;
    phoneNumbers: PhoneNumber[];
}

export interface SaveContactRequest {
    id: string;    
    firstName: string;
    surname: string;
    dateOfBirth: string | null;
    country: string;
    city: string;
    street: string;
    iban: string;
    phoneNumbers: PhoneNumber[];
}
