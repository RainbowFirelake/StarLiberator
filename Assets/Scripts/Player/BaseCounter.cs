using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseCounter : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text _text;

    private void OnEnable()
    {
        SunSystemGeneration.OnLevelUpdate += UpdateText;
    }

    private void OnDisable()
    {
        SunSystemGeneration.OnLevelUpdate -= UpdateText;
    }

    private void UpdateText(int count)
    {
        _text.text = "”ничтожено баз: " + count.ToString();
    }
}
