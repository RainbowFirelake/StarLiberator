using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShiftEffect : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private float _maxFOV;
    [SerializeField]
    private float _fovChangeSpeed = 10f;
    [SerializeField]
    private AudioSource _engineSoundSource;
    [SerializeField]
    private float _maxPitch = 2f;
    [SerializeField]
    private float _pitchChangeSpeed = 1f;
   
    private float _minFOV;
    private float _minPitch;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _minFOV = _camera.fieldOfView;
        _minPitch = _engineSoundSource.pitch;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            IncreaseFOV();
            IncreasePitch();
        }
        else
        {
            DecreaseFOV();
            DecreasePitch();
        }

    }

    private void IncreaseFOV()
    {
        if (_camera.fieldOfView < _maxFOV)
        {
            _camera.fieldOfView += _fovChangeSpeed * Time.deltaTime;
        }
    }

    private void IncreasePitch()
    {
        if (_engineSoundSource.pitch < _maxPitch)
        {
            _engineSoundSource.pitch += _pitchChangeSpeed * Time.deltaTime;
        }
    }

    private void DecreasePitch()
    {
        if (_engineSoundSource.pitch > _minPitch)
        {
            _engineSoundSource.pitch -= _pitchChangeSpeed * Time.deltaTime;
        }
    }

    private void DecreaseFOV()
    {
        if (_camera.fieldOfView > _minFOV)
        {
            _camera.fieldOfView -= _fovChangeSpeed * Time.deltaTime;
        }
    }
}
