export class Error {
  code: string;
  extraMessage?: string;
  parameters?: any[];

  constructor(code: string, extraMessage?: string, parameters?: any[]) {
    this.code = code;
    this.extraMessage = extraMessage;
    this.parameters = parameters || [];
  }
}
