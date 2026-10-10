import { PhoneNumber } from "./phone-number.model";

export interface ContactSummary {
    id: number;
    firstName: string;
    surname: string;
    country: string;
    city: string;
    primaryPhoneNumber: string;
}

export interface Contact {
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