using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat Xp Database")]
public class CombatXpDatabase : ScriptableObject
{
    public List<int> levelXpRequired;
}