namespace SupplierRegServer.Business.Models.Validations.Documents;

public class CnpjValidator
{
    public const int CnpjLength = 14;

    public static bool Validate(string cpnj)
    {
        var cnpjNumbers = Utils.OnlyNumbers(cpnj);

        if (!HasValidLength(cnpjNumbers)) return false;
        return !HasRepeatedDigits(cnpjNumbers) && HasValidDigits(cnpjNumbers);
    }

    private static bool HasValidLength(string value)
    {
        return value.Length == CnpjLength;
    }

    private static bool HasRepeatedDigits(string value)
    {
        string[] invalidNumbers =
        {
                "00000000000000",
                "11111111111111",
                "22222222222222",
                "33333333333333",
                "44444444444444",
                "55555555555555",
                "66666666666666",
                "77777777777777",
                "88888888888888",
                "99999999999999"
            };
        return invalidNumbers.Contains(value);
    }

    private static bool HasValidDigits(string value)
    {
        var number = value[..(CnpjLength - 2)];

        var verifyingDigit = new VerifyingDigit(number)
            .WithMultipliersFromTo(2, 9)
            .Replacing("0", 10, 11);
        var firstDigit = verifyingDigit.CalculatesDigit();
        verifyingDigit.AddDigit(firstDigit);
        var secondDigit = verifyingDigit.CalculatesDigit();

        return string.Concat(firstDigit, secondDigit) == value.Substring(CnpjLength - 2, 2);
    }
}