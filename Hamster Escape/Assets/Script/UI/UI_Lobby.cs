using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UI_Lobby : MonoBehaviour
{
    public RectTransform lstChain;

    private void OnEnable()
    {
        lstChain.anchoredPosition = new Vector2(938f, 76f);
        lstChain.DOAnchorPos(new Vector2(0f, 76f), 0.5f).SetEase(Ease.OutQuart).SetDelay(0.5f);
    }

}
