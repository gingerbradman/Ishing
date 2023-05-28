using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{

    bool atRest = false;

    // User Inputs
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;

    Rigidbody2D rb;

    // Position Storage Variables
    Vector3 posOffset = new Vector2 ();
    Vector3 tempPos = new Vector2 ();

    // Use this for initialization
    void Start () {
    // Store the starting position & rotation of the object

        rb = this.gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update () {
        if(atRest)
        {
            // Float up/down with a Sin()
            tempPos = posOffset;
            tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
        }
    }

    public void AtRest()
    {
        rb.velocity = Vector2.zero;
        transform.rotation = Quaternion.identity;
        posOffset = transform.position;
        atRest = true;
    }
}
