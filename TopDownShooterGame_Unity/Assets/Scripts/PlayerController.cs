﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof( Rigidbody ) )]
public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    private Vector3 velocity;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        rb.MovePosition( rb.position + velocity * Time.fixedDeltaTime );
    }

    public void LookAt(Vector3 lookPoint) {
        Vector3 heightCorrectedPoint = new Vector3( lookPoint.x , transform.position.y , lookPoint.z );
        transform.LookAt( heightCorrectedPoint );
    }

    public void Move(Vector3 _velocity) {
        velocity = _velocity;
    }
}
