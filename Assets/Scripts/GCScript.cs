using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GCScript : MonoBehaviour
{
    private CityScript cityScript;

    private float respawnVar;

    private GameObject[] worldTiles;
    private TileScript tileScript;

    public bool lake;
    public bool woods;
    public bool mountain;
    public bool oil;
    public int cities = 0;

    // Start is called before the first frame update
    void Start()
    {
        worldTiles = GameObject.FindGameObjectsWithTag("WorldTile");

        for(int i = 0; i < worldTiles.Length; i++)
        {
            GameObject obj = worldTiles[i];
            int randomizeArray = Random.Range(0, i);
            worldTiles[i] = worldTiles[randomizeArray];
            worldTiles[randomizeArray] = obj;
        }

        for(int i = 0; i < worldTiles.Length; i++)
        {
            tileScript = worldTiles[i].GetComponent<TileScript>();
            tileScript.generation();
        }

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

    public void cityCounter()
    {
        cities += 1;
    }
}
