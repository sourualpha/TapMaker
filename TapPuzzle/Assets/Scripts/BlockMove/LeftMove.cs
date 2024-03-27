using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeftMove : MonoBehaviour, IPointerClickHandler
{
    public Rigidbody rb;
    public float moveForce = 10f;
    private bool isClicked = false;
    private bool isTouching = false; // 他のオブジェクトと接触しているかどうかを判定するフラグ
    GameManager gameManager;
    Vector3 sp;

    private void Start()
    {
        // Rigidbody2Dコンポーネントを取得します
        rb = GetComponent<Rigidbody>();
        sp = transform.localPosition;
        gameManager = FindObjectOfType<GameManager>();
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

    private void OnCollisionEnter(Collision collision)
    {
        transform.position = sp;
        rb.velocity = Vector3.zero;
        if (isClicked == true)
        {
            gameManager.DecreaseIQ(10);
        }
        isClicked = false;


    }

    void Move()
    {
        if (isClicked && !isTouching)
        {
            // 左方向に移動する
            rb.AddForce(Vector3.left * moveForce, ForceMode.Impulse);
        }
        if(transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}