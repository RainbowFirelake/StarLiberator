using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpHider : MonoBehaviour
{
    public GameObject text;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            text.SetActive(!text.active);
        }    
    }
}
