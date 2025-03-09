using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlash : MonoBehaviour
{

    //public LightOff lightOff; // Assign these in the inspector
    //public LightOn lightOn;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
