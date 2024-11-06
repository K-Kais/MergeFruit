using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerInProgress : MonoBehaviour
{
    [SerializeField] private float countDown = 0.5f;
    private void Update()
    {
        if (Gameplay.Instance.gameState == GameStates.InProgress)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0)
            {
                Gameplay.Instance.gameState = GameStates.Ready;
                countDown = 0.5f;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cube")
        {
            Gameplay.Instance.gameState = GameStates.InProgress;
            countDown = 0.5f;
        }
    }
}
