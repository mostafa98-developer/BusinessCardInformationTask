import { ResultCode } from "../../shared/Enums/ResultCode.enums";
import { Error } from "./error.common";


export class ServiceResultBase {
  status: ResultCode;
  errors: Error[];

  constructor(status: ResultCode = ResultCode.Ok) {
    this.status = status;
    this.errors = []; // Initialize errors
  }

  get isSucceed(): boolean {
    return this.status === ResultCode.Ok ||
           this.status === ResultCode.NoContent ||
           this.status === ResultCode.Created;
  }

  get hasErrors(): boolean {
    return this.errors.length > 0;
  }

  addErrors(...codes: string[]): void {
    const newErrors = codes.map(code => new Error(code));
    this.errors.push(...newErrors);
  }

  addErrorWithExtraMessage(extraMessage: string, ...codes: string[]): void {
    const newErrors = codes.map(code => new Error(code, extraMessage));
    this.errors.push(...newErrors);
  }

  addErrorWithParams(code: string, ...parameters: any[]): void {
    const error = new Error(code, undefined, parameters);
    this.errors.push(error);
  }

  addErrorsList(errors: Error[]): void {
    this.errors.push(...(errors || []));
  }

  addError(error: Error): void {
    if (error) {
      this.errors.push(error);
    }
  }
}

export class ServiceResult<T> extends ServiceResultBase {
  data?: T;

  constructor(status: ResultCode = ResultCode.Ok, data?: T) {
    super(status);
    this.data = data;
  }
}
