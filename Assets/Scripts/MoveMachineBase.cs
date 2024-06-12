using UnityEngine;

public class MoveMachineBase : MonoBehaviour
{
    public float maxHorz = 13.6f;

    public float minHorz = -13.6f;

    public float moveHorzSpeed = 5f;

    public float moveDirection;

    public bool canMove;

    public bool moveRight;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove = true;
    }

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
       
            if (Input.GetMouseButtonDown(0) && canMove)
            {
                canMove = false;
            }
       
    }

    private void MoveHorizontal()
    {
        if (canMove)
        {
            if (moveRight)
            {
                moveDirection += moveHorzSpeed * Time.deltaTime;
            }
            else if (!moveRight)
            {
                moveDirection -= moveHorzSpeed * Time.deltaTime;
            }
            base.transform.position = new Vector3(moveDirection, base.transform.position.y, base.transform.position.z);
            if (base.transform.position.x >= maxHorz)
            {
                moveRight = false;
            }
            else if (base.transform.position.x <= minHorz)
            {
                moveRight = true;
            }
        }
    }
}
