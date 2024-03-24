import { Pipe, PipeTransform } from '@angular/core';
import { TransacaoTipo } from '../modelos/TransacaoTipo';

@Pipe({
  name: 'converterTransacao',
  standalone: true
})
export class ConverterTipoTransacaoPipe implements PipeTransform {

  transform(value: TransacaoTipo): string {
    switch (value) {
      case TransacaoTipo.SAQUE:
        return 'Saque';
      case TransacaoTipo.DEPOSITO:
        return 'Depósito';
      case TransacaoTipo.TRANSFERENCIA:
        return 'Transferência';
      default:
        return 'Tipo desconhecido';
    }
  }
}
