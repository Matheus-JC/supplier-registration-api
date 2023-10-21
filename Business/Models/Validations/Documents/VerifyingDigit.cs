namespace SupplierRegServer.Business.Models.Validations.Documents;

public class VerifyingDigit
{
    private string _number;
    private const int Module = 11;
    private readonly List<int> _multiplicators = new() { 2, 3, 4, 5, 6, 7, 8, 9 };
    private readonly IDictionary<int, string> _substitutions = new Dictionary<int, string>();
    private bool _moduleAddOn = true;

    public VerifyingDigit(string number)
    {
        _number = number;
    }

    public VerifyingDigit WithMultipliersFromTo(int firstMultiplicator, int lastMultiplicator)
    {
        _multiplicators.Clear();
        for (var i = firstMultiplicator; i <= lastMultiplicator; i++)
            _multiplicators.Add(i);

        return this;
    }

    public VerifyingDigit Replacing(string substitute, params int[] digits)
    {
        foreach (var i in digits)
        {
            _substitutions[i] = substitute;
        }
        return this;
    }

    public void AddDigit(string digit)
    {
        _number = string.Concat(_number, digit);
    }

    public string CalculatesDigit()
    {
        return !(_number.Length > 0) ? "" : GetDigitSum();
    }

    private string GetDigitSum()
    {
        var sum = 0;
        for (int i = _number.Length - 1, m = 0; i >= 0; i--)
        {
            var product = (int)char.GetNumericValue(_number[i]) * _multiplicators[m];
            sum += product;

            if (++m >= _multiplicators.Count) m = 0;
        }

        var mod = sum % Module;
        var result = _moduleAddOn ? Module - mod : mod;

        return _substitutions.ContainsKey(result) ? _substitutions[result] : result.ToString();
    }
}