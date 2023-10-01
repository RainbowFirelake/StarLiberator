using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreen : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private float time;


    private void Start()
    {
        StartCoroutine(BlackScreenEffect());
    }

    private IEnumerator BlackScreenEffect()
    {
        _canvasGroup.alpha = 1;
        yield return new WaitForSeconds(1f);
        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }
    }
}
