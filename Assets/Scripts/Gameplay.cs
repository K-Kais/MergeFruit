using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
   public static Gameplay Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void MergeCube(Vector2 currentPos, Vector2 targetPos, CubeType cubeType)
    {
        Vector2 middlePos = (currentPos + targetPos) / 2;
        CubeSpawner.Instance.Spawn(cubeType, middlePos);
    }
}
