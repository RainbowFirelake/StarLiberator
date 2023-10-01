using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SideManager), typeof(Plane))]
public class Rammer : MonoBehaviour
{
    [SerializeField]
    private float _ramDamage;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _ramSFX;

    private Rigidbody _rb;
    private SideManager _side;
    private MFlight.Demo.Plane _plane;

    private void Start()
    {
        _side = GetComponent<SideManager>();
        _rb = GetComponent<Rigidbody>();
        _plane = GetComponent<MFlight.Demo.Plane>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_plane.IsShifted) return;

        var health = other.GetComponent<Health>();
        if (health == null) return;

        if (health.GetSide() == _side.GetSide()) return;
        if (health.IsDead()) return;

        var speed = _rb.velocity.z;
        health.TakeDamage(_ramDamage);

        if (_audioSource != null)
            _audioSource.Play();
        if (_ramSFX != null)
            Instantiate(_ramSFX, other.transform);
    }
}
