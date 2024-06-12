using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Items
{
    private void OnDisable()
    {
        GameManager.instance.StopGame();
    }
}
