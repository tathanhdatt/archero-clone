using Dt.Attribute;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointTracker : MonoBehaviour
{
    [SerializeField, Required]
    private FloatVariable maxHealth;

    [SerializeField, Required]
    private FloatVariable currentHealth;

    [SerializeField, Required]
    private Image visual;

    private void OnEnable()
    {
        this.currentHealth.OnValueChanged += OnValueChangedHandler;
    }

    private void OnValueChangedHandler()
    {
        float percentage = this.currentHealth.Value / this.maxHealth.Value;
        this.visual.fillAmount = percentage;
    }

    private void OnDisable()
    {
        this.currentHealth.OnValueChanged -= OnValueChangedHandler;
    }
}