export interface Cadastro {
  nome: string;
  sobrenome: string;
  email: string;
  senha: string;
  cpf: string;
  rg: string;
  endereco: {
    id: number;
    rua: string;
    numero: number;
    bairro: string;
    complemento: string;
    cep: string;
    cidade: string;
    estado: number;
  };
}
