using UnityEngine;

public class Hook : MonoBehaviour
{
	public Transform itemHolder;

	private bool itemAttached;

	private int itemAttachedCount;

	private HookMovement hookMovement;

	public void Awake()
	{
		hookMovement = GetComponentInParent<HookMovement>();
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if ((collision.tag == "SmallGold" || collision.tag == "MediumGold" || collision.tag == "LargeGold" || collision.tag == "Stone") && itemAttachedCount == 0)
		{
			itemAttached = true;
			itemAttachedCount = 1;
			collision.transform.parent = itemHolder;
			collision.transform.position = itemHolder.position;
			hookMovement.moveSpeed = collision.GetComponent<Items>().hookSpeed;
			hookMovement.HookAttachedItem();
			if (collision.tag == "SmallGold" || collision.tag == "MediumGold" || collision.tag == "LargeGold")
			{
				AudioManager.instance.Play("Hook Grab Gold");
			}
			else if (collision.tag == "Stone")
			{
				AudioManager.instance.Play("Hook Grab Stone");
			}
			AudioManager.instance.Play("Pull Sound");
		}
		if (!(collision.tag == "DeliverItem") || !itemAttached)
		{
			return;
		}
		Transform child = itemHolder.GetChild(0);
		if (child.tag == "SmallGold" || child.tag == "MediumGold" || child.tag == "LargeGold")
		{
			GameManager.instance.countDownTimer += 5;
		}
		else if (child.tag == "Stone")
		{
			if (GameManager.instance.countDownTimer - 5 < 0)
			{
				GameManager.instance.countDownTimer = 0;
			}
			else
			{
				GameManager.instance.countDownTimer -= 5;
			}
		}
		child.parent = null;
		child.gameObject.SetActive(value: false);
		AudioManager.instance.Play("Collect Item");
		itemAttached = false;
		itemAttachedCount = 0;
		hookMovement.moveSpeed = hookMovement.initialMoveSpeed;
		AudioManager.instance.StopPlay("Pull Sound");
	}
}
