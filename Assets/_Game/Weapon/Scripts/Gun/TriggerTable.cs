using UnityEngine;

public abstract class TriggerTable : ScriptableObject
{
    public abstract void ResetTable();

    private void OnEnable()
    {
        ResetTable();
    }
}