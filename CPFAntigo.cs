using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPF;

namespace CPF
{
    /// <summary>
    /// Implementação da interface CPF para o formato antigo do CPF (Antigo).
    /// </summary>
    public class CPFAntigo(string cpf9Digits) : ICPF
    {
        private readonly string _cpf9Digits = cpf9Digits;
        public string dv1 { get; private set; } = string.Empty;
        public string dv2 { get; private set; } = string.Empty;

        /// <summary>
        /// Implementação da interface CPF para o formato antigo (Antigo), 
        /// utilizando 9 dígitos e chamando o método interno EvaluateCPFDV2 
        /// para retornar o DV completo.
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public void EvaluateCPFDV()
        {
            if (string.IsNullOrEmpty(_cpf9Digits) || _cpf9Digits.Length != 9)
            {
                throw new ArgumentException("O CPF deve ser uma string contendo exatamente 9 dígitos.");
            }

            // Verifica se todos os caracteres são dígitos
            if (!_cpf9Digits.All(char.IsDigit))
            {
                throw new ArgumentException("O CPF deve conter apenas dígitos.");
            }

            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += (_cpf9Digits[i] - '0') * (10 - i);
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
            if (string.IsNullOrEmpty(_cpf9Digits + dv1) || (_cpf9Digits + dv1).Length != 10)
            {
                throw new ArgumentException("O CPF deve ser uma string contendo exatamente 10 dígitos.");
            }

            // Verifica se todos os caracteres são dígitos
            if (!((_cpf9Digits + dv1).All(char.IsDigit)))
            {
                throw new ArgumentException("O CPF deve conter apenas dígitos.");
            }

            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += ((_cpf9Digits + dv1)[i] - '0') * (11 - i);
            }

            int remainder = sum % 11;
            dv2 = remainder < 2 ? "0" : (11 - remainder).ToString();
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(_cpf9Digits) || string.IsNullOrEmpty(dv1) || string.IsNullOrEmpty(dv2))
            {
                throw new InvalidOperationException("O CPF deve ser calculado antes de ser convertido em string.");
            }

            return $"{_cpf9Digits}-{dv1}{dv2}";
        }
    }
}
