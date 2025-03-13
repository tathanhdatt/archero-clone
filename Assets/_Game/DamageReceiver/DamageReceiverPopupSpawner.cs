using System.Globalization;
using UnityEngine;

public class DamageReceiverPopupSpawner : MonoBehaviour
{
    [SerializeField]
    private TextPopup prefab;

    [SerializeField]
    private Color normalColor;

    [SerializeField]
    private Color criticalColor;
    
    [SerializeField]
    private float randomRadius;

    public void Create(DamageType type, float incomingDamage)
    {
        TextPopup textPopup = Instantiate(this.prefab, transform);
        Vector3 randomPos = Random.insideUnitCircle * this.randomRadius;
        randomPos.z = 0;
        textPopup.transform.localPosition = randomPos;
        switch (type)
        {
            case DamageType.Critical:
                textPopup.SetColor(this.criticalColor);
                break;
            case DamageType.NoType:
            case DamageType.Normal:
            case DamageType.Lightning:
            case DamageType.Poison:
            default:
                textPopup.SetColor(this.normalColor);
                break;
        }

        textPopup.Initialize((-incomingDamage).ToString(CultureInfo.InvariantCulture));
    }
}