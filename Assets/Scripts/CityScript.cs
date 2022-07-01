using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityScript : MonoBehaviour
{
    private float maxHealth = 1000f;
    private float health;
    public HealthBar healthBar;
    public GameObject healthBarRender;

    private float maxSpawnNr;
    private float spawnNr;

    [SerializeField] private GameObject soldier;
    [SerializeField] private GameObject tank;
    [SerializeField] private GameObject jet;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBarRender.SetActive(false);

        maxSpawnNr = 4001;
    }

    // Update is called once per frame
    void Update()
    {
       if(health <= 0)
        {
            Destroy(gameObject);
        } 
    }

    private void FixedUpdate()
    {
        spawnNr = Random.Range(1, maxSpawnNr);

        if(spawnNr > 804 && spawnNr < 820)
        {
            Debug.Log("Spawn Foot Soldier");
            GameObject soldierInstant = Instantiate(soldier, transform.position, transform.rotation);
        } else if(spawnNr > 311 && spawnNr < 315)
        {
            Debug.Log("Spawn Tank");
            GameObject tankInstant = Instantiate(tank, transform.position, transform.rotation);
        } else if(spawnNr > 1332 && spawnNr < 1334)
        {
            Debug.Log("Spawn Figher Jet");
            GameObject jetInstant = Instantiate(jet, transform.position, transform.rotation);
        }
    }

    public void spawnUp()
    {
        maxSpawnNr += 500;
    }

    public void noSpawn()
    {
        maxSpawnNr = 0;
    }

    public void TakeDamage()
    {
        health -= 10f;
        healthBar.SetHealth(health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            healthBarRender.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            healthBarRender.SetActive(false);
        }
    }
}
