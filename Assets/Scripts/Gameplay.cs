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
    public void MergeFruit(Vector2 currentPos, Vector2 targetPos, FruitType fruitType)
    {
        Vector2 middlePos = (currentPos + targetPos) / 2;
        FruitSpawner.Instance.Spawn(fruitType, middlePos);
    }
}
