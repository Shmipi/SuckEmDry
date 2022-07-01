using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] Text lakeText;
    [SerializeField] Text woodText;
    [SerializeField] Text mountainText;
    [SerializeField] Text oilText;

    private GameObject lakeObject;
    private GameObject woodObject;
    private GameObject mountainObject;
    private GameObject oilObject;

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

        lakeText.enabled = false;
        woodText.enabled = false;
        mountainText.enabled = false;
        oilText.enabled = false;

        lakeObject = GameObject.FindGameObjectWithTag("Lake");
        woodObject = GameObject.FindGameObjectWithTag("Woods");
        mountainObject = GameObject.FindGameObjectWithTag("Mountain");
        oilObject = GameObject.FindGameObjectWithTag("Oil");
    }

    // Update is called once per frame
    void Update()
    {
        if(lakeObject.GetComponent<ResourceScript>().empty == true)
        {
            lakeText.enabled = true;
        }

        if(woodObject.GetComponent<ResourceScript>().empty == true)
        {
            woodText.enabled = true;
        }

        if(mountainObject.GetComponent<ResourceScript>().empty == true)
        {
            mountainText.enabled = true;
        }

        if(oilObject.GetComponent<ResourceScript>().empty == true)
        {
            oilText.enabled = true;
        }

        if(lakeObject.GetComponent<ResourceScript>().empty == true && woodObject.GetComponent<ResourceScript>().empty == true && mountainObject.GetComponent<ResourceScript>().empty == true && oilObject.GetComponent<ResourceScript>().empty == true)
        {
            GameWon();
        }

        if(respawnVar <= 0)
        {
            cityScript.noSpawn();
        }
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

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    private void GameWon()
    {
        Debug.Log("You Win!");
    }
}
