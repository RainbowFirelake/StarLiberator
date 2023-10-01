using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyperDriveJump : MonoBehaviour
{
    public static event Action<bool> OnHyperDriveJumpAvailable;

    [SerializeField]
    private GameObject _go;
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private float _time;

    private bool _active = false;

    private bool _jumpAvailable = false;

    private void OnEnable()
    {
        SunSystemGeneration.OnEnemiesDestroyed += EnableJump;
    }

    private void OnDisable()
    {
        SunSystemGeneration.OnEnemiesDestroyed -= EnableJump;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && _jumpAvailable)
        {
            StartHyperJump();
        }
    }

    public void StartHyperJump()
    {
        _go.SetActive(true);
        _active = true;
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
        DisableJump();
        StartCoroutine(HyperJump());
    }

    private IEnumerator HyperJump()
    {
        SunSystemGeneration.Instance.DestroyObjects();
        yield return new WaitForSeconds(3f);
        SunSystemGeneration.Instance.RegenerateSunSystem();
        _go.SetActive(false);
    }

    private void EnableJump()
    {
        _jumpAvailable = true;
        OnHyperDriveJumpAvailable?.Invoke(true);
    }

    private void DisableJump()
    {
        _jumpAvailable = false;
        OnHyperDriveJumpAvailable?.Invoke(false);
    }
}
