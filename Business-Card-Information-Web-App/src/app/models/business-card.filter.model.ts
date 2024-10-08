import { BaseFilter } from "./Base.filter.model";

export class BusinessCardFilter extends BaseFilter {
  dob?: Date;
  phone?: string;
  gender?: string;
}
