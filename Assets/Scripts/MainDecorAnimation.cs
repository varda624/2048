using UnityEngine;
using DG.Tweening;

public class MainDecorAnimation : MonoBehaviour
{
    public RectTransform PanPivot;
    public RectTransform Text;

    public float PanAngle;
    public float JumpHeight;
    public float Duration;

    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(PanPivot.DORotate(new Vector3(0, 0, PanAngle), Duration));
        seq.Join(Text.DOAnchorPosY(Text.anchoredPosition.y + JumpHeight, Duration));
        seq.Join(Text.DORotate(new Vector3(0, 0, 360), Duration, RotateMode.FastBeyond360));
        seq.Append(PanPivot.DORotate(new Vector3(0, 0,  -PanAngle), Duration));
        seq.Join(Text.DOAnchorPosY(Text.anchoredPosition.y - JumpHeight + 250, Duration));
        seq.SetLoops(-1);
    }
}
