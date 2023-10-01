using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperJumpText : MonoBehaviour
{
    [SerializeField]
    private GameObject text;

    private void OnEnable()
    {
        HyperDriveJump.OnHyperDriveJumpAvailable += ActivateHyperJumpText;
    }

    private void OnDisable()
    {
        HyperDriveJump.OnHyperDriveJumpAvailable -= ActivateHyperJumpText;
    }

    private void ActivateHyperJumpText(bool isActive)
    {
        text.SetActive(isActive);
    }
}
