using System;
using Dt.Attribute;
using UnityEngine;

[Serializable]
public class FloatReference
{
    public bool useConstant;
    [SerializeField]// ShowIf(nameof(useConstant))]
    private float initValue;
    [SerializeField]//ShowIf(nameof(useConstant))]
    private float value;
    [SerializeField]
    private FloatVariable variable;
    
    public float Value
    {
        get => this.useConstant ? this.value : this.variable.Value;
        set
        {
            if (this.useConstant)
            {
                this.value = value;
            }
            else
            {
                this.variable.Value = value;
            }
        }
    }

    public void ResetValue()
    {
        if (this.useConstant)
        {
            this.value = this.initValue;
        }
        else
        {
            this.variable.ResetValue();
        }
    }
}