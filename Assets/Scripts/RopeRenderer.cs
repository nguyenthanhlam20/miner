using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
	private LineRenderer lineRenderer;

	public Transform startPos;

	private float lineWidth = 0.1f;

	private void Start()
	{
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.startWidth = lineWidth;
		lineRenderer.endWidth = lineWidth;
		lineRenderer.enabled = false;
	}

	private void Update()
	{
	}

	public void RenderLine(Vector3 endPos, bool enableRenderer)
	{
		if (enableRenderer)
		{
			if (!lineRenderer.enabled)
			{
				lineRenderer.enabled = true;
			}
			lineRenderer.positionCount = 2;
		}
		else if (lineRenderer.enabled)
		{
			lineRenderer.enabled = false;
		}
		if (lineRenderer.enabled)
		{
			Vector3 position = startPos.position;
			position.z = -10f;
			startPos.position = position;
			lineRenderer.SetPosition(0, startPos.position);
			lineRenderer.SetPosition(1, endPos);
		}
	}
}
