using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public Transform itemHolder;

    private bool itemAttached;

    private int itemAttachedCount;

    private HookMovement hookMovement;

    public static Hook Instance;

    public float boostSpeed { get; set; } = 0f;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
        hookMovement = GetComponentInParent<HookMovement>();

    }

    private List<string> itemTags = new() { "Gold", "Stone" };

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (itemTags.Contains(collision.tag) && itemAttachedCount == 0) AttachHookToItem(collision);
        if (!collision.CompareTag("DeliverItem") || !itemAttached) return;
        DisableItem();
        RewardAdditionalTime(collision);
        ResetHook();
    }

    private void AttachHookToItem(Collider2D collision)
    {
        itemAttached = true;
        itemAttachedCount = 1;
        collision.transform.parent = itemHolder;
        collision.transform.position = itemHolder.position;
        hookMovement.moveSpeed = collision.GetComponent<Items>().hookSpeed + boostSpeed;
        hookMovement.HookAttachedItem();

        AudioManager.instance.Play(collision.CompareTag("Stone") ? AudioName.HookGrabStone : AudioName.HookGrabGold);
        AudioManager.instance.Play(AudioName.PullSound);
    }

    private void RewardAdditionalTime(Collider2D collision)
    {
        if (!collision.CompareTag("Stone")) GameManager.instance.CountDownTimer += 2;
    }

    private void DisableItem()
    {
        Transform child = itemHolder.GetChild(0);
        GameManager.instance.DisplayScore(child.GetComponent<Items>().scoreValue);
        Destroy(child.gameObject);
    }

    public void ExplodeItem()
    {
        if (itemHolder != null && itemHolder.childCount > 0)
        {
            var child = itemHolder?.GetChild(0);
            Destroy(child?.gameObject);
            ResetHook();
        }
    }

    private void ResetHook()
    {
        AudioManager.instance.Play(AudioName.CollectItem);
        itemAttached = false;
        itemAttachedCount = 0;
        hookMovement.moveSpeed = hookMovement.initialMoveSpeed;
        AudioManager.instance.Play(AudioName.PullSound);
    }
}
