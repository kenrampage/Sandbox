using System;

public class SessionDataValue

{
    public object Value;
    public Type ValueType;

    public SessionDataValue(object value, Type valueType)
    {
        Value = value;
        ValueType = valueType;
    }

}
