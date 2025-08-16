using CPF;

namespace CPFTest
{
    [TestClass]
    public class TestCPFApp
    {
        // CPFs Antigos
        static readonly Dictionary<string, string> oldCPFs = new Dictionary<string, string>
        {
            { "123456789", "09" },
            { "987654321", "00" },
            { "111222333", "96" },
            { "000111222", "85" },
            { "999888777", "14" },
            { "000000000", "00" },
            { "999999999", "99" },
            { "569122009", "82" },
            { "A13353535", "O CPF deve conter apenas dígitos." }, // CPF inválido
            { "8a1234568", "O CPF deve conter apenas dígitos." }, // CPF inválido
            { "12", "O CPF deve ser uma string contendo exatamente 9 dígitos." }, // CPF inválido
            { "a23;", "O CPF deve ser uma string contendo exatamente 9 dígitos." }, // CPF inválido
            { "89123ZZZZ", "O CPF deve conter apenas dígitos." }, // CPF inválido
            { "1234567890", "O CPF deve ser uma string contendo exatamente 9 dígitos." } // CPF inválido
        };

        // CPFs Antigos compostos por 10 dígitos
        static readonly Dictionary<List<string>, string> oldCPFs10Digits = new Dictionary<List<string>, string>
        {
            { new List<string> { "123456789", "0" }, "9" },
            { new List<string> { "987654321", "0" }, "0" },
            { new List<string> { "111222333", "9" }, "6" },
            { new List<string> { "000111222", "8" }, "5" },
            { new List<string> { "999888777", "1" }, "4" },
            { new List<string> { "000000000", "0" }, "0" },
            { new List<string> { "999999999", "9" }, "9" },
            { new List<string> { "569122009", "8" }, "2" },
            { new List<string> { "", "" }, "O CPF deve ser uma string contendo exatamente 10 dígitos." }, // CPF vazio
            { new List<string> { "1234567890", "1" }, "O CPF deve ser uma string contendo exatamente 10 dígitos." }, // CPF muito longo
            { new List<string> { "569122009", "" }, "O CPF deve ser uma string contendo exatamente 10 dígitos." }, // CPF inválido
            { new List<string> { "569122009", "25" }, "O CPF deve ser uma string contendo exatamente 10 dígitos." },
            { new List<string> { "A13353535", "0" }, "O CPF deve conter apenas dígitos." }, // CPF inválido
            { new List<string> { "8a1234568", "0" }, "O CPF deve conter apenas dígitos." }, // CPF inválido
            { new List<string> { "12", "0" }, "O CPF deve ser uma string contendo exatamente 10 dígitos." }, // CPF inválido
            { new List<string> { "a23;", "0" }, "O CPF deve ser uma string contendo exatamente 10 dígitos." }, // CPF inválido
            { new List<string> { "89123ZZZZ", "0" }, "O CPF deve conter apenas dígitos." } // CPF inválido
        };

        // CPFs Novos
        static readonly Dictionary<string, string> newCPFs = new Dictionary<string, string>
        {
            { "A12345678", "81" },
            { "B98765432", "29" },
            { "C11122233", "98" },
            { "D00011122", "01" },
            { "E99988877", "63" },
            { "F00000000", "43" },
            { "G99999999", "56" },
            { "1A2B3C4D5", "50" }, // Alfanumérico misto
            { "a1b2c3d4e", "61" }, // Alfanumérico misto
            { "9Ab2cKef0", "60" }, // Alfanumérico misto
            { "ZZZZZZZZZZ", "O CPF deve ser uma string contendo exatamente 9 dígitos/caracteres." }, // CPF inválido
            { "Z\\", "O CPF deve ser uma string contendo exatamente 9 dígitos/caracteres." }, // CPF inválido
            { "Z1Z2Z;Z4Z5", "O CPF deve ser uma string contendo exatamente 9 dígitos/caracteres." }, // CPF inválido
            {"", "O CPF deve ser uma string contendo exatamente 9 dígitos/caracteres." } // CPF vazio
        };

