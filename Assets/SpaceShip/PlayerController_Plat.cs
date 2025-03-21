using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerController_Plat : MonoBehaviour
{
    private float _horizontal;
    [SerializeField] private Rigidbody2D myRBD;
   // private float _vertical;
    [SerializeField] private float velocityModifier;

    [SerializeField] private float jumpForce;

    [SerializeField] private int maxJumps = 2;  // Máximo número de saltos permitidos
    private int jumpCount = 0;
    //private bool isGrounded = false;

   // private GameObject _ghostPrefab;
    public static event Action<int> OnCollisionItem;



    void Start()
    {

    }

    public void OnMovement(InputAction.CallbackContext move)
    {
        _horizontal = move.ReadValue<Vector2>().x;
       // _vertical = move.ReadValue<Vector2>().y;
    }
    public void OnJump(InputAction.CallbackContext jump)
    {
        if (jump.performed && jumpCount < maxJumps)
        {
             myRBD.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCount += 1;

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Comprobar si el objeto con el que se colisiona está en la capa del suelo
        if (collision.gameObject.tag== "Ground")
        {
           // Debug.Log("ground");
            jumpCount = 0;

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Coin")
        {
            OnCollisionItem?.Invoke(5);
            Destroy(collision);
        }
    }
    void Update()
    {
       // Debug.Log(jumpCount);
    }
   
    public void FixedUpdate()
    {
        myRBD.velocity = new Vector2(_horizontal * velocityModifier, myRBD.velocity.y);
    }
    public void SendPosition()
    {

    }
   
}
