using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField]
    private List<InitializableMono> initializables;

    private void OnEnable()
    {
        this.initializables.ForEach(initializable => initializable.Initialize());
    }

    private void OnDisable()
    {
        this.initializables.ForEach(init => init.Terminate());
    }
}