using MFlight;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField]
    private Transform _sunPosition;
    [SerializeField]
    private List<GameObject> _sunPrefabs = new List<GameObject>();

    [SerializeField]
    private GameObject _emptyObject;
    //[SerializeField]
    //private Planet[] _planetPresets;
    [SerializeField]
    private List<ColourSettings> _colourSettings;
    [SerializeField]
    private ShapeSettings[] _shapeSettings;

    [SerializeField]
    private Player _player;
    [SerializeField]
    private List<Transform> _playerSpawnPositions;

    [SerializeField]
    private EnemyController[] _enemiesPrefabs;
    [SerializeField]
    private GameObject _enemyBase;

    public int _maxNumberOfPlanetsToGenerate = 4;
    public int _currentLevel = 0;
    private int _currentEnemiesNumber = 0;
    private bool _isFirstGenerate = true;

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
        Vector3 prevPoint = _sunPosition.position;
        Instantiate(_sunPrefabs[Random.Range(0, _sunPrefabs.Count)], _sunPosition.position, _sunPosition.rotation);
        var numberOfPlanetsToGenerate = Random.Range(1, _maxNumberOfPlanetsToGenerate + 1);
        var onWhichPlanetGenerateBase = Random.Range(0, numberOfPlanetsToGenerate);
        if (_isFirstGenerate)
        {
            onWhichPlanetGenerateBase = 0;
            _isFirstGenerate = false;
        }    
        for (int i = 0; i < numberOfPlanetsToGenerate; i++) 
        {
            var stepX = Random.Range(-9000, -13000);
            var stepZ = Random.Range(-3000, 3000);
            GameObject planet = Instantiate(_emptyObject, new Vector3(), Quaternion.identity);
            var planetComponent = planet.AddComponent<Planet>();
            var randColor = Random.Range(0, _colourSettings.Count);
            var randShape = Random.Range(0, _shapeSettings.Length);
            Debug.Log(randColor + ", " + randShape);
            planetComponent.SetColorAndShape(_colourSettings[randColor], _shapeSettings[randShape]);
            planetComponent.resolution = 64;
            planetComponent.GeneratePlanet();
            planet.GetComponent<PlanetBehaviour>().ChangeColliderRadius(planetComponent.shapeSettings.planetRadius);
            planet.transform.position = new Vector3(prevPoint.x + stepX, prevPoint.y, prevPoint.z + stepZ);
            planet.transform.rotation = Random.rotation;
            if (i == onWhichPlanetGenerateBase)
            {
                int x = Random.Range(-500, 500);
                int z = Random.Range(1500, 2500);
                int y = Random.Range(-700, 700);
                int enemyShipsCount = Random.Range(2 + _currentLevel, 4 + _currentLevel);
                var enemyBase = Instantiate(_enemyBase, new Vector3(
                    planet.transform.position.x + x, planet.transform.position.y + y, planet.transform.position.z + z), Random.rotation);
                _currentEnemiesNumber = 1;
                for (int k = 0; k < enemyShipsCount; k++)
                {
                    x = Random.Range(500, 1000);
                    y = Random.Range(-500, 500);
                    z = Random.Range(-250, 750);
                    var enemyShip = Instantiate(_enemiesPrefabs[Random.Range(0, _enemiesPrefabs.Length)], new Vector3(
                    enemyBase.transform.position.x + x, enemyBase.transform.position.y + y, enemyBase.transform.position.z + z), Quaternion.identity);
                    _currentEnemiesNumber++;
                }
                OnCurrentEnemiesUpdate?.Invoke(_currentEnemiesNumber);
            }
            prevPoint = planet.transform.position;
        }

        _player.transform.position = _playerSpawnPositions[Random.Range(0, _playerSpawnPositions.Count)].position;
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
