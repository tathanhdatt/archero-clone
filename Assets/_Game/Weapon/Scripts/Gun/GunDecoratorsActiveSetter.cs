using Dt.Attribute;
using UnityEngine;

public class GunDecoratorsActiveSetter : MonoBehaviour
{
    [SerializeField]
    private TypeGunDecoratorDict items;

    [SerializeField, Required]
    private GunDecoratorsTriggerTable triggerTable;

    private void OnEnable()
    {
        RefreshAll();
        this.triggerTable.OnTriggerActiveChanged += OnTriggerChangedHandler;
    }

    private void OnTriggerChangedHandler(GunDecoratorType type, bool active)
    {
        this.items[type].gameObject.SetActive(active);
    }

    public void RefreshAll()
    {
        foreach (GunDecoratorType type in this.triggerTable.triggersDict.Keys)
        {
            bool isActive = this.triggerTable.triggersDict[type];
            this.items[type].gameObject.SetActive(isActive);
        }
    }


    private void OnDisable()
    {
        this.triggerTable.OnTriggerActiveChanged -= OnTriggerChangedHandler;
    }
}