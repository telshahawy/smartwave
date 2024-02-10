import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'keys'
})
export class KeysPipe implements PipeTransform {

  transform(value: Object, args?: number[]): number[]
 {

    return Object.keys(value).filter(f => !isNaN(Number(f)))
    .map(k => parseInt(k));
  }


}
