using System.Globalization;
using Dt.Attribute;
using TMPro;
using UnityEngine;

public class FloatVariableTextDisplayTracker : MonoBehaviour
{
    [SerializeField, Required]
    private FloatVariable floatVariable;

    [SerializeField, Required]
    private TMP_Text textDisplay;

    [SerializeField]
    private string format = "{0}";

    private void Awake()
    {
        this.textDisplay.SetText(GetDisplayText());
        this.floatVariable.OnValueChanged += OnValueChangedHandler;
    }

    [Button]
    private void OnValueChangedHandler()
    {
        this.textDisplay.SetText(GetDisplayText());
    }

    private string GetDisplayText()
    {
        return string.Format(
            CultureInfo.InvariantCulture,
            this.format,
            this.floatVariable.Value);
    }
}