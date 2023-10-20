using System;

public class PersistentData
{
    public object Value;
    public Type ValueType;

    public PersistentData(object value, Type valueType)
    {
        Value = value;
        ValueType = valueType;
    }

}
