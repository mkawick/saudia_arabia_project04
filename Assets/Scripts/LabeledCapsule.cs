using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LabeledCapsule : MonoBehaviour
{
    [SerializeField]
    TMP_Text mTextOverHead;
    [SerializeField]
    Canvas mCanvas;
    Transform mTextOverTransform;
    void Awake()
    {
        mTextOverTransform = mTextOverHead.transform;
    }
    void LateUpdate()
    {
        Transform camPos = mCanvas.worldCamera.transform;
        mTextOverTransform.LookAt(camPos.forward - camPos.position);
        mTextOverTransform.rotation = camPos.rotation;
    }
}
