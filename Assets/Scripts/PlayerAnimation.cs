using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	private Animator anim;

	private void Awake()
	{
		anim = GetComponent<Animator>();
	}

	public void IdleAnimation()
	{
		anim.Play("Idle");
	}

	public void WrapAnimation()
	{
		anim.Play("Wrap");
	}

	public void CheerAnimation()
	{
		anim.Play("Cheer");
	}
}
