using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopWall : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (MouseInput.Instance.cube == null && collision.transform.TryGetComponent<CubeCollision>(out CubeCollision cubeColision))
        {
            CubeSpawner.Instance.SpawnRandom();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Gameplay.Instance.gameState != GameStates.Ready) return;
        if (collision.transform.TryGetComponent<CubeCollision>(out CubeCollision cubeColision))
        {
            PopupManager.Instance.ShowPopup<PopupGameOver>();
            Gameplay.Instance.gameState = GameStates.GameOver;
        }
    }
}