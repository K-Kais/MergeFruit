using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public static FruitSpawner Instance;
    [SerializeField] private GameObject[] fruitPrefabs;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        SpawnRandom();
    }

    public void SpawnRandom()
    {
        int randomIndex = Random.Range(0, 5);
        GameObject fruit = Instantiate(fruitPrefabs[randomIndex], fruitPrefabs[randomIndex].transform.position, Quaternion.identity);
        MouseInput.Instance.fruit = fruit.transform;
    }
    public void Spawn(FruitType fruitType, Vector2 position)
    {
        GameObject fruit = Instantiate(fruitPrefabs[(int)fruitType], position, Quaternion.identity);
        fruit.GetComponent<FruitCollision>().fruitState = FruitState.Collision;
        fruit.GetComponent<CircleCollider2D>().isTrigger = false;
        var rb = fruit.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        rb.AddForce(Vector2.up * (int)fruitType, ForceMode2D.Impulse);

    }
}
