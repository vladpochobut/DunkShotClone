using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarHandler : MonoBehaviour
{
    private const string BallTag = "Ball";
    public static event Action onStarCollect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == BallTag)
        {
            onStarCollect?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