        // CPFs Novos compostos por 10 dígitos
        static readonly Dictionary<List<string>, string> newCPFs10Digits = new Dictionary<List<string>, string>
        {
            { new List<string> { "A12345678", "8" }, "1" },
            { new List<string> { "B98765432", "2" }, "9" },
            { new List<string> { "C11122233", "9" }, "8" },
            { new List<string> { "D00011122", "0" }, "1" },
            { new List<string> { "E99988877", "6" }, "3" },
            { new List<string> { "f00000000", "4" }, "3" },
            { new List<string> { "G99999999", "5" }, "6" },
            { new List<string> { "", "" }, "O primeiro DV deve ser um dígito." }, // CPF vazio
            { new List<string> { "A123456789", "" }, "O primeiro DV deve ser um dígito." }, // CPF muito longo
            { new List<string> { "81A325DK5", "32" }, "O primeiro DV deve ser um dígito." }, // CPF inválido
            { new List<string> { "81A325DK6", "" }, "O primeiro DV deve ser um dígito." }, // CPF inválido
            { new List<string> { "", "35" }, "O primeiro DV deve ser um dígito." }, // CPF vazio
            { new List<string> { "81A325;K5", "1" }, "O CPF deve conter apenas dígitos e letras maiúsculas." }, // CPF inválido
            { new List<string> { "81A3:_DK5", "1" }, "O CPF deve conter apenas dígitos e letras maiúsculas." }, // CPF inválido
            { new List<string> { "12", "0" }, "O CPF deve ser uma string contendo exatamente 9 dígitos/caracteres." }, // CPF inválido
            { new List<string> { "a23;", "0" }, "O CPF deve ser uma string contendo exatamente 9 dígitos/caracteres." }, // CPF inválido
            { new List<string> { "9Ab2cKef0", "6" }, "0" }
        };

        [TestMethod]
        public void TestOldCPF()
        {
            foreach (KeyValuePair<string, string> keyValuePair in oldCPFs)
            {
                string cpf = keyValuePair.Key;
                string expectedDV = keyValuePair.Value;
                ICPF icpf = new CPFAntigo(cpf);
                var result = GetFinalDV(icpf);
                Assert.AreEqual(expectedDV, result, $"Falha para o CPF: {cpf}. Resultado esperado: {expectedDV}, mas obteve: {result} - Resultado Final: {cpf}-{result}");
            }
        }

        [TestMethod]
        public void TestNewCPF()
        {
            foreach (KeyValuePair<string, string> keyValuePair in newCPFs)
            {
                string cpf = keyValuePair.Key;
                string expectedDV = keyValuePair.Value;
                ICPF icpf = new CPF2025(cpf);
                var result = GetFinalDV(icpf);
                Assert.AreEqual(expectedDV, result, $"Falha para o CPF: {cpf}. Resultado esperado: {expectedDV}, mas obteve: {result} - Resultado Final: {cpf}-{result}");
            }
        }

        // Retorna os 2 dígitos verificadores (DVs) do formato de CPF.
        public string GetFinalDV(ICPF icpf)
        {
            try
            {
                icpf.EvaluateCPFDV();
                icpf.EvaluateCPFDV2();
            }
            catch (ArgumentException ex)
            {
                return ex.Message;
            }

            return $"{icpf.dv1}{icpf.dv2}";
        }

        [TestMethod]
        public void TestOldCPFDV2()
        {
            foreach (KeyValuePair<List<string>, string> keyValuePair in oldCPFs10Digits)
            {
                List<string> cpfData = keyValuePair.Key;
                string cpf9Digits = cpfData[0];
                string firstDV = cpfData[1];
                string expectedDV = keyValuePair.Value;
                ICPF icpf = new CPFAntigo(cpf9Digits);

                string result;

                try
                {
                    icpf.EvaluateCPFDV2();
                    result = icpf.dv1 + icpf.dv2;
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }

                Assert.AreEqual(expectedDV, result, $"Falha para o CPF: {cpf9Digits}. Resultado esperado: {expectedDV}, mas obteve: {result} - Resultado Final: {cpf9Digits}-{firstDV}{result}");
            }
        }

        [TestMethod]
        public void TestNewCPFDV2()
        {
            foreach (KeyValuePair<List<string>, string> keyValuePair in newCPFs10Digits)
            {
                List<string> cpfData = keyValuePair.Key;
                string cpf9Digits = cpfData[0];
                string firstDV = cpfData[1];
                string expectedDV = keyValuePair.Value;
                ICPF icpf = new CPF2025(cpf9Digits);
                string result;

                try
                {
                    icpf.EvaluateCPFDV2();
                    result = icpf.dv1 + icpf.dv2;
                }
                catch (Exception ex)
                {
                    result = ex.Message;
                }

                Assert.AreEqual(expectedDV, result, $"Falha para o CPF: {cpf9Digits}. Resultado esperado: {expectedDV}, mas obteve: {result} - Resultado Final: {cpf9Digits}-{firstDV}{result}");
            }
        }
    }
}
