using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPF;

namespace CPF
{
    /// <summary>
    /// Implementação da interface CPF para o novo formato de CPF (2025).
    /// O que utiliza letras e números.
    /// </summary>
    public class CPF2025(string cpf9Digits) : ICPF
    {
        private readonly string _cpf9Digits = cpf9Digits?.Trim().ToUpperInvariant() ?? string.Empty;

        public string dv1 { get; private set; } = string.Empty;
        public string dv2 { get; private set; } = string.Empty;

        /// <summary>
        /// Calcula o dígito verificador (DV) do CPF fornecido.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public void EvaluateCPFDV()
        {
            string cpf9DigitsReformatted = _cpf9Digits;
            bool isValid = cpf9DigitsReformatted.All(c => char.IsDigit(c) || (c >= 'A' && c <= 'Z'));

            if (!isValid || cpf9DigitsReformatted.Length != 9)
            {
                throw new ArgumentException("O CPF deve ser uma string contendo exatamente 9 dígitos/caracteres.");
            }

            int sum = 0;
            for (int i = 0; i < 9; i++)
            {
                if (char.IsDigit(cpf9DigitsReformatted[i]))
                {
                    sum += (cpf9DigitsReformatted[i] - '0') * (10 - i);
                }
                else
                {
                    // Converte a letra em um número baseado em sua posição no alfabeto (A=10, B=11, ..., Z=35)
                    sum += (cpf9DigitsReformatted[i] - 'A' + 10) * (10 - i);
                }
            }

            int remainder = sum % 11;

            dv1 = remainder < 2 ? "0" : (11 - remainder).ToString();
        }

        /// <summary>
        /// Calcula o segundo dígito verificador (DV) do CPF fornecido.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public void EvaluateCPFDV2()
        {
            if (string.IsNullOrEmpty(dv1) || dv1.Length != 1)
                throw new ArgumentException("O primeiro DV deve ser um dígito.");

            if (string.IsNullOrEmpty(_cpf9Digits) || _cpf9Digits.Length != 9)
                throw new ArgumentException("O CPF deve ser uma string contendo exatamente 9 dígitos/caracteres.");

            string cpf10Digits = _cpf9Digits + dv1;
            cpf10Digits = cpf10Digits.Trim().ToUpperInvariant();

            if (!cpf10Digits.All(c => char.IsDigit(c) || (c >= 'A' && c <= 'Z')))
                throw new ArgumentException("O CPF deve conter apenas dígitos e letras maiúsculas.");

            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                int value = char.IsDigit(cpf10Digits[i]) ? (cpf10Digits[i] - '0') : (cpf10Digits[i] - 'A' + 10);
                sum += value * (11 - i);
            }

            int remainder = sum % 11;
            dv2 = remainder < 2 ? "0" : (11 - remainder).ToString();
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(_cpf9Digits) || string.IsNullOrEmpty(dv1) || string.IsNullOrEmpty(dv2))
            {
                throw new InvalidOperationException("O CPF não foi totalmente calculado.");
            }

            return $"{_cpf9Digits}-{dv1}{dv2}";
        }
    }
}
