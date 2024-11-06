using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public static MouseInput Instance;
    public Transform cube;
    [SerializeField] private float litmitX;
    private void Awake()
    {
        Instance = this;
    }
    private void Update()
    {
        if (cube == null) return;
        if (Input.GetMouseButton(0))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            cube.position = new Vector2(Mathf.Clamp(mousePosition.x, -litmitX, litmitX), 4f);
        }
        if (Input.GetMouseButtonUp(0))
        {
            cube.GetComponent<Rigidbody2D>().gravityScale = 1f;
            cube.GetComponent<CircleCollider2D>().isTrigger = false;
            cube = null;
        }
    }
}
