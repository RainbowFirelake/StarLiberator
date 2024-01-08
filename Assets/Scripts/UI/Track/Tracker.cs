using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour
{
    public bool IsFree
    {
        get => _currentTrackable == null;
    }

    [SerializeField]
    private Image _image;
    [SerializeField]
    private TrackableUnit _currentTrackable;

    private Camera _mainCam;
    private Transform _transform;

    private void Start()
    {
        _mainCam = Camera.main;
        _transform = transform;
    }

    private void OnValidate()
    {
        if (!TryGetComponent(out _image))
        {
            Debug.LogWarning($"There is no image component on {gameObject.name}");
        }
    }

    private void Update()
    {
        if (IsFree || !IsTrackableVisible())
        {
            DisableTrackerImage();
            return;
        }

        EnableTrackerImage();
        SetTrackerPosition();
    }

    public void InitTrackable(TrackableUnit trackable) => _currentTrackable = trackable;

    private void DisableTrackerImage() => _image.enabled = false;

    private void EnableTrackerImage() => _image.enabled = true;

    private void SetTrackerPosition() => 
        _transform.position = RectTransformUtility.WorldToScreenPoint(_mainCam, _currentTrackable.Position);

    private bool IsTrackableVisible()
    {
        var vector = _mainCam.WorldToViewportPoint(_currentTrackable.Position);

        if (vector.x >= 0 && vector.x <= 1 && vector.y >= 0 && vector.y <= 1 && vector.z >= 0)
        {
            return true;
        }

        return false;
    }
}
