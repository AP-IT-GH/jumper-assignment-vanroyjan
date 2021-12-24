using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMover : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        int rng = Random.Range(2, 5);
        float rngSpeed = rng / 5f;
        speed = rngSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed);
        if (transform.position.x < -5.5)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Cube")
        {
            Destroy(gameObject);
        }
    }
}
