using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // ドラッグ開始イベント
    public Action<PointerEventData> OnBeginDragEvent;

    // ドラッグ中イベント
    public Action<PointerEventData> OnDragEvent;

    // ドラッグ終了イベント
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

    
    /// 自身が所属してるキャンバス 
    private Canvas _belongedCanvas;


    /// スクリーン座標を自身が所属してるキャンバス上の座標に変換
    
    /// <param name="pointerPos">クリーン座標</param>
    /// <returns>自身が所属してるキャンバス上の座標</returns>
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
    /// 所属するCanvasを取得
    /// </summary>
    /// <param name="t">Transform</param>
    /// <returns>所属するCanvas</returns>
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