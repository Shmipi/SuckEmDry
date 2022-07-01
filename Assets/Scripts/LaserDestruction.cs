using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDestruction : MonoBehaviour
{
    private SoldierScript soldierScript;
    private TankScript tankScript;
    private JetScript jetScript;
    private CityScript cityScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Soldier")
        {
            soldierScript = collision.gameObject.GetComponent<SoldierScript>();
            soldierScript.TakeDamage();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Tank")
        {
            tankScript = collision.gameObject.GetComponent<TankScript>();
            tankScript.TakeDamage();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Jet")
        {
            jetScript = collision.gameObject.GetComponent<JetScript>();
            jetScript.TakeDamage();
            Destroy(gameObject);
        } else if(collision.gameObject.tag == "City")
        {
            cityScript = collision.gameObject.GetComponent<CityScript>();
            cityScript.TakeDamage();
            Destroy(gameObject);
        }
    }
}
