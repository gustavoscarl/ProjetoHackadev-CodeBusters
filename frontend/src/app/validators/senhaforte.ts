import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function criarSenhaForte(): ValidatorFn{

  return (control: AbstractControl) : ValidationErrors | null => {
    const value = control.value;
    if(!value){
      return null;
    }

    const temMaiusculos = /[A-Z]+/.test(value);
    const temMinusculos = /[a-z]+/.test(value);
    const temNumericos = /[0-9]+/.test(value);

    const senhaValida = temMaiusculos && temMinusculos && temNumericos

    return !senhaValida ? { senhaForte : true } : null;
  }
}