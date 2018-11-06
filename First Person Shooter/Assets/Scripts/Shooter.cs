using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    public GameObject decalPrefab;

    GameObject[] totalDecals;
    int actual_decal = 0;

    public AudioSource fireSound;

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
                    out hit))
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

                fireSound.Play();
            }
        }
    }
}
