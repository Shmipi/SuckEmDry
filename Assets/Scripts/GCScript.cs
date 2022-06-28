using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCScript : MonoBehaviour
{
    private CityScript cityScript;

    private float respawnVar;

    // Start is called before the first frame update
    void Start()
    {
        cityScript = GameObject.FindGameObjectWithTag("City").GetComponent<CityScript>();

        respawnVar = 1200f;
        Debug.Log("RespawnVar" + respawnVar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void respawnDepletion()
    {
        respawnVar -= 100f;
        cityScript.spawnUp();
        Debug.Log("RespawnVar" + respawnVar);
    }
}
