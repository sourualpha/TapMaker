using UnityEngine;
using UnityEngine.EventSystems;

public class ClickMove : MonoBehaviour, IPointerClickHandler
{
    private bool isClicked = false;
    private float speed = 5f;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("�N���b�N�C�x���g������");
        isClicked = true;
    }

    void Update()
    {
        if (isClicked)
        {
            // �E�����Ɉړ�����
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }
}
