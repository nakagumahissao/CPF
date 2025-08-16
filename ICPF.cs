using System.Runtime.CompilerServices;

namespace CPF
{
    /// <summary>
    /// Interface for CPF (Common Programming Framework).
    /// It serves as a generic interface for CPF-related functionalities.
    /// There are two versions of CPF. The old one composed of only numbers and the new one
    /// composed of letters and numbers.
    /// By means of this interface, we can ensure that both versions are covered under a single type.
    /// </summary>
    public interface ICPF
    {
        /// <summary>
        /// Evaluates the CPF check digit (DV) for the CPF string provided - Expected string, array or list containing 9 digits and or characters.
        /// </summary>
        /// <param name="cpf9Digits">The CPF in string format containing 9 digits and or characters</param>
        public string EvaluateCPFDV(string cpf9Digits);

        /// <summary>
        /// Evaluates the CPF check digit (DV) for the CPF string provided - Expected string, array or list containing 9 digits plus the 10th digit.
        /// </summary>
        /// <param name="cpf10Digits">The CPF in string format containing 9 digits plus the 10th digit</param>
        /// <returns></returns>
        public string EvaluateCPFDV2(string cpf9Digits, string firstDV);
    }
}
