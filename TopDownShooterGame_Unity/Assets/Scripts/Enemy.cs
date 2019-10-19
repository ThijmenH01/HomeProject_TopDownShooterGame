using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent( typeof( NavMeshAgent ) )]
public class Enemy : LivingEntity {
    private NavMeshAgent pathFineder;
    private Transform target;

    protected override void Start() {
        base.Start();
        pathFineder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag( "Player" ).transform;
        StartCoroutine( UpdatePath( .25f ) );
    }

    private IEnumerator UpdatePath(float refreshRate) {

        while(target != null) {
            Vector3 targetPosition = new Vector3( target.position.x , 0 , target.position.z );
            if(!isDead) {
                pathFineder.SetDestination( targetPosition );
            }
            yield return new WaitForSeconds( refreshRate );
        }
    }
}
