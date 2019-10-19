using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent( typeof( NavMeshAgent ) )]
public class Enemy : LivingEntity {

    public enum State {
        Idle = 0,
        Chasing,
        Attacking
    }
    public State currentState;

    private NavMeshAgent pathFinder;
    private Transform target;
    private float attackDistanceThreshold = 0.5f;
    private float timeBetweenAttacks = 1f;
    private float nextAttacktime;
    private float myCollisionRadius;
    private float targetCollisionRadius;
    private Material skinMaterial;
    private Color originalColor;

    protected override void Start() {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();
        skinMaterial = GetComponent<Renderer>().material;
        originalColor = skinMaterial.color;
        currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag( "Player" ).transform;
        myCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
        StartCoroutine( UpdatePath( .25f ) );
    }

    private void Update() {
        if(Time.time > nextAttacktime) {
            float sqrDistToTarget = (target.position - transform.position).sqrMagnitude;
            if(sqrDistToTarget < Mathf.Pow( attackDistanceThreshold + myCollisionRadius + targetCollisionRadius, 2 )) {
                nextAttacktime = Time.time + timeBetweenAttacks;
                StartCoroutine( Attack( 3 ) );
            }
        }
    }

    private IEnumerator Attack(float attackSpeed) {
        currentState = State.Attacking;
        pathFinder.enabled = false;

        Vector3 originalPos = transform.position;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        Vector3 attackPos = target.position - dirToTarget * (myCollisionRadius);

        float percent = 0f;

        skinMaterial.color = Color.red;

        while(percent <= 1f) {
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow( percent , 2 ) + percent) * 4;
            transform.position = Vector3.Lerp( originalPos , attackPos , interpolation );

            yield return null;
        }
        skinMaterial.color = originalColor;
        currentState = State.Chasing;
        pathFinder.enabled = true;
    }

    private IEnumerator UpdatePath(float refreshRate) {
        while(target != null) {
            if(currentState == State.Chasing) {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (myCollisionRadius + targetCollisionRadius + attackDistanceThreshold / 2);
                if(!isDead) {
                    pathFinder.SetDestination( targetPosition );
                }
            }
            yield return new WaitForSeconds( refreshRate );
        }
    }
}
