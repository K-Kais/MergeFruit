using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public static MouseInput Instance;
    public Transform fruit;
    [SerializeField] private float litmitX;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (fruit == null) return;
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            fruit.position = new Vector2(Mathf.Clamp(mousePosition.x, -litmitX, litmitX), 4f);
        }
        if (Input.GetMouseButtonUp(0))
        {
            fruit.GetComponent<Rigidbody2D>().gravityScale = 1f;
            fruit.GetComponent<CircleCollider2D>().isTrigger = false;
            fruit = null;
        }
    }
}
