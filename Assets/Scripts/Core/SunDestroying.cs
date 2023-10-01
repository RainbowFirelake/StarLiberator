using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDestroying : MonoBehaviour
{
    private void OnEnable()
    {
        SunSystemGeneration.OnSunGenerate += DestroySun;
    }

    private void OnDisable()
    {
        SunSystemGeneration.OnSunGenerate -= DestroySun;
    }

    public void DestroySun()
    {
        Destroy(this.gameObject);
    }
}
