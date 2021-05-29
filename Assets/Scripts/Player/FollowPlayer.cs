using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform objectToFollow;
    [SerializeField] float distanciaDesvio = 5, alturaExtra = 10;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 10f);

        Vector3 pos = transform.position;
        pos = objectToFollow.position + (mouseWorldPosition - objectToFollow.position) / distanciaDesvio;
        pos.y += alturaExtra;
        clampPos(ref pos);
        transform.position = pos;
    }

    void clampPos(ref Vector3 pos)
    {
        if (pos.x < -48) pos.x = -48;
        else if (pos.x > 48) pos.x = 48;
        if (pos.y < -48) pos.y = -48;
        else if (pos.y > 48) pos.y = 48;
    }
}
