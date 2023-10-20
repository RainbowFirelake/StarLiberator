using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlanetBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _minRotationSpeed = 1f;
    [SerializeField]
    private float _maxRotationSpeed = 2f;
    [SerializeField]
    private SphereCollider _sphereCollider;
    [SerializeField]
    private LightSource _lightSource;

    private float _currentRotationSpeed = 1f;
    private Transform _transform;

    private void Start()
    {
        _currentRotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed + 1);
        _transform = GetComponent<Transform>();
    }

    private void OnEnable()
    {
        SunSystemGeneration.OnSunGenerate += DestroyPlanet;
    }

    private void OnDisable()
    {
        SunSystemGeneration.OnSunGenerate -= DestroyPlanet;
    }

    private void OnValidate()
    {
        if (!TryGetComponent<LightSource>(out _lightSource))
        {
            Debug.LogWarning("There is no light source on this object", this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _transform.eulerAngles += new Vector3(0, _currentRotationSpeed * Time.deltaTime, 0);
    }

    public void ChangeColliderRadius(float radius)
    {
        _sphereCollider.radius = radius;
    }

    public void Init(GameObject sun)
    {
        _lightSource.Sun = sun;
    }

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<Health>();
        if (health)
        {
            health.TakeDamage(100000);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var health = collision.gameObject.GetComponent<Health>();
        if (health)
        {
            health.TakeDamage(100000);
        }
    }

    private void DestroyPlanet()
    {
        Destroy(this.gameObject);
    }
}
