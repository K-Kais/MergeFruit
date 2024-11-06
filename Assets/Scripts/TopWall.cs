using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopWall : MonoBehaviour
{
    [SerializeField] private float countDown = 1.5f;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            collision.GetComponent<FruitCollision>().fruitState = FruitState.Falling;
            FruitSpawner.Instance.SpawnRandom();
            countDown = 1.5f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit") && collision.GetComponent<FruitCollision>().fruitState == FruitState.Collision)
        {
            countDown -= Time.deltaTime;
            if (countDown <= 0)
            {
                countDown = 1.5f;
                PopupManager.Instance.ShowPopup<PopupGameOver>();
            }
        }
    }
}