using UnityEngine;

public class BGScroller : MonoBehaviour
{
	private Renderer rend;

	public float speed;

	private Vector2 offSet;

	private void Start()
	{
		rend = GetComponent<Renderer>();
	}

	private void Update()
	{
		BGScroll();
	}

	private void BGScroll()
	{
		offSet = new Vector2(speed * Time.time / 50f, 0f);
		rend.material.mainTextureOffset = offSet;
	}
}
