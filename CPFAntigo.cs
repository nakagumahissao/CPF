using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPF;

namespace CPF
{
    /// <summary>
    /// Implementation of the CPF interface for the old CPF format (Antigo).
    /// </summary>
    public class CPFAntigo(string cpf9Digits) : ICPF
    {
        private readonly string _cpf9Digits = cpf9Digits;

        /// <summary>
        /// Implementation of the CPF interface for the old CPF format (Antigo) using 9 digits and passing to the internal method EvaluateCPFDV2 and returns the full DV
        /// </summary>
        /// <param name="cpf9Digits">The first 9 digits of the CPF</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string EvaluateCPFDV()
        {
            if (string.IsNullOrEmpty(_cpf9Digits) || _cpf9Digits.Length != 9)
            {
                throw new ArgumentException("CPF must be a string containing exactly 9 digits.");
            }

            // Check if all characters are digits
            if (!_cpf9Digits.All(char.IsDigit))
            {
                throw new ArgumentException("CPF must contain only digits.");
            }

            int sum = 0;

            for (int i = 0; i < 9; i++)
            {
                sum += (_cpf9Digits[i] - '0') * (10 - i);
            }

            int remainder = sum % 11;
            return remainder < 2 ? "0" : (11 - remainder).ToString();
        }

        /// <summary>
        /// Evaluates the second check digit (DV) for the CPF string provided.
        /// </summary>
        /// <param name="cpf9Digits">The first 9 digits of the CPF</param>
        /// <param name="firstDV">The first digit of the DV evaluated</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string EvaluateCPFDV2(string firstDV)
        {
            if (string.IsNullOrEmpty(_cpf9Digits + firstDV) || (_cpf9Digits + firstDV).Length != 10)
            {
                throw new ArgumentException("CPF must be a string containing exactly 10 digits.");
            }

            // Check if all characters are digits
            if (!((_cpf9Digits + firstDV).All(char.IsDigit)))
            {
                throw new ArgumentException("CPF must contain only digits.");
            }

            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += ((_cpf9Digits + firstDV)[i] - '0') * (11 - i);
            }

            int remainder = sum % 11;
            return remainder < 2 ? "0" : (11 - remainder).ToString();
        }
    }
}
