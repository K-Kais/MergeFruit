using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeCollision : MonoBehaviour
{
    public CubeType cubeType;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.TryGetComponent<CubeCollision>(out CubeCollision cubeColision))
        {
            var thisInstanceID = gameObject.GetInstanceID();
            var collisionInstanceID = collision.gameObject.GetInstanceID();
            if (cubeColision.cubeType == cubeType && thisInstanceID > collisionInstanceID)
            {
                Gameplay.Instance.MergeCube(transform.position, collision.transform.position, cubeType);
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
public enum CubeType
{
    Cube1 = 1,
    Cube2 = 2,
    Cube3 = 3,
    Cube4 = 4,
    Cube5 = 5,
    Cube6 = 6,
    Cube7 = 7,
    Cube8 = 8,
    Cube9 = 9,
    Cube10 = 10,
    Cube11 = 11,
    Cube12 = 12
}