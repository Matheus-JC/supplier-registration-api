namespace SupplierRegServer.Business.Models.Validations.Documents;

public class CpfValidator
{
    public const int CpfLength = 11;

    public static bool Validate(string cpf)
    {
        var cpfNumbers = Utils.OnlyNumbers(cpf);

        if (!HasValidLength(cpfNumbers)) return false;
        return !HasRepeatedDigits(cpfNumbers) && HasValidDigits(cpfNumbers);
    }

    private static bool HasValidLength(string value)
    {
        return value.Length == CpfLength;
    }

    private static bool HasRepeatedDigits(string value)
    {
        string[] invalidNumbers =
        {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };
        return invalidNumbers.Contains(value);
    }

    private static bool HasValidDigits(string value)
    {
        var number = value.Substring(0, CpfLength - 2);
        var verifyingDigit = new VerifyingDigit(number)
            .WithMultipliersFromTo(2, 11)
            .Replacing("0", 10, 11);
        var firstDigit = verifyingDigit.CalculatesDigit();
        verifyingDigit.AddDigit(firstDigit);
        var secondDigit = verifyingDigit.CalculatesDigit();

        return string.Concat(firstDigit, secondDigit) == value.Substring(CpfLength - 2, 2);
    }
}