using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityScript : MonoBehaviour
{

    private float maxSpawnNr;
    private float spawnNr;

    [SerializeField] private GameObject soldier;
    [SerializeField] private GameObject tank;

    // Start is called before the first frame update
    void Start()
    {
        maxSpawnNr = 3001;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        spawnNr = Random.Range(1, maxSpawnNr);

        if(spawnNr > 804 && spawnNr < 820)
        {
            Debug.Log("Spawn Foot Soldier");
            GameObject soldierInstant = Instantiate(soldier, gameObject.transform.position, gameObject.transform.rotation);
        } else if(spawnNr > 311 && spawnNr < 315)
        {
            Debug.Log("Spawn Tank");
            GameObject tankInstant = Instantiate(tank, gameObject.transform.position, gameObject.transform.rotation);
        } else if(spawnNr > 1332 && spawnNr < 1334)
        {
            Debug.Log("Spawn Figher Jet");
        }
    }

    public void spawnUp()
    {
        maxSpawnNr += 100;
    }
}
