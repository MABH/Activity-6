using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicoptero : MonoBehaviour {
   public  GameObject crosshair, text, player;	

    private void OnTriggerEnter(Collider col)
    {
        string tag= col.gameObject.tag;
        Debug.Log("colision"+tag);
        if (col.gameObject.tag == "Player")
        {
            crosshair.SetActive(false);
            text.SetActive(true);
            player.SetActive(false);
        }
    }
}
