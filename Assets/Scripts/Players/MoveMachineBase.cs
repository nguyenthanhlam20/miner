using UnityEngine;

public class MoveMachineBase : MonoBehaviour
{
    public bool canMove = true;

    public float maxHorz = 13.6f;

    public float minHorz = -13.6f;

    public float moveHorzSpeed = 5f;

    public float moveDirection;

    public bool moveRight;

    private void Update()
    {
        if (GameManager.instance.IsGameOver == false)
        {
            MoveHorizontal();
            GetInput();
        }
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0) && canMove) canMove = false;
    }

    private void MoveHorizontal()
    {
        if (canMove)
        {
            CalculateDirection();
            ChooseDirection();
        }
    }

    private void CalculateDirection()
    {
        if (moveRight)
        {
            moveDirection += moveHorzSpeed * Time.deltaTime;
        }
        else if (!moveRight)
        {
            moveDirection -= moveHorzSpeed * Time.deltaTime;
        }
        transform.position = new Vector3(moveDirection, transform.position.y, transform.position.z);
    }

    private void ChooseDirection()
    {
        if (transform.position.x >= maxHorz)
        {
            moveRight = false;
        }
        else if (transform.position.x <= minHorz)
        {
            moveRight = true;
        }
    }
}
