export interface Cliente {
  id?: number;
  nome?: string;
  sobrenome?: string;
  email?: string;
  temConta?: boolean;
  temCartao?: boolean;
  contaId?: number | null;
  contaUrl?: string;
}
