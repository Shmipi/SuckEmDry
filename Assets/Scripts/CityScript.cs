using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityScript : MonoBehaviour
{

    private float maxSpawnNr;
    private float spawnNr;

    [SerializeField] private GameObject soldier;

    // Start is called before the first frame update
    void Start()
    {
        maxSpawnNr = 1001;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        spawnNr = Random.Range(1, maxSpawnNr);

        if(spawnNr > 804 && spawnNr < 815)
        {
            Debug.Log("Spawn Foot Soldier");
            GameObject soldierInstant = Instantiate(soldier, gameObject.transform.position, gameObject.transform.rotation);
        } else if(spawnNr > 211 && spawnNr < 216)
        {
            Debug.Log("Spawn Tank");
        } else if(spawnNr > 521 && spawnNr < 524)
        {
            Debug.Log("Spawn Figher Jet");
        }
    }

    public void spawnUp()
    {
        maxSpawnNr += 100;
    }
}
