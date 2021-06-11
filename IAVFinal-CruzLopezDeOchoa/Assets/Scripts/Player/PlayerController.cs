using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game related
/// </summary>
public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    [SerializeField] private float playerSpeed = 2.0f;

    Vector2 move;
    Vector3 endResult;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        if (Mathf.Abs(move.x) <= 0.1f) move.x = 0;
        if (Mathf.Abs(move.y) <= 0.1f) move.y = 0;
        move.Normalize();
        endResult.x = move.x;
        endResult.y = 0;
        endResult.z = move.y;
        controller.Move(endResult * Time.deltaTime * playerSpeed);
    }
}
