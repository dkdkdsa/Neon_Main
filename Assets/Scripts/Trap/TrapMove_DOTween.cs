using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrapMove_DOTween : MonoBehaviour
{

    [SerializeField] private float duration;
    [SerializeField] private float delayTime;
    [SerializeField] private Vector2 moveTarget;
    [SerializeField] private Ease ease;
    [SerializeField] private LoopType loopType;

    private Vector2 initPos;
    private Sequence sequence;

    //함정 움직임(DOTween)
    void Start()
    {
        initPos = transform.position;
        sequence = DOTween.Sequence()
        .Append(transform.DOMove(new Vector3(moveTarget.x + initPos.x, moveTarget.y + initPos.y), duration)).SetEase(ease).AppendInterval(delayTime)
        .Append(transform.DOMove(initPos, duration)).SetEase(ease).AppendInterval(delayTime).SetLoops(-1, loopType);

    }
}
