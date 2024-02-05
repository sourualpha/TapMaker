using UnityEngine;
using UnityEngine.EventSystems;

public class ClickMove : MonoBehaviour, IPointerClickHandler
{
    private bool isClicked = false;
    private float speed = 5f;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("クリックイベントが発生");
        isClicked = true;
    }

    void Update()
    {
        if (isClicked)
        {
            // 右方向に移動する
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
