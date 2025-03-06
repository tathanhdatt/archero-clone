using System.Linq;
using Dt.Attribute;
using UnityEngine;

public class GunDecoratorsActiveSetter : MonoBehaviour
{
    [SerializeField]
    private GunDecoratorItem[] items;

    [SerializeField, Required]
    private GunDecoratorsTriggerTable triggerTable;

    private void OnEnable()
    {
        RefreshAll();
        this.triggerTable.OnTriggerChanged += OnTriggerChangedHandler;
    }

    private void OnTriggerChangedHandler(GunDecoratorTrigger trigger)
    {
        ActiveDecorator(trigger);
    }

    public void RefreshAll()
    {
        foreach (GunDecoratorTrigger trigger in this.triggerTable.triggers)
        {
            ActiveDecorator(trigger);
        }
    }

    private void ActiveDecorator(GunDecoratorTrigger trigger)
    {
        GunDecorator decorator = GetDecorator(trigger.type);
        if (decorator == null)
        {
            Debug.LogWarning($"Can not find decorator of type {trigger.type}", gameObject);
            return;
        }

        if (!trigger.active) return;
        decorator.gameObject.SetActive(true);
        DisableConflictDecorators(trigger.conflictTypes);
    }

    private void DisableConflictDecorators(GunDecoratorType[] conflictTypes)
    {
        foreach (GunDecoratorItem item in this.items)
        {
            if (conflictTypes.Contains(item.type))
            {
                item.decorator.gameObject.SetActive(false);
            }
        }
    }

    private GunDecorator GetDecorator(GunDecoratorType type)
    {
        foreach (GunDecoratorItem item in this.items)
        {
            if (item.type == type)
            {
                return item.decorator;
            }
        }

        return null;
    }

    private void OnDisable()
    {
        this.triggerTable.OnTriggerChanged -= OnTriggerChangedHandler;
    }
}

[System.Serializable]
public class GunDecoratorItem
{
    public GunDecorator decorator;
    public GunDecoratorType type;
}