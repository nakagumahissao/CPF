using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPF;

namespace CPF
{
    /// <summary>
    /// Implementation of the CPF interface for the new CPF format (2025).
    /// The one that uses letters and numbers.
    /// </summary>
    public class CPF2025(string cpf9Digits) : ICPF
    {
        private readonly string _cpf9Digits = cpf9Digits;

        /// <summary>
        /// Evaluates the CPF check digit (DV) for the CPF string provided.
        /// </summary>
        /// <param name="cpf9Digits">CPF with 9 digits</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string EvaluateCPFDV()
        {
            string cpf9DigitsReformatted = _cpf9Digits?.Trim().ToUpperInvariant() ?? string.Empty;
            bool isValid = cpf9DigitsReformatted.All(c => char.IsDigit(c) || (c >= 'A' && c <= 'Z'));

            if (!isValid || cpf9DigitsReformatted.Length != 9)
            {
                throw new ArgumentException("CPF must be a string containing exactly 9 digits/characters.");
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
                    // Convert letter to a number based on its position in the alphabet (A=10, B=12, ..., Z=35)
                    sum += (cpf9DigitsReformatted[i] - 'A' + 10) * (10 - i);
                }
            }

            int remainder = sum % 11;
            return remainder < 2 ? "0" : (11 - remainder).ToString();
        }

        /// <summary>
        /// Evaluates the second check digit (DV) for the CPF string provided.
        /// </summary>
        /// <param name="cpf9Digits">CPF with 9 digits</param>
        /// <param name="firstDV">The first DV Evaluated</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string EvaluateCPFDV2(string firstDV)
        {
            if (string.IsNullOrEmpty(firstDV) || firstDV.Length != 1)
                throw new ArgumentException("First DV must be a digit.");

            if (string.IsNullOrEmpty(_cpf9Digits) || _cpf9Digits.Length != 9)
                throw new ArgumentException("CPF must be a string containing exactly 9 digits/characters.");

            string cpf10Digits = _cpf9Digits + firstDV;
            cpf10Digits = cpf10Digits.Trim().ToUpperInvariant();

            if (!cpf10Digits.All(c => char.IsDigit(c) || (c >= 'A' && c <= 'Z')))
                throw new ArgumentException("CPF must contain only digits and uppercase letters.");

            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                int value = char.IsDigit(cpf10Digits[i]) ? (cpf10Digits[i] - '0') : (cpf10Digits[i] - 'A' + 10);
                sum += value * (11 - i);
            }

            int remainder = sum % 11;
            return remainder < 2 ? "0" : (11 - remainder).ToString();
        }
    }
}
