using System.Runtime.CompilerServices;

namespace CPF
{
    /// <summary>
    /// Interface para CPF (Common Programming Framework).
    /// Serve como uma interface genérica para funcionalidades relacionadas ao CPF.
    /// Existem duas versões do CPF: a antiga, composta apenas por números, 
    /// e a nova, composta por letras e números.
    /// Por meio desta interface, podemos garantir que ambas as versões 
    /// sejam tratadas sob um único tipo.
    /// </summary>
    public interface ICPF
    {
        public string dv1 { get; }
        public string dv2 { get; }

        /// <summary>
        /// Calcula o dígito verificador (DV) do CPF fornecido.  
        /// Espera-se uma string, array ou lista contendo exatamente 9 dígitos e/ou caracteres.
        /// </summary>
        /// <param name="cpf9Digits">O CPF em formato string contendo 9 dígitos e/ou caracteres</param>
        public void EvaluateCPFDV();

        /// <summary>
        /// Calcula o segundo dígito verificador (DV) do CPF fornecido.  
        /// Espera-se uma string, array ou lista contendo 9 dígitos mais o 10º dígito.
        /// </summary>
        /// <param name="cpf10Digits">O CPF em formato string contendo 9 dígitos mais o 10º dígito</param>
        public void EvaluateCPFDV2();

        public string ToString();
    }
}
