using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Action<PointerEventData> OnBeginDragEvent;

    public Action<PointerEventData> OnDragEvent;

    public Action<PointerEventData> OnEndDragEvent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent?.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        OnDragEvent?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragEvent?.Invoke(eventData);
    }

    /// <summary> 
    /// ���g���������Ă�L�����o�X 
    /// </summary>
    private Canvas _belongedCanvas;

    /// <summary>
    /// �X�N���[�����W�����g���������Ă�L�����o�X��̍��W�ɕϊ�
    /// </summary>
    /// <param name="pointerPos">�N���[�����W</param>
    /// <returns>���g���������Ă�L�����o�X��̍��W</returns>
    public Vector2 GetPositionOnCanvas(Vector2 pointerPos)
    {
        if (_belongedCanvas == null)
        {
            _belongedCanvas = GetBelongedCanvas(transform);
        }
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_belongedCanvas.transform as RectTransform, pointerPos, _belongedCanvas.worldCamera, out Vector2 localPointerPos);
        return localPointerPos;
    }

    /// <summary> 
    /// ��������Canvas���擾 
    /// </summary>
    private Canvas GetBelongedCanvas(Transform t)
    {
        if (t == null)
        {
            return null;
        }

        var canvas = t.GetComponent<Canvas>();
        if (canvas != null)
        {
            return canvas;
        }

        return GetBelongedCanvas(t.parent);
    }
}