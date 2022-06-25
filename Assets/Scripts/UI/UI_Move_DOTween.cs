using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UI_Move_DOTween : MonoBehaviour
{

    [SerializeField] private UI_Move_DOTween loadingObject;
    [SerializeField] private StartState startState;
    [SerializeField] private Ease ease;
    [SerializeField] private Vector2 moveDir;
    [SerializeField] private float delayTime;
    [SerializeField] private float duration;

    private Vector3 originPos;

    private enum StartState
    {

        activate_Showing,
        activate_Disquise,
        Disabled,

    }

    void Awake()
    {
        
        originPos = transform.position;

    }

    void Start()
    {

        startState = startState switch
        {

            StartState.activate_Showing => StartState.activate_Showing,

            StartState.activate_Disquise => StartState.activate_Disquise,

            StartState.Disabled => StartState.Disabled,

            _ => StartState.Disabled

        };

        if(startState == StartState.activate_Showing)
        {

            Showing();

        }
        else if(startState == StartState.activate_Disquise)
        {

            Disguise();

        }


    }

    public void Showing()
    {

        Sequence sequence = DOTween.Sequence()
        .OnStart(() => 
        {
            if(transform.position != originPos)
            {

                transform.position = originPos;

            }

        })
        .Append(transform.DOMove(new Vector2(960 + moveDir.x, 540 + moveDir.y), duration).SetEase(ease)
        .SetDelay(delayTime)
        .OnComplete(() => 
        {
            if(loadingObject != null)
            {

                loadingObject.Disguise();

            }

        }));
    }

    public void Disguise()
    {

        Sequence sequence = DOTween.Sequence()
        .Append(transform.DOMove(new Vector2(originPos.x, originPos.y), duration).SetEase(ease)
        .SetDelay(delayTime)
        .OnComplete(() =>
        {
            if(loadingObject != null)
            {

                if (transform.position != originPos)
                {

                    transform.position = originPos;

                }

                loadingObject.Showing();

            }

        }));

    }

}
