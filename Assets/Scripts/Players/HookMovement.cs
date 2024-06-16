using UnityEngine;

public class HookMovement : MonoBehaviour
{
	public float minZ = -55f;

	public float maxZ = 55f;

	public float minY = -2.5f;

	public float rotateSpeed = 20f;

	public float moveSpeed = 3f;

	public float maxHorz = 13.6f;

	public float minHorz = -13.6f;

	public float moveHorzSpeed = 5f;

	public float rotateAngle;

	public float initialMoveSpeed;

	public float initialY;

	private bool rotateRight;

	private bool canRotate;

	private bool moveDown;

	private RopeRenderer ropeRenderer;

	private void Awake() => ropeRenderer = GetComponent<RopeRenderer>();

    private void Start()
	{
		initialY = transform.position.y;
		initialMoveSpeed = moveSpeed;
		canRotate = true;
	}

	private void Update()
	{
		Rotate();
		GetInput();
		MoveRope();
	}

	private void Rotate()
	{
		if (canRotate)
		{
			CalculateAngle();
			ChooseAngle();
		}
	}

	private void GetInput()
	{
		if (Input.GetMouseButtonDown(0) && canRotate)
		{
			canRotate = false;
			moveDown = true;
			AudioManager.instance.Play("Rope Stretch");
		}
	}

	private void MoveRope()
	{
		if (!canRotate && !canRotate)
		{
			Vector3 position = CalculateRopePosition();
			RenderLine(position);
        }
	}

	private void RenderLine(Vector3 position)
	{
        if (position.y <= minY)
        {
            moveDown = false;
        }
        if (position.y >= initialY)
        {
            canRotate = true;
            moveSpeed = initialMoveSpeed;
            gameObject.GetComponentInParent<MoveMachineBase>().canMove = true;
            ropeRenderer.RenderLine(position, enableRenderer: false);
            AudioManager.instance.StopPlay(AudioName.RopeStretch);
        }
        ropeRenderer.RenderLine(position, enableRenderer: true);
    }

	private Vector3 CalculateRopePosition()
    {
        Vector3 position = transform.position;
        if (moveDown)
        {
            position -= transform.up * Time.deltaTime * moveSpeed;
        }
        else
        {
            position += transform.up * Time.deltaTime * moveSpeed;
        }
        transform.position = position;
		return position;
    }

	public void HookAttachedItem() => moveDown = false;

    private void CalculateAngle()
	{
        if (rotateRight)
        {
            rotateAngle += rotateSpeed * Time.deltaTime;
        }
        else
        {
            rotateAngle -= rotateSpeed * Time.deltaTime;
        }
        transform.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.forward);
    }

	private void ChooseAngle()
	{
        if (rotateAngle >= maxZ)
        {
            rotateRight = false;
        }
        else if (rotateAngle <= minZ)
        {
            rotateRight = true;
        }
    }
}
