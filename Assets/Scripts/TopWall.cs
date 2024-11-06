using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopWall : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            collision.GetComponent<FruitCollision>().fruitState = FruitState.Falling;
            FruitSpawner.Instance.SpawnRandom();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit") && collision.GetComponent<FruitCollision>().fruitState == FruitState.Collision)
        {
            Debug.Log("Game Over");
        }
    }
}