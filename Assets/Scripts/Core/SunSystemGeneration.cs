using MFlight;
using StarLiberator.Planets;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class SunSystemGeneration : MonoBehaviour
{
    public static SunSystemGeneration Instance;
    public static event System.Action OnSunGenerate;
    public static event System.Action OnEnemiesDestroyed;
    public static event System.Action<int> OnCurrentEnemiesUpdate;
    public static event System.Action<int> OnLevelUpdate;

    public event System.Action<Vector3> OnPlanetGenerate;

    [SerializeField]
    private Transform _sun;
    [SerializeField]
    private List<GameObject> _starPrefabs = new List<GameObject>();

    [SerializeField]
    private List<PlanetPivot> _planetPrefabs = new();

    [SerializeField]
    private Transform _playerSpawnPosition;

    [SerializeField]
    private float _minDistanceBetweenPlanetsX = 12000;
    [SerializeField]
    private float _maxDistanceBetweenPlanetsX = 15000;

    [SerializeField]
    private float _minDistanceBetweenPlanetsZ = -3000;
    [SerializeField]
    private float _maxDistanceBetweenPlanetsZ = 3000;

    public int _minNumberOfPlanetsToGenerate = 2;
    public int _maxNumberOfPlanetsToGenerate = 4;
    public int _currentLevel = 0;
    private int _currentEnemiesNumber = 0;
    private bool _isFirstGenerate = true;

    private List<PlanetBehaviour> _currentPlanets = new();
    private Player _player;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }    
    }

    private void OnEnable()
    {
        Health.OnEnemyDie += OnEnemyDie;
    }

    private void OnDisable()
    {
        Health.OnEnemyDie -= OnEnemyDie;
    }

    private void Start()
    {
        RegenerateSunSystem();
        _isFirstGenerate = true;
    }

    public async void RegenerateSunSystem()
    {
        OnLevelUpdate?.Invoke(_currentLevel);
        Vector3 prevPoint = _sun.position;
        Instantiate(_starPrefabs[Random.Range(0, _starPrefabs.Count)], _sun.position, _sun.rotation);
        var numberOfPlanetsToGenerate = Random.Range(_minNumberOfPlanetsToGenerate, _maxNumberOfPlanetsToGenerate + 1);
        var onWhichPlanetGenerateBase = Random.Range(0, numberOfPlanetsToGenerate);

        if (_isFirstGenerate)
        {
            onWhichPlanetGenerateBase = 0;
            _isFirstGenerate = false;
        }   
        
        for (int i = 0; i < numberOfPlanetsToGenerate; i++) 
        {
            var stepX = Random.Range(_minDistanceBetweenPlanetsX, _maxDistanceBetweenPlanetsX);
            var stepZ = Random.Range(_minDistanceBetweenPlanetsZ, _maxDistanceBetweenPlanetsZ);

            PlanetPivot planet = Instantiate(_planetPrefabs[Random.Range(0, _planetPrefabs.Count)], _sun.position, Quaternion.identity);
            planet.PlanetBehaviour.Init(_sun.gameObject);
            
            planet.SetPositionForPlanet(prevPoint.x + stepX, prevPoint.y, prevPoint.z + stepZ);
            prevPoint = planet.PlanetBehaviour.transform.position;
            planet.SetYRotationAroundSun(_sun.position, Random.Range(0f, 360f));

            if (i == onWhichPlanetGenerateBase)
            {
                OnPlanetGenerate?.Invoke(planet.PlanetBehaviour.transform.position);
            }

        }

        var health = _player.GetComponent<Health>();
        health.SetHealth(health.GetMaxHealth());
        _currentLevel++;
        await Task.Yield();
    }

    public void DestroyObjects()
    {
        OnSunGenerate?.Invoke();
    }

    public void OnEnemyDie(Side side)
    {
        if (side == Side.Enemy)
        {
            _currentEnemiesNumber--;
            if (_currentEnemiesNumber == 0)
            {
                OnEnemiesDestroyed?.Invoke();
                OnLevelUpdate?.Invoke(_currentLevel);
            }
            OnCurrentEnemiesUpdate?.Invoke(_currentEnemiesNumber);
        }
    }
}
