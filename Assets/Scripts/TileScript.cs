using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    private GCScript gc;

    private bool hasDeposit;
    private bool hasCity;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite img1;
    [SerializeField] private Sprite img2;
    [SerializeField] private Sprite img3;
    [SerializeField] private Sprite img4;
    [SerializeField] private Sprite img5;

    private int rndNr = 0;

    [SerializeField] private GameObject lake;
    [SerializeField] private GameObject woods;
    [SerializeField] private GameObject mountain;
    [SerializeField] private GameObject oil;
    [SerializeField] private GameObject city;

    // Start is called before the first frame update
    void Start()
    {
        hasDeposit = false;
        hasCity = false;

        rndNr = Random.Range(1, 6);

        if(rndNr == 1)
        {
            spriteRenderer.sprite = img1;
        } else if(rndNr == 2)
        {
            spriteRenderer.sprite = img2;
        } else if(rndNr == 3)
        {
            spriteRenderer.sprite = img3;
        } else if(rndNr == 4)
        {
            spriteRenderer.sprite = img4;
        } else if(rndNr == 5)
        {
            spriteRenderer.sprite = img5;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generation()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GCScript>();

        if(hasDeposit == false && hasCity == false)
        {
            if (gc.lake == false)
            {
                GameObject deposit = Instantiate(lake, gameObject.transform.position, gameObject.transform.rotation);
                gc.lake = true;
                hasDeposit = true;
            }
            else if (gc.woods == false)
            {
                GameObject deposit = Instantiate(woods, gameObject.transform.position, gameObject.transform.rotation);
                gc.woods = true;
                hasDeposit = true;
            }
            else if (gc.mountain == false)
            {
                GameObject deposit = Instantiate(mountain, gameObject.transform.position, gameObject.transform.rotation);
                gc.mountain = true;
                hasDeposit = true;
            }
            else if (gc.oil == false)
            {
                GameObject deposit = Instantiate(oil, gameObject.transform.position, gameObject.transform.rotation);
                gc.oil = true;
                hasDeposit = true;
            }
            else if (gc.cities < 4)
            {
                GameObject deposit = Instantiate(city, gameObject.transform.position, gameObject.transform.rotation);
                gc.CityCounter();
                Debug.Log("Spawned cities!" + gc.cities);
                hasCity = true;
            }
        }
    }
}
