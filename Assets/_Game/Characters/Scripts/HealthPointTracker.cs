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

    private void LateUpdate()
    {
        float percentage = this.currentHealth.Value / this.maxHealth.Value;
        this.visual.fillAmount = percentage;
    }
}