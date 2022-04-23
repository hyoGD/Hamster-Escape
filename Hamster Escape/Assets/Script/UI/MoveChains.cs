using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MoveChains : MonoBehaviour
{
    public Transform   endRectranform;
   // public RectTransform ChainParent;
    private Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
       // ChainParent.DOScaleX(0, 0.1f);
       // ChainParent.DOScaleX(1, 1f).SetEase(Ease.OutQuad);
    }

    private void OnEnable()
    {
        gameObject.GetComponent<RectTransform>().DOAnchorPos(endRectranform.position, 1f).SetEase(Ease.OutQuart);
      //  ChainParent.DOScaleX(1, 1f).SetEase(Ease.OutQuad);
    }

    private void OnDisable()
    {
        transform.DOMove(startPos, 1f);
      //  ChainParent.DOScaleX(0, 0.1f);
    }
}
