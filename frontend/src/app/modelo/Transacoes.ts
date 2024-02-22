// Interface
export interface Transacao {
    // Atributos
    nome?:string;
    descricao?:string;
    tipo?:string;
    valor?:string;
    contaDestino?:number;
    data:Date|string;
}

