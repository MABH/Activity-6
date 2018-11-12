using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class shooter : MonoBehaviour {
    public GameObject decalPrefab;
    public GameObject crosshair, player,congratulations, camara;
    public Text mensaje;
    GameObject[] totalDecals;
    int actual_decal = 0;
    public AudioSource fireSound;
    public AudioSource fireSoundEnemy;
    public float playerLife = 100;
    public GameObject[] enemy;

    private void Start()
    {        
       totalDecals = new GameObject[10];        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(
                Camera.main.ViewportPointToRay(
                    new Vector3(0.5f, 0.5f, 0)),
                    out hit) )
            {
                if (hit.transform.tag != "Enemy")
                {
                    Destroy(totalDecals[actual_decal]);
                    totalDecals[actual_decal] =
                        GameObject.Instantiate(decalPrefab,
                            hit.point + hit.normal * 0.01f,
                            Quaternion.FromToRotation(
                                Vector3.forward,
                                -hit.normal))
                        as GameObject;

                    actual_decal++;
                    if (actual_decal == 10) actual_decal = 0;
                }
                    fireSound.Play();
                
                Debug.Log("Objeto golpeado: "+ hit.transform.name);
                
                switch (hit.transform.name)
                {
                    case "Enemy1":
                        enemy[0].transform.GetComponent<EnemyAI>().life-=10;

                            break;                        
                    case "Enemy2":
                        enemy[1].transform.GetComponent<EnemyAI>().life -= 10;

                        break; 
                    case "Enemy3":
                        enemy[2].transform.GetComponent<EnemyAI>().life -= 10;

                        break;
                    case "Enemy4":
                        enemy[3].transform.GetComponent<EnemyAI>().life -= 10;
                        break;
                    case "Enemy5":
                        enemy[4].transform.GetComponent<EnemyAI>().life -= 10;
                        break;
                    case "Enemy6":      
                        enemy[5].transform.GetComponent<EnemyAI>().life -= 10;
                        break;
                    case "Enemy7":
                        enemy[6].transform.GetComponent<EnemyAI>().life -= 10;
                        break;
                    case "Enemy8":
                        enemy[7].transform.GetComponent<EnemyAI>().life -= 7;
                        break;
                    case "Enemy9":
                        enemy[8].transform.GetComponent<EnemyAI>().life -= 7;
                        break;
                    case "Enemy10":
                        enemy[9].transform.GetComponent<EnemyAI>().life -= 7;
                        break;
                    case "Enemy11":
                        enemy[10].transform.GetComponent<EnemyAI>().life -= 7;
                        break;
                }

               
            }          
        }        
    }

    //Daño que hace el enemigo al player
    //Si la vida del player < 0 está muerto
    public void Hit(float damage)
    {
        playerLife -= damage;
        fireSoundEnemy.Play();
        //Debug.Log("Player hitted: " + playerLife);
        if (playerLife<=0)
        {
            crosshair.SetActive(false);
            camara.SetActive(true);
            mensaje.text = "You have died¡¡¡";
            congratulations.SetActive(true);
            player.SetActive(false);
            Debug.Log("Estas muerto");
        }
    }

}
