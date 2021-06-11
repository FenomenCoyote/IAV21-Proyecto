using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Game related
/// When all nexuses are destroyed, it changes scene
/// </summary>
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
            SceneManager.LoadScene("End");
        }
    }
}
