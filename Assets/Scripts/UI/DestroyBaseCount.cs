using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBaseCount : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Text _text;

    private void OnEnable()
    {
        SunSystemGeneration.OnCurrentEnemiesUpdate += UpdateText;
    }

    private void OnDisable()
    {
        SunSystemGeneration.OnCurrentEnemiesUpdate -= UpdateText;
    }

    private void UpdateText(int number)
    {
        _text.text = "Осталось врагов: " + Mathf.Max(0, number - 1).ToString();
    }
}
