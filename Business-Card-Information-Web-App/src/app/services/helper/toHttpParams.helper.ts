import { HttpParams } from '@angular/common/http';

export function toHttpParams(filter: any): HttpParams {
  let params = new HttpParams();

  for (const key of Object.keys(filter)) {
    const value = filter[key];
    if (value != null) {
      params = params.append(key, value);
    }
  }

  return params;
}
