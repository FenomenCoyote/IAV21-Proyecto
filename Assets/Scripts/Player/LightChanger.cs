using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChanger : MonoBehaviour
{
    [SerializeField] int minimumPoint = 200;
    [SerializeField] float frecuency = 0.5f;
    [SerializeField] Color initialColor = Color.green;

    float mediumPoint;
    float sinChange;
    Light light_;

    PlayerLife pLife;

    void Start()
    {
        light_ = GetComponent<Light>();
        pLife = GetComponentInParent<PlayerLife>();
        sinChange = (255 - minimumPoint) / 2;
        mediumPoint = (minimumPoint + sinChange) / 255f;
        sinChange /= 255;
    }

    // Update is called once per frame
    void Update()
    {
        float adjustLife = 10 / pLife.life;
        frecuency = 0.5f * adjustLife;
        float delta = 1 - mediumPoint + sinChange * Mathf.Sin((2 * Mathf.PI * frecuency) * Time.time);
        delta *= adjustLife;

        if (initialColor == Color.green)
        {
            light_.color = initialColor + new Color(delta, -delta, 0.0f);
        }
        else if (initialColor == Color.blue)
        {
            light_.color = initialColor + new Color(delta, 0.0f, -delta);
        }
    }
}
