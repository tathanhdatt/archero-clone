using Dt.Attribute;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointTracker : InitializableMono
{
    [SerializeField, Required]
    private FloatVariable maxHealth;

    [SerializeField, Required]
    private FloatVariable currentHealth;

    [SerializeField, Required]
    private Image visual;
    
    [SerializeField, Required]
    private TMP_Text text;

    public override void Initialize()
    {
        this.maxHealth.OnValueChanged += OnValueChangedHandler;
        this.currentHealth.OnValueChanged += OnValueChangedHandler;
    }

    private void OnValueChangedHandler()
    {
        float percentage = this.currentHealth.Value / this.maxHealth.Value;
        this.visual.fillAmount = percentage;
        this.text.SetText(((int)this.currentHealth.Value).ToString());
    }

    public override void Terminate()
    {
        this.maxHealth.OnValueChanged -= OnValueChangedHandler;
        this.currentHealth.OnValueChanged -= OnValueChangedHandler;
    }
}