export type PhoneType = 'Mobile' | 'Home' | 'Work';

export interface PhoneNumber {
  number: string;
  type: PhoneType;
  isPrimary: boolean;
}