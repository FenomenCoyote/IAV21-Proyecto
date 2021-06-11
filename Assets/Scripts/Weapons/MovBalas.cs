using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game related
/// </summary>
public class MovBalas : MonoBehaviour {

    float speed;

    Rigidbody rb;

	// Use this for initialization
	void Start ()
    {
        if (GetComponent<Rigidbody>() != null) rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }

    public void ChangeSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
