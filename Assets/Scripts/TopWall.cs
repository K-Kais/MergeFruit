using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopWall : MonoBehaviour
{
    [SerializeField] private float countDown = 1.5f;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cube"))
        {
            collision.GetComponent<CubeCollision>().cubeState = CubeState.Falling;
            CubeSpawner.Instance.SpawnRandom();
            countDown = 1.5f;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cube") && collision.GetComponent<CubeCollision>().cubeState == CubeState.Collision)
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