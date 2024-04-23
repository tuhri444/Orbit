using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    public float TimeStep => _timeStep; 

    [SerializeField] [Range(0.00001f, 10000f)]private float _timeStep;

    private List<OrbitalMovement> _planets;

    private void Start()
    {
        _planets = new List<OrbitalMovement>(FindObjectsOfType<OrbitalMovement>());
    }

    private void Update()
    {
        CalculateOrbits();
        ApplyOrbits();
    }

    private void CalculateOrbits()
    {
        foreach (OrbitalMovement planet in _planets)
        {
            foreach (OrbitalMovement otherPlanet in _planets)
            {
                if (planet == otherPlanet) continue;
                planet.CalculateNewPosition(otherPlanet.Mass, otherPlanet.transform.position);
            }
        }
    }

    private void ApplyOrbits()
    {
        foreach (OrbitalMovement planet in _planets)
        {
            planet.transform.position = planet.NewPosition;
        }
    }

}
