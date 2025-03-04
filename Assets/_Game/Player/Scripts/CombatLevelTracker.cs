using Dt.Attribute;
using UnityEngine;
using UnityEngine.Events;

public class CombatLevelTracker : MonoBehaviour
{
    [SerializeField, Required]
    private CombatXpDatabase xpDatabase;

    [SerializeField, ReadOnly]
    private int currentXp;

    [SerializeField, Required]
    private IntVariable currentLevel;

    public UnityEvent<int> onLevelUp;
    public UnityEvent<float> onReceivedXpPercentage;

    public void Awake()
    {
        this.currentXp = 0;
    }

    public void AddXp(int xpToAdd)
    {
        this.currentXp += xpToAdd;
        int xpRequired = this.xpDatabase.levelXpRequired[this.currentLevel.Value - 1];
        this.onReceivedXpPercentage?.Invoke((float)this.currentXp / xpRequired);
        if (this.currentXp < xpRequired) return;
        bool isMaxLevel = this.currentLevel.Value >= this.xpDatabase.levelXpRequired.Count;
        if (isMaxLevel) return;
        this.currentLevel.Value += 1;
        this.currentXp = 0;
        this.onReceivedXpPercentage?.Invoke(0);
        this.onLevelUp?.Invoke(this.currentLevel.Value);
    }
}