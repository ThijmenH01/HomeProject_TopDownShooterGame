using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent( typeof( NavMeshAgent ) )]
public class Enemy : MonoBehaviour {
    private NavMeshAgent pathFineder;
    private Transform target;

    private void Awake() {
        pathFineder = GetComponent<NavMeshAgent>();
    }

    void Start() {
        target = GameObject.FindGameObjectWithTag( "Player" ).transform;
        StartCoroutine( UpdatePath( .25f ) );
    }

    private IEnumerator UpdatePath(float refreshRate) {

        while(target != null) {
            Vector3 targetPosition = new Vector3( target.position.x, 0 , target.position.z );
            pathFineder.SetDestination( targetPosition );
            yield return new WaitForSeconds( refreshRate );
        }

    }
}
