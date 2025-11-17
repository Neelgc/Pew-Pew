using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    int speed = 15;
    
    InputManager inputManager;

    Transform cameraDirection;
    float jumpStrength = 200f;

    Rigidbody _rb;
    public bool _isGrounded = false;

    public LayerMask layer;

    void Start()
    {
        cameraDirection = Camera.main.transform;

        inputManager = InputManager.Instance;

        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void UpdatePlayer()
    {
        Movement();

        if (inputManager.PlayerJumpThisFrame() && _isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpStrength);
            _isGrounded = false;
        }
    }

    void Movement()
    {
        float bonusSpeed = 1f;
        float targetFOV = 60f;
        if (inputManager.PlayerSprinting())
        {
            bonusSpeed = 1.7f;
            targetFOV = 40f;
        }


        cameraDirection.GetComponent<Camera>().fieldOfView = Mathf.Lerp(cameraDirection.GetComponent<Camera>().fieldOfView, targetFOV, Time.deltaTime * 5);

        Vector3 movement;
        Vector2 inputVector = inputManager.PlayerMovement();

        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

        movement = cameraDirection.forward * inputVector.y + cameraDirection.right * inputVector.x;

        movement = new Vector3(movement.x, 0, movement.z);

        movement.Normalize();

        movement = movement * speed * Time.deltaTime * bonusSpeed;


        //transform.position += movement;

        _rb.MovePosition(transform.position + movement);
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Environement") && _rb.linearVelocity.y <= 0f)
        {
            _isGrounded = true;
        }
    }
}
