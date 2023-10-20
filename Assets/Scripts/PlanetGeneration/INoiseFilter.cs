using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGeneration
{
    public interface INoiseFilter
    {

        float Evaluate(Vector3 point);
    }
}