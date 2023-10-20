using UnityEngine;

namespace StarLiberator.Planets
{
    public class PlanetPivot : MonoBehaviour
    {
        [field : SerializeField]
        public PlanetBehaviour PlanetBehaviour { get; private set; }

        private Transform _transform;

        private void OnValidate()
        {
            PlanetBehaviour = GetComponentInChildren<PlanetBehaviour>();
            _transform = GetComponent<Transform>();
        }

        public void SetPositionForPlanet(float x, float y, float z)
        {
            PlanetBehaviour.transform.position = new Vector3(x, y, z);
        }

        public void SetYRotationAroundSun(Vector3 sunPos, float angle)
        {
            transform.RotateAround(sunPos, new Vector3(0, 1, 0), angle);
        }
    }
}
