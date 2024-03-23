import { AbstractControl, ValidationErrors, ValidatorFn, FormGroup } from "@angular/forms";

export function confirmarSenha(senhaControlName: string): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    const senha = control.value;
    if (!senha) {
      return null; // Senha vazia, validação passa
    }

    const formGroup = control.parent as FormGroup;
    if (!formGroup) {
      return null; // Não é um FormGroup, validação passa
    }

    const confirmSenhaControl = formGroup.get(senhaControlName);
    if (!confirmSenhaControl) {
      return null; // Controle de confirmação de senha não encontrado, validação passa
    }

    const confirmSenha = confirmSenhaControl.value;
    const senhasIguais = senha === confirmSenha;

    return senhasIguais ? null : { senhaNaoConfirmada: true };
  };
}
