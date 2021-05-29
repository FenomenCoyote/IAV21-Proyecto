using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {

    public void Destroy(float t)
    {
        Destroy(this.gameObject, t);
    }
}