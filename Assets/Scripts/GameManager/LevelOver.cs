using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOver : MonoBehaviour
{
    Transform nexusPool;
    private void Start()
    {
        nexusPool = GameObject.Find("Nexus").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(nexusPool.childCount <= 0)
        {
            Application.Quit(0);
        }
    }
}
