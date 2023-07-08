using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool IsMoving { get; set; }

    public void MovePlayer(float moveTime) 
    {
        IsMoving = true;
        Debug.Log("Start moving");
    }

    private void Update()
    {
        if (IsMoving) 
        {
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IsMoving = false;
        Debug.Log("Stop moving");
    }

}
