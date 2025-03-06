using System;

public abstract class ScriptableObjectVariable : ScriptableObjectArchitecture
{
    public abstract event Action OnValueChanged;
}