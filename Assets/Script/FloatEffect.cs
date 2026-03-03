using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Nhớ import DOTween

public class FloatEffect : MonoBehaviour
{
    public float amplitude = 10f;       // Biên độ dao động (độ cao trôi nổi)
    public float duration = 1f;         // Thời gian một chu kỳ đi lên/đi xuống
    public float phaseOffset = 0f;      // Độ trễ pha (nếu cần dùng cho nhiều text khác nhau)

    private Vector2 startPos;
    private RectTransform rect;

    void OnEnable()
{
    rect = GetComponent<RectTransform>();
    startPos = rect.anchoredPosition;

    rect.DOAnchorPosY(startPos.y + amplitude, duration)
        .SetEase(Ease.InOutSine)
        .SetLoops(-1, LoopType.Yoyo)
        .SetDelay(phaseOffset)
        .SetUpdate(true);
}

    void OnDisable()
    {
        // Hủy tween khi object bị disable để tránh lỗi hoặc memory leak
        transform.DOKill();
    }
}
