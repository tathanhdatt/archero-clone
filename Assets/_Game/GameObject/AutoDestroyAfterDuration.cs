using System.Collections;
using UnityEngine;

public class AutoDestroyAfterDuration : MonoBehaviour
{
    [SerializeField]
    private float duration = 3f;

    private void OnEnable()
    {
        StartCoroutine(CoolDown());
    }

    private IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(this.duration);
        Destroy(gameObject);
    }
}