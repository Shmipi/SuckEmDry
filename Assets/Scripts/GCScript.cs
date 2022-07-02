using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GCScript : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip destruction;
    [SerializeField] private AudioClip playerDestruction;
    [SerializeField] private AudioClip soldierDeath;
    [SerializeField] private AudioClip levelUp;

    private GameObject[] cityObjects;

    private float respawnVar;
    private int cityRespawnVar;
    private bool canRespawn;

    private float startTime = 0f;
    private float holdTime = 0.1f;
    private float timer = 0f;

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

    [SerializeField] private GameObject winBanner;
    [SerializeField] private GameObject loseBanner;

    [SerializeField] private XPScript xPScript;
    [SerializeField] private GameObject levelUpBar;
    [SerializeField] private GameObject secretButton;

    private float maxXp = 100f;
    public float xp;

    private float lvl;

    public float damageMultiplier;
    public float harvestMultiplier;
    public float speedIncrease;

    [SerializeField] private GameObject explosion;
    [SerializeField] private GameObject bloodSplatter;

    // Start is called before the first frame update
    void Start()
    {
        startTime = 0f;
        holdTime = 0.1f;
        timer = 0f;

        levelUpBar.SetActive(false);
        xp = 0f;
        lvl = 0f;
        xPScript.SetMaxXp(maxXp, xp);

        damageMultiplier = 1f;
        harvestMultiplier = 1f;
        speedIncrease = 0f;

        lake = false;
        woods = false;
        mountain = false;
        oil = false;
        cities = 0;


        winBanner.SetActive(false);
        loseBanner.SetActive(false);

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
            tileScript.Generation();
        }

        respawnVar = 1200f;
        cityRespawnVar = 201;
        canRespawn = true;
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
            cityObjects = GameObject.FindGameObjectsWithTag("City");
            for (int i = 0; i < cityObjects.Length; i++)
            {
                cityObjects[i].GetComponent<CityScript>().NoSpawn();
            }
        }
        if(lvl < 5)
        {
            if (xp >= maxXp)
            {
                LevelUp();
            }
        }

        if(cities < 4 && canRespawn == true)
        {
            int citySpawnChance = Random.Range(0, cityRespawnVar);
            Debug.Log("Attempting to spawn " + citySpawnChance);
            if (citySpawnChance == 136)
            {
                Debug.Log("Spawn Time!");
                int tileToSpawn = Random.Range(0, worldTiles.Length + 1);
                worldTiles[tileToSpawn].GetComponent<TileScript>().Generation();
            }
            canRespawn = false;
            startTime = Time.time;
            timer = startTime;
        }

        if(canRespawn == false)
        {
            timer += Time.deltaTime;
            if (timer > (startTime + holdTime))
            {
                canRespawn = true;
            }
        }
    }

    public void RespawnDepletion()
    {
        respawnVar -= 100f;
        cityRespawnVar += 100;
        Debug.Log("RespawnVar" + respawnVar);
        cityObjects = GameObject.FindGameObjectsWithTag("City");

        for (int i = 0; i < cityObjects.Length; i++)
        {
            cityObjects[i].GetComponent<CityScript>().SpawnUp();
        }
    }

    public void CityCounter()
    {
        cities += 1;
    }

    public void CityDestruction()
    {
        cities -= 1;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        loseBanner.SetActive(true);
    }

    private void GameWon()
    {
        Debug.Log("You Win!");
        winBanner.SetActive(true);
    }

    public void IncreaseDamage()
    {
        damageMultiplier += 1;

        Debug.Log("Damage up!");
    }

    public void IncreaseHarvest()
    {
        harvestMultiplier += 1;

        Debug.Log("Harvest up!");
    }

    public void IncreaseSpeed()
    {
        speedIncrease += 2;

        Debug.Log("Speed up!");
    }

    public void ActivateSecret()
    {
        PlayerMovement playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerScript.secretActive = true;
    }

    public void IncreaseXp(float xpIncrease)
    {
        if(lvl < 5)
        {
            xp += xpIncrease;
            xPScript.SetXp(xp);
        } 
    }

    public void LevelUp()
    {
        lvl += 1;
        audioSource.PlayOneShot(levelUp);
        levelUpBar.SetActive(true);

        if(lvl < 5)
        {
            secretButton.SetActive(false);
            xp = 0;
            xPScript.SetXp(xp);
        } else
        {
            secretButton.SetActive(true);
            xPScript.MaxXpReached();
        }

        Debug.Log("Level up! " + lvl);
    }

    public void DestructionFx(int target, Transform targetTransform)
    {
        if(target == 1)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(destruction);
            GameObject explosionInstant = Instantiate(explosion, targetTransform.position, targetTransform.rotation);
        } else if (target == 2)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(playerDestruction);
            GameObject explosionInstant = Instantiate(explosion, targetTransform.position, targetTransform.rotation);
        } else
        {
            audioSource.pitch = Random.Range(0.8f, 1.0f);
            audioSource.PlayOneShot(soldierDeath);
            GameObject bloodInstant = Instantiate(bloodSplatter, targetTransform.position, targetTransform.rotation);
        }
        
    }
}
