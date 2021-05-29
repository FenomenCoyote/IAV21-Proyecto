using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusColor : MonoBehaviour
{
    EnemyLife life;
    Renderer renderer_;

    [SerializeField] Color color;
    float maxLife, previousLife = 0;

    // Start is called before the first frame update
    void Start()
    {
        life = GetComponent<EnemyLife>();
        renderer_ = GetComponent<Renderer>();
        maxLife = life.getLife();
    }

    // Update is called once per frame
    void Update()
    {
        if(previousLife != life.getLife()) {
            previousLife = life.getLife();
            color.r = 1 - (previousLife / maxLife);
            renderer_.material.SetColor(Shader.PropertyToID("_Color"), color);
        }
    }
}
