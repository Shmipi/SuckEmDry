using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite img1;
    [SerializeField] private Sprite img2;
    [SerializeField] private Sprite img3;
    [SerializeField] private Sprite img4;
    [SerializeField] private Sprite img5;

    private int rndNr = 0;

    // Start is called before the first frame update
    void Start()
    {
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
}