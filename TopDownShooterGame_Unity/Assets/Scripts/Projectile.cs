﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public LayerMask collisionMask;

    private float speed = 10;
    private float damage = 1;

    public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }

    void Update() {
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions( moveDistance );
        transform.Translate( Vector3.forward * moveDistance );
    }

    private void CheckCollisions(float moveDistance) {
        Ray ray = new Ray( transform.position , transform.forward );
        RaycastHit hit;

        if(Physics.Raycast( ray , out hit , moveDistance , collisionMask , QueryTriggerInteraction.Collide )) {
            OnHitObject( hit );
        }
    }

    private void OnHitObject(RaycastHit hit) {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
        if(damageableObject != null) {
            damageableObject.TakeHit( damage , hit );
        }
        Destroy( gameObject ); 
    }
}
