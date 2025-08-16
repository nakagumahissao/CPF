using CPF;


namespace CPFTest
{
    [TestClass]
    public class TestCPFApp
    {
        // Old CPFs
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
            { "A13353535", "CPF must contain only digits." }, // Invalid CPF
            { "8a1234568", "CPF must contain only digits." }, // Invalid CPF
            { "12", "CPF must be a string containing exactly 9 digits." }, // Invalid CPF
            { "a23;", "CPF must be a string containing exactly 9 digits." }, // Invalid CPF
            { "89123ZZZZ", "CPF must contain only digits." }, // Invalid CPF
            { "1234567890", "CPF must be a string containing exactly 9 digits." } // Invalid CPF
        };

        // Old CPFs composed of 10 digits
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
            { new List<string> { "", "" }, "CPF must be a string containing exactly 10 digits." }, // Empty CPF
            { new List<string> { "1234567890", "1" }, "CPF must be a string containing exactly 10 digits." }, // Too long CPF
            { new List<string> { "569122009", "" }, "CPF must be a string containing exactly 10 digits." }, // Invalid CPF
            { new List<string> { "569122009", "25" }, "CPF must be a string containing exactly 10 digits." },
            { new List<string> { "A13353535", "0" }, "CPF must contain only digits." }, // Invalid CPF
            { new List<string> { "8a1234568", "0" }, "CPF must contain only digits." }, // Invalid CPF
            { new List<string> { "12", "0" }, "CPF must be a string containing exactly 10 digits." }, // Invalid CPF
            { new List<string> { "a23;", "0" }, "CPF must be a string containing exactly 10 digits." }, // Invalid CPF
            { new List<string> { "89123ZZZZ", "0" }, "CPF must contain only digits." } // Invalid CPF
        };  

        // New CPFs
        static readonly Dictionary<string, string> newCPFs = new Dictionary<string, string>
        {
            { "A12345678", "81" },
            { "B98765432", "29" },
            { "C11122233", "98" },
            { "D00011122", "01" },
            { "E99988877", "63" },
            { "F00000000", "43" },
            { "G99999999", "56" },
            { "1A2B3C4D5", "50" }, // Mixed alphanumeric
            { "a1b2c3d4e", "61" }, // Mixed alphanumeric
            { "9Ab2cKef0", "60" }, // Mixed alphanumeric
            { "ZZZZZZZZZZ", "CPF must be a string containing exactly 9 digits/characters." }, // Invalid CPF
            { "Z\\", "CPF must be a string containing exactly 9 digits/characters." }, // Invalid CPF
            { "Z1Z2Z;Z4Z5", "CPF must be a string containing exactly 9 digits/characters." }, // Invalid CPF
            {"", "CPF must be a string containing exactly 9 digits/characters." } // Empty CPF

        };

        // New CPFs composed of 10 digits
        static readonly Dictionary<List<string>, string> newCPFs10Digits = new Dictionary<List<string>, string>
        {
            { new List<string> { "A12345678", "8" }, "1" },
            { new List<string> { "B98765432", "2" }, "9" },
            { new List<string> { "C11122233", "9" }, "8" },
            { new List<string> { "D00011122", "0" }, "1" },
            { new List<string> { "E99988877", "6" }, "3" },
            { new List<string> { "f00000000", "4" }, "3" },
            { new List<string> { "G99999999", "5" }, "6" },
            { new List<string> { "", "" }, "First DV must be a digit." }, // Empty CPF
            { new List<string> { "A123456789", "" }, "First DV must be a digit." }, // Too long CPF
            { new List<string> { "81A325DK5", "32" }, "First DV must be a digit." }, // Empty CPF
            { new List<string> { "81A325DK6", "" }, "First DV must be a digit." }, // Empty CPF
            { new List<string> { "", "35" }, "First DV must be a digit." }, // Empty CPF
            { new List<string> { "81A325;K5", "1" }, "CPF must contain only digits and uppercase letters." }, // Invalid CPF
            { new List<string> { "81A3:_DK5", "1" }, "CPF must contain only digits and uppercase letters." }, // Invalid CPF
            { new List<string> { "12", "0" }, "CPF must be a string containing exactly 9 digits/characters." }, // Invalid CPF
            { new List<string> { "a23;", "0" }, "CPF must be a string containing exactly 9 digits/characters." }, // Invalid CPF
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
                Assert.AreEqual(expectedDV, result, $"Failed for CPF: {cpf}. Expected result: {expectedDV}, but got: {result} - Final Result: {cpf}-{result}");
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
                Assert.AreEqual(expectedDV, result, $"Failed for CPF: {cpf}. Expected result: {expectedDV}, but got: {result} - Final Result: {cpf}-{result}");
            }
        }

        // Returns the 2 check digits (DVs) for the CPF format.
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

                Assert.AreEqual(expectedDV, result, $"Failed for CPF: {cpf9Digits}. Expected result: {expectedDV}, but got: {result} - Final Result: {cpf9Digits}-{firstDV}{result}");
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

                Assert.AreEqual(expectedDV, result, $"Failed for CPF: {cpf9Digits}. Expected result: {expectedDV}, but got: {result} - Final Result: {cpf9Digits}-{firstDV}{result}");
            }
        }
    }
}
