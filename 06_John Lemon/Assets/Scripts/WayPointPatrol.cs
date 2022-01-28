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

    private int currentWayPointIndex = 0;//Para controlar el siguiente waypoint al que deberá dirigirse
    
    // Start is called before the first frame update
    void Start()
    {
        //Capturamos el componente de navegación por la malla
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        //Enviamos hacia el primer waypoint
        navMeshAgent.SetDestination(wayPoints[currentWayPointIndex].position);
    }

    // Update is called once per frame
    void Update()
    {
        //Cuando haya llegado al destino (waypoint) actual
        //(se puede comprobar si la distancia restante es menor que la distancia de parado, que está en 0.2)
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            //Calculamos el próximo waypoint, debiendo volver al primero cuando alcance ya el último de la lista
            //Con esta fórmula aseguramos que el valor siempre estará entre 0 y Length-1, sin tener que usar "If"
            currentWayPointIndex = (currentWayPointIndex + 1) % wayPoints.Length;
            //Y enviamos al nuevo waypoint
            navMeshAgent.SetDestination(wayPoints[currentWayPointIndex].position);
        }
    }

}
