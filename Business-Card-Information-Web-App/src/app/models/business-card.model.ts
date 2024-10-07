export interface BusinessCard {
  id: number;
  name: string;
  gender: string;
  dateOfBirth: Date;
  email: string;
  phone: string;
  address: string;
  photoBase64?: string;
}
