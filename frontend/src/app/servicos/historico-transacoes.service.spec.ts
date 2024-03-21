import { TestBed } from '@angular/core/testing';

import { HistoricoTransacoesService } from './historico-transacoes.service';

describe('HistoricoTransacoesService', () => {
  let service: HistoricoTransacoesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HistoricoTransacoesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
