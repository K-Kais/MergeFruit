using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner Instance;
    [SerializeField] private GameObject[] cubePrefabs;
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
        GameObject cube = Instantiate(cubePrefabs[randomIndex], cubePrefabs[randomIndex].transform.position, Quaternion.identity);
        MouseInput.Instance.cube = cube.transform;
    }
    public void Spawn(CubeType cubeType, Vector2 position)
    {
        GameObject cube = Instantiate(cubePrefabs[(int)cubeType], position, Quaternion.identity);
        cube.GetComponent<CircleCollider2D>().isTrigger = false;
        var rb = cube.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        rb.AddForce(Vector2.up * (int)cubeType, ForceMode2D.Impulse);
    }
}
