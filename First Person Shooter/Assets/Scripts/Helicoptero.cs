using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicoptero : MonoBehaviour {
   public  GameObject crosshair, text, player, camara;	

    private void OnTriggerEnter(Collider col)
    {
        string tag= col.gameObject.tag;
        Debug.Log("colision"+tag);
        if (col.gameObject.tag == "Player")
        {
            camara.SetActive(true);
            crosshair.SetActive(false);
            text.SetActive(true);
            player.SetActive(false);
        }
    }
}
