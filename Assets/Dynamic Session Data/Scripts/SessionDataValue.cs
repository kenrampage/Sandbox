public class SessionDataValue
{
    public SessionDataValueType Type;
    public object Value;

    public SessionDataValue(SessionDataValueType type, object value)
    {
        Type = type;
        Value = value;
    }
}
