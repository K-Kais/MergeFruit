using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCollision : MonoBehaviour
{
    public FruitType fruitType;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fruit" && collision.transform.TryGetComponent<FruitCollision>(out FruitCollision fruitColision))
        {
            var thisInstanceID = gameObject.GetInstanceID();
            var collisionInstanceID = collision.gameObject.GetInstanceID();
            if (fruitColision.fruitType == fruitType && thisInstanceID > collisionInstanceID)
            {
                Gameplay.Instance.MergeFruit(transform.position, collision.transform.position, fruitType);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
public enum FruitType
{
    Apple = 1,
    Banana = 2,
    Orange = 3,
    Pear = 4,
    Watermelon = 5,
    Cherry = 6,
    Pineapple = 7,
    Strawberry = 8,
    Kiwi = 9,
    Grapes = 10
}