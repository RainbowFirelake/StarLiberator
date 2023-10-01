using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitMarker : MonoBehaviour
{
    public static HitMarker Instance = null;

    [SerializeField]
    private Image _image;
    [SerializeField]
    private float _fadeOutTime = 0.5f;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void HitShow()
    {
        _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1);
    }

    private void Update()
    {
        if (_image.color.a > 0)
        {
            float newAlpha = _image.color.a - (Time.deltaTime / _fadeOutTime);
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, newAlpha);
        }
    }
}
