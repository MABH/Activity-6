using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState {
    EnemyAI myEnemy;
    float actualTimeBetweenShoots = 0;

    //Cuando llamamos al constructor, guardamos
    //una referencia a la IA de nuestro enemigo
    public AttackState(EnemyAI enemy)
    {
        myEnemy = enemy;
    }
	
    //Aquí va toda la funcionalidad que queramos
    // que haga el enemigo cuando esté en este estado
    public void UpdateState()
    {
        myEnemy.myLight.color = Color.red;
        actualTimeBetweenShoots += Time.deltaTime;
    }

    //Si el player nos ha disparado no haremos nada
    public void Impact() { Debug.Log("Attack Impact"); myEnemy.Hit(10); }

    //Como ya estamos en el estado Attack, no
    //llamaremos nunca a estas funciones desde este estado
    public void GoToAttackState() { }
    public void GoToPatrolState() { }

    public void GoToAlertState()
    {
        myEnemy.currentState = myEnemy.alertState;
    }

    //El player ya está en nuestro trigger
    public void OnTriggerEnter(Collider col) { }

    //Orientaremos el enemigo mirando siempre al
    //player mientras le ataquemos
    public void OnTriggerStay(Collider col)
    {
        //Estaremos mirando siempre al player
        Vector3 lookDirection = col.transform.position - myEnemy.transform.position;

        //Rotando solamente en el eje Y
        myEnemy.transform.rotation =
            Quaternion.FromToRotation(Vector3.forward,
            new Vector3(lookDirection.x, 0, lookDirection.z));

        //Le toca volver a disparar
        if(actualTimeBetweenShoots > myEnemy.timeBetweenShoots)
        {
            actualTimeBetweenShoots = 0;
            col.gameObject.GetComponent<shooter>().Hit(myEnemy.damageForce);            
        }
    }

    //Si el player sale de su radio, pasa a modo Alert
    public void OnTriggerExit(Collider col)
    {
        GoToAlertState();
    }
}
