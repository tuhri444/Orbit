using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMovement : MonoBehaviour
{
    public static readonly float GRAVITATIONALCONSTANT = 0.00000000006672f;

    public float Mass;
    public Vector3 Velocity;
    public Vector3 Acceleration;
    public Vector3 NewPosition;

    private PhysicsManager _manager;

    [SerializeField] private Vector3 _startingVelocity;

    private void Awake()
    {
        _manager = FindObjectOfType<PhysicsManager>();
    }

    private void Start()
    {
        Velocity = _startingVelocity;   
    }

    private float CalculateGravitationalForce(float massOther, Vector3 positionOther)
    {
        return GRAVITATIONALCONSTANT * (Mass * massOther) / Mathf.Pow(Vector3.Distance(transform.position, positionOther),2);
    }

    private Vector3 CalculateAccelerationForce(float force, Vector3 positionOther)
    {
        return (positionOther - transform.position).normalized*(force / Mass);
    }

    private Vector3 CalculateVelocityForce()
    {
        return Velocity + Acceleration * _manager.TimeStep;
    }

    public Vector3 CalculateNewPosition(float massOther, Vector3 positionOther)
    {
        float gravity = CalculateGravitationalForce(massOther, positionOther);
        Acceleration = CalculateAccelerationForce(gravity, positionOther);
        Velocity = CalculateVelocityForce();
        NewPosition = transform.position + Velocity * _manager.TimeStep;

        return NewPosition;
    }
}
