using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class WayPointPatrol : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    //Los diversos puntos de destino los guardaremos en un array o en una lista
    public Transform[] wayPoints;
    
    // Start is called before the first frame update
    void Start()
    {
        //Capturamos el componente de navegación por la malla
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        //Enviamos hacia el primer waypoint
        navMeshAgent.SetDestination(wayPoints[0].position);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
