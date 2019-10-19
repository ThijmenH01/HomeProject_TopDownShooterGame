using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (PlayerController))]
[RequireComponent(typeof (GunController))]
public class Player : LivingEntity {
    public float moveSpeed = 5;

    private Camera viewCamera;
    private PlayerController controller;
    private GunController gunController;

    protected override void Start() {
        base.Start();
        viewCamera = Camera.main;
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
    }

    void Update() {
        //Movement
        Vector3 moveInput = new Vector3( Input.GetAxisRaw( "Horizontal" ) , 0 , Input.GetAxisRaw( "Vertical" ) );
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move( moveVelocity );

        //Look
        Ray ray = viewCamera.ScreenPointToRay( Input.mousePosition );
        Plane groundPlane = new Plane( Vector3.up , Vector3.zero );
        float rayDist;

        if(groundPlane.Raycast( ray , out rayDist )) {
            Vector3 point = ray.GetPoint( rayDist );
            Debug.DrawLine( ray.origin , point , Color.red );
            controller.LookAt( point );
        }

        //Weapon
        if(Input.GetMouseButton( 0 )) {
            gunController.Shoot();
        }
    }
}
