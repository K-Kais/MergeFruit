using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopWall : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cube") && MouseInput.Instance.cube == null)
        {
            CubeSpawner.Instance.SpawnRandom();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Gameplay.Instance.gameState != GameStates.Ready) return;
        if (collision.gameObject.CompareTag("Cube"))
        {
            PopupManager.Instance.ShowPopup<PopupGameOver>();
            Gameplay.Instance.gameState = GameStates.GameOver;
        }
    }
}