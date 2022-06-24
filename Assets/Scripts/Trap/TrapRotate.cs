using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRotate : MonoBehaviour
{

    [SerializeField] private float duration;
    [SerializeField] private float delayTime;
    [SerializeField] private float rotateAngles;
    [SerializeField] private Ease ease;
    [SerializeField] private LoopType loopType;

    private Sequence rotateSequence;

    //함정 돌아가는거(DOTween)
    void Start()
    {

        rotateSequence = DOTween.Sequence()
        .Append(transform.DORotate(new Vector3(0, 0, rotateAngles), duration).SetEase(ease))
        .AppendInterval(delayTime)
        .SetLoops(-1, loopType);

    }

}
