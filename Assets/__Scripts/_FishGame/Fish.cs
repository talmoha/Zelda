using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    protected BoundsCheck bndCheck;
    private float paramater = 0; //paramater for the sin wave
  
    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>(); //set bound check
    }

    // Update is called once per frame
    void Update()
    {
        paramater += 0.01f;
        transform.position = transform.position + new Vector3(0.01f, 0.01f*Mathf.Sin(paramater), 0);

        if (bndCheck.offRight)
        {
            Destroy(gameObject);
        }
    }

}
