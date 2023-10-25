using System;
using UnityEngine;
using Zenject;

namespace StarLiberator.Enemies
{

    [RequireComponent(typeof(Rigidbody), typeof(Shooter))]
    public class EnemyController : MonoBehaviour
    {
        public event Action<EnemyController> OnKilled;

        [SerializeField]
        private Shooter _shooter;
        [SerializeField]
        private Transform _currentTarget = null;
        [SerializeField]
        private TargetingHelper _targetingHelper;
        [SerializeField]
        private float _flySpeed = 50;
        [SerializeField]
        private float _forceMult = 90;
        [SerializeField]
        private float _rotationSpeed;
        [SerializeField]
        private float _rotationDistance = 600f;
        [SerializeField]
        private float _shootDistance = 2000f;

        private Transform _transform;
        private Rigidbody _rb;
        private Rigidbody _targetRb;

        private bool _isRotatingToPlayer = false;
        private bool _isMovingToPlayer = false;

        public void Init(Player player)
        {
            _currentTarget = player.transform;
            _targetingHelper.Initialize(_transform);
            _targetingHelper.SetTarget(_currentTarget);
            _targetRb = _currentTarget.GetComponent<Rigidbody>();
        }

        private void Start()
        {
            _transform = GetComponent<Transform>();
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            var distance = .0f;
            if (_currentTarget != null)
            {
                distance = Vector3.Distance(_currentTarget.position, _transform.position);
            }
            if (_currentTarget != null && distance < 10000f)
            {
                _isMovingToPlayer = true;
                if (distance > _rotationDistance)
                {
                    _isRotatingToPlayer = true;
                    var rotate = Quaternion.LookRotation(_currentTarget.position - _transform.position);
                    _transform.rotation = Quaternion.Slerp(_transform.rotation, rotate, Time.deltaTime * _rotationSpeed);
                }
                else
                {
                    _isRotatingToPlayer = false;
                }
                if (distance < _shootDistance)
                {
                    _shooter.SetAimDirection(GetPredictedTargetPosition(distance));
                    _shooter.Shoot();
                }
            }
        }
        
        private void FixedUpdate()
        {
            if (_isMovingToPlayer)
            {
                _rb.AddRelativeForce(Vector3.forward * _flySpeed * _forceMult);
            }
        }

        private Vector3 GetPredictedTargetPosition(float distance)
        {
            var time = distance / _shooter.GetBulletSpeed();
            return _currentTarget.position + (_targetRb.velocity * time);
        }

        private void OnDestroy()
        {
            OnKilled?.Invoke(this);
        }
    }
}