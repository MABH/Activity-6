using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AlertState alertState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public IEnemyState currentState;

    [HideInInspector] public NavMeshAgent navMeshAgent;

    public Light myLight;
    public float life = 100;
    public float timeBetweenShoots = 1.0f;
    public float damageForce = 10f;
    public float rotationTime = 3.0f;
    public float shootHeight = 0.5f;
    public Transform[] waypoints;

    // Use this for initialization
    void Start () {
        //Creamos los estados de nuestra IA
        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        attackState = new AttackState(this);

        //Le decimos que inicialmente empezará patrullando
        currentState = patrolState;

        //Guardamos la referencia en nuestro NavMesh Agent
        navMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        //Como nuestros estados no heredan de
        //Monobehaviour, no se llama a su uodate
        //automáticamente, y nos encargaremos
        //nosotros de llamarlo a acada frame.
        currentState.UpdateState();
        //Morir
        if (life < 0) Destroy(this.gameObject);
	}

    //Cuando el player nos dispara, nos quitamos vida
    //y avisamos al estado en el que estemos de que
    //nos han disparado
    public void Hit(float damage)
    {
        life -= damage;
        currentState.Impact();
        Debug.Log("Enemy life: " + life); 
    }

    //Ya que nuestros states no heredan de
    //Monobehaviour, tendremos que avisarles
    //cuando algo entra, está o sale de nuestro trigger.
    private void OnTriggerEnter(Collider col)
    {        
         currentState.OnTriggerEnter(col);
    }
    private void OnTriggerStay(Collider col)
    {
        currentState.OnTriggerStay(col);
    }
    private void OnTriggerExit(Collider col)
    {
        currentState.OnTriggerExit(col);
    }
}
