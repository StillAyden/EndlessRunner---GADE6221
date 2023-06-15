using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MovementSystem : MonoBehaviour
{
    GameInputs _inputs;
    Rigidbody rb;
    
    [Header("Movement")]
    [SerializeField] float moveForce = 10f;

    [Header("Jump")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool grounded = false;
    [SerializeField] float overlapRadius = 1.05f;

    float moveInput; 

    private void OnDisable()
    {
        _inputs.Player.Move.performed -= OnMove;
        _inputs.Player.Jump.performed -= x => MovePlayer();
        //_inputs.Player.SlideForceDown.performed -= x => MovePlayer();
        //_inputs.Player.Restart.performed -= x => Restart();

        _inputs.Disable();
    }
    private void OnEnable()
    {
        _inputs.Player.Enable(); 

        _inputs.Player.Move.performed += OnMove;
        _inputs.Player.Jump.performed += x => MovePlayer();
        //_inputs.Player.SlideForceDown.performed += x => MovePlayer();
        // _inputs.Player.Restart.performed += x => Restart();
    }

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        _inputs = new GameInputs();        
    }
    private void OnMove(InputAction.CallbackContext info)
    {
        //Input x = A & D Keys (But saved in the x value of our Vector 2)
        //Save relevent/needed  information reived from input
        moveInput = info.ReadValue<float>();
    }







    void MovePlayer()
    {

        
        if (_inputs.Player.Jump.triggered && grounded) 
        {
            rb.AddForce(moveForce * Vector3.up, ForceMode.Impulse);
        }
        else if (_inputs.Player.SlideForceDown.triggered)  
        {
            rb.AddForce(moveForce * Vector3.down, ForceMode.Impulse);

        }
    }

    void Restart()
    {
        //By pressing the key "r" you can restart the game from any time frame
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnMove(InputAction.CallbackContext info)
    {
        //Input x = A & D Keys (But saved in the x value of our Vector 2)
        //Save relevent/needed  information reived from input
        moveInput = info.ReadValue<float>();
    }

    private void FixedUpdate() //Constant, No jarring or delays
    {
        //Make Player move here when keyboard input is received
        //Adding velocity to the Rigidbody
        rb.velocity = new Vector3(moveInput * moveSpeed * Time.fixedDeltaTime, rb.velocity.y, rb.velocity.z);
    }

    private void Update()
    {
        grounded = Physics.CheckSphere(transform.position, overlapRadius, groundLayer);
    }
}
