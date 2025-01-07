using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MovementController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private float _maxSpeed = 3;

    [SerializeField] private float _jumpForce = 5;
    [SerializeField] private Transform _feet;
    [SerializeField] private bool _isGrounded = false;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 movDir = new Vector3(-Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Debug.Log(Input.GetAxis("Horizontal"));
        
        Quaternion cucc =  Quaternion.FromToRotation(movDir, this.transform.eulerAngles);
        movDir = cucc * movDir;
        Debug.Log(movDir);

        Vector3 movDir2 = new Vector3(0,0,0);
        if (Input.GetAxis("Horizontal") > 0) 
        {
            movDir2 += this.transform.right; 
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            movDir2 += -this.transform.right;
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            movDir2 += this.transform.forward;
        }
        if (Input.GetAxis("Vertical") < 0)
        {
            movDir2 += -this.transform.forward;
        }
        Vector3 force = movDir2.normalized * _movementSpeed;

        Vector3 horizontalVelocity = _rigidbody.velocity;
        horizontalVelocity.y = 0;
        if(horizontalVelocity.magnitude > _maxSpeed)
        {
            float mul = _maxSpeed / horizontalVelocity.magnitude;

            horizontalVelocity *= mul;
        }
        horizontalVelocity.y = _rigidbody.velocity.y;
        _rigidbody.velocity = horizontalVelocity;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if(_isGrounded)
            {
                force += new Vector3(0, _jumpForce, 0);
                _isGrounded = false;
            }
        }
        //Debug.Log(horizontalVelocity.y);

        

        Debug.Log(_rigidbody.velocity);
        _rigidbody.AddForce(force);
    }

    public void SetGrounded(bool value)
    {
        _isGrounded = value;
    }
}
