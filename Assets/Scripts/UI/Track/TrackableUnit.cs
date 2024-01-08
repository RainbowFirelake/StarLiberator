using System;
using UnityEngine;
using Zenject;

public class TrackableUnit : MonoBehaviour
{
    public event Action<TrackableUnit> OnDisableTracking;

    public Vector3 Position 
    { 
        get => _transform.position;  
    }

    private Transform _transform;

    [Inject]
    private void Construct(RectangleOnUnits rectOnUnits)
    {
        rectOnUnits.RegisterNewTrackable(this);
    }

    private void Start()
    {
        _transform = transform; 
    }

    private void OnDestroy()
    {
        OnDisableTracking?.Invoke(this);
    }
}
