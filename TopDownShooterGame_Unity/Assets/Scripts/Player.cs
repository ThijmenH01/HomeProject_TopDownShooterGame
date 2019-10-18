using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerController))]
public class Player : MonoBehaviour {
    public float moveSpeed = 5;

    private Camera viewCamera;
    private PlayerController controller;

    private void Awake() {
        viewCamera = Camera.main;
        controller = GetComponent<PlayerController>();
    }

    void Start() {
        
    }

    void Update() {
        Vector3 moveInput = new Vector3( Input.GetAxisRaw( "Horizontal" ) , 0 , Input.GetAxisRaw( "Vertical" ) );
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move( moveVelocity );

        Ray ray = viewCamera.ScreenPointToRay( Input.mousePosition );
        Plane groundPlane = new Plane( Vector3.up , Vector3.zero );
        float rayDist;

        if(groundPlane.Raycast( ray , out rayDist )) {
            Vector3 point = ray.GetPoint( rayDist );
            Debug.DrawLine( ray.origin , point , Color.red );
            controller.LookAt( point );
        }
    }
}
