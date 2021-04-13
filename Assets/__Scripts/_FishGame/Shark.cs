using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{

    protected BoundsCheck bndCheck;
    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>(); //set bound check
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(-0.05f, 0, 0);

        if (bndCheck.offLeft)
        {
            Destroy(gameObject);
        }
    }
}
