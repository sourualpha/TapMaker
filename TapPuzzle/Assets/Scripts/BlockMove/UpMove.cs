using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Common;

public class UpMove : MonoBehaviour, IPointerClickHandler
{
    public Rigidbody rb;
    public float moveForce = Common.GrovalConst.MoveForce;
    private bool isClicked = false;
    GameManager gameManager;
    Vector3 sp;//初期位置

    private void Start()
    {
        // Rigidbody2Dコンポーネントを取得します
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        sp = transform.localPosition;
    }
    private void Update()
    {
        Move();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("クリックイベントが発生");
        isClicked = true;
    }

    //他のブロックに当たった際にHPを減らし初期位置に戻す
    private void OnCollisionEnter(Collision collision)
    {
        transform.position = sp;
        rb.velocity = Vector3.zero; 
        if(isClicked == true)
        {
            gameManager.DecreaseIQ(Common.GrovalConst.DecreaseIQAmount);
        }
        isClicked = false;


    }

    void Move()
    {
        if (isClicked)
        {
            // 上方向に移動する
            rb.AddForce(Vector3.up * moveForce, ForceMode.Impulse);
        }
        if(transform.position.y > Common.GrovalConst.ResetPosition)
        {
            Destroy(gameObject);

        }
    }
}