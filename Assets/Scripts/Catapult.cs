using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Catapult : MonoBehaviour
{
    private const float AngleReset = -30;
    private const float AngleMax = 90f;

    [SerializeField] private HingeJoint _joint;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Projectile _projectile;
    
    private Rigidbody _rigidbodyProjectile;
    
    private UserInput _userInput;

    private void Awake()
    {
        _userInput = new UserInput();
        
        _rigidbodyProjectile = _projectile.GetComponent<Rigidbody>() ?? throw new Exception("No Rigidbody on projectile");
    }

    private void OnEnable()
    {
        _userInput.Enable();
        
        _userInput.Input.Launch.performed += Launch;
        _userInput.Input.Reloader.performed += Reloader;
    }

    private void Start()
    {
        Reloader(default);
    }

    private void OnDisable()
    {
        _userInput.Disable();
        
        _userInput.Input.Launch.performed -= Launch;
        _userInput.Input.Reloader.performed -= Reloader;
    }

    private void Launch(InputAction.CallbackContext context)
    {
        float force = 1000;
        
        JointSpring spring = _joint.spring;
        spring.targetPosition = AngleMax;
        spring.spring = force;
        _joint.spring = spring;
    }
    
    private void Reloader(InputAction.CallbackContext context)
    {
        float force = 10;
     
        _projectile.transform.position = _spawnPoint.position;
        _rigidbodyProjectile.linearVelocity = Vector3.zero;
        
        JointSpring spring = _joint.spring;
        spring.targetPosition = AngleReset;
        spring.spring = force;
        _joint.spring = spring;
    }
    
    private void ResetProjectile()
    {
    }
}
