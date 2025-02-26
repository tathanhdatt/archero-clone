using Dt.Attribute;
using UnityEngine;
using UnityEngine.UI;

public class DamageReceiverHealthPointTracker : MonoBehaviour
{
    [SerializeField, Required]
    private DamageReceiver damageReceiver;

    [SerializeField, Required]
    private Image visual;

    private void LateUpdate()
    {
        float percentage = this.damageReceiver.CurrentHealth / this.damageReceiver.MaxHealth;
        this.visual.fillAmount = percentage;
    }
}