using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Pusher : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    
    private UserInput _userInput;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _userInput = new UserInput();
    }

    private void Start()
    {
        _rigidbody = _transform.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _userInput.Enable();
        _userInput.Input.Push.performed += Push;
    }

    private void OnDisable()
    {
        _userInput.Disable();
        _userInput.Input.Push.performed -= Push;
    }

    private void Push(InputAction.CallbackContext obj)
    {
        Debug.Log("Push");
        _rigidbody.AddForce(Vector3.left * 100);
    }
}
