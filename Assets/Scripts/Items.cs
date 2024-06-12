using UnityEngine;

public class Items : MonoBehaviour
{
	public float hookSpeed;

	public int scoreValue;

	private void OnDisable()
	{
		GameManager.instance.DisplayScore(scoreValue);
	}
}
