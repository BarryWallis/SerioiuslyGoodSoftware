namespace Exercise3Lib;
public class Power
{
    private int _value;

    public int Value
    {
        get => _value;
        set => _value = value < 0 ? throw new ArgumentOutOfRangeException(nameof(Value)) : value;
    }


}
