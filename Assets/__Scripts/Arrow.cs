using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed;//speed of arrow
    public Rigidbody2D myRigidbody;//rigidbody
    public float lifetime;//fields for lifetime of arrows
    private float lifetimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        lifetimeCounter = lifetime;//set counter to lifetime
    }

    private void Update() //destroy arrow after its lifetime is up
    {
        lifetimeCounter -= Time.deltaTime;
        if(lifetimeCounter <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void Setup(Vector2 velocity, Vector3 direction)//method taking in speed and direction we want arrow to face
    {
        myRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }

    public void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy"))//if enemy then destroy the object the arrow hits
        {
            Destroy(this.gameObject);
        }
    }
}
