using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    EnemyAI myEnemy;
    private int nextWayPoint = 0;

    //Cuando llamamos al constructor, guardamos
    //una referencia a la IA de nuestro enemigo
    public PatrolState(EnemyAI enemy)
    {
        myEnemy = enemy;
    }

    //Aqui va toda la funcionalidad que queramos
    //que haga el enemigo cuando esté en este estado.
    public void UpdateState()
    {
        //Debug.Log("PatrolState");
        myEnemy.myLight.color = Color.green;
        //Le decimos al NavMeshAgent cuál es el punto
        //al que ha de dirigirse.
        myEnemy.navMeshAgent.destination =
            myEnemy.waypoints[nextWayPoint].position;

        //Debug.Log("myEnemy remainingDistance"+ myEnemy.navMeshAgent.remainingDistance);
        //Debug.Log("myEnemy remainingDistance" + myEnemy.navMeshAgent.stoppingDistance);
        //Si hemos llegado al destino, cambiamos la
        //referencia al siguiente Waypoint
        if (myEnemy.navMeshAgent.remainingDistance <=
            myEnemy.navMeshAgent.stoppingDistance)
        {
            nextWayPoint = (nextWayPoint + 1) % myEnemy.waypoints.Length;
        }
    }

    //Si el player nos ha disparado
    public void Impact()
    {
        Debug.Log("Patrol Impact");
        //myEnemy.Hit(10);    
        myEnemy.life-=10;    
        GoToAttackState();
    }

    public void GoToAlertState()
    {
        //Paramos su movimiento
        myEnemy.navMeshAgent.isStopped = true;
        myEnemy.currentState = myEnemy.alertState;
    }

    public void GoToAttackState()
    {
        //Paramos su movimiento
        myEnemy.navMeshAgent.isStopped = true;
        myEnemy.currentState = myEnemy.attackState;
    }

    //Como ya estamos en el estado Patrol, no
    //llamaremos nunca a esta función desde este estado
    public void GoToPatrolState() { }

    public void OnTriggerEnter(Collider col)
    {
        //Debug.Log("OnTriggerEnter" + col.gameObject.tag);
        if (col.gameObject.tag == "Player")
            GoToAlertState();
    }
    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
            GoToAlertState();
    }
    public void OnTriggerExit(Collider col) { }
}
