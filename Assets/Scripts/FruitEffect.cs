using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitEffect : MonoBehaviour
{
    [SerializeField] private float initialScaleTime = 0.1f;
    private void Start()
    {
        Scale();
    }

    public void Scale()
    {
        float bounceScale = transform.localScale.x;
        transform.localScale = Vector3.zero;
        transform.DOScale(bounceScale, initialScaleTime).SetEase(Ease.OutBack);
    }
}
