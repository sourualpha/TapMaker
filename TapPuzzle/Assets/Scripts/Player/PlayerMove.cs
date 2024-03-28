using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    #region 変数

    // 移動操作を受け付けるタッチエリア 
    
    [SerializeField]
    private DragHandler _moveController;
    
    // 移動速度（m/秒） 
    
    [SerializeField]
    private float _movePerSecond = 7f;

    // 移動操作のタッチ位置ポインタ 
    
    [SerializeField]
    private Sprite _leftPointer;

    [SerializeField]
    private GameObject _camera;

    //カメラ操作のタッチ位置ポインタ
    [SerializeField]
    private Sprite _rightPointer;
    
    // カメラ操作を受け付けるタッチエリア 
    
    [SerializeField]
    private DragHandler _lookController;

    
    // カメラ速度（°/px） 
    
    [SerializeField]
    private float _angularPerPixel = 1f;


    //ジョイスティック
    
    [SerializeField]
    private VariableJoystick _moveControllers;


    [SerializeField]
    private GameManager _gameManager;
    //カメラ操作として前フレームにタッチしたキャンバス上の座標
    private Vector2 _lookPointerPosPre;
    
    /// 移動操作としてタッチ開始したスクリーン座標 
    
    private Vector2 _movePointerPosBegin;

    private Vector3 _moveVector;

    #endregion
    
    // 起動時 
    
    private void Awake()
    {
        _moveController.OnBeginDragEvent += OnBeginDragMove;
        _moveController.OnDragEvent += OnDragMove;
        _moveController.OnEndDragEvent += OnEndDragMove;
        _lookController.OnBeginDragEvent += OnBeginDragLook;
        _lookController.OnDragEvent += OnDragLook;
    }

    //更新処理 
    private void Update()
    {
        if(_gameManager.isOption == false)
        {
            UpdateMove(_moveVector);
            PCMove();
        }

    }


    // 移動操作
    #region Move

    //ドラッグ操作開始（移動用） 
    private void OnBeginDragMove(PointerEventData eventData)
    {
        // タッチ開始位置を保持
        _movePointerPosBegin = eventData.position;
    }

    //ドラッグ操作中（移動用） 
    private void OnDragMove(PointerEventData eventData)
    {
        // タッチ開始位置からのスワイプ量を移動ベクトルにする
        var vector = eventData.position - _movePointerPosBegin;
        _moveVector = new Vector3(vector.x, 0f, vector.y);

    }

    private void UpdateMove(Vector3 vector)
    {
        // 現在向きを基準に、入力されたベクトルに向かって移動
        transform.position += transform.rotation * vector.normalized * _movePerSecond * Time.deltaTime;
    }

    //ドラッグ操作終了（移動用） 
    private void OnEndDragMove(PointerEventData eventData)
    {
        // 移動ベクトルを解消
        _moveVector = Vector3.zero;
    }

    //WASD、矢印キーでの移動
    private void PCMove()
    {
        // カメラの向きに合わせた移動ベクトルを計算する
        Vector3 moveDirection = Vector3.zero;
        if (_camera != null)
        {
            // カメラの向きからx軸とz軸の成分を取得して移動ベクトルとする
            Vector3 cameraForward = _camera.transform.forward;
            // y軸成分を0にすることで水平な方向のみを考慮する
            cameraForward.y = 0; 
            Vector3 cameraRight = _camera.transform.right;
            

            Vector3 cameraUp = _camera.transform.up;
            cameraUp.z = 0;
            cameraUp.x = 0;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                moveDirection += cameraForward;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                moveDirection -= cameraForward;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                moveDirection -= cameraRight;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                moveDirection += cameraRight;
            }

            if(Input.GetKey(KeyCode.Space))
            {
                moveDirection += cameraUp;
            }
            else if(Input.GetKey(KeyCode.LeftShift))
            {
                moveDirection -= cameraUp;
            }

            // 移動ベクトルの大きさを1に正規化する
            if (moveDirection.magnitude > 0)
            {
                moveDirection.Normalize();
            }

            // プレイヤーキャラクターを移動させる
            this.transform.position += moveDirection * _movePerSecond * Time.deltaTime;
        }
    }

    #endregion


    // カメラ操作
    #region Look
    //ドラッグ操作開始（カメラ用） 
    private void OnBeginDragLook(PointerEventData eventData)
    {
        _lookPointerPosPre = _lookController.GetPositionOnCanvas(eventData.position);
    }

    //ドラッグ操作中（カメラ用） 
    private void OnDragLook(PointerEventData eventData)
    {
        var pointerPosOnCanvas = _lookController.GetPositionOnCanvas(eventData.position);
        // キャンバス上で前フレームから何px操作したかを計算
        var vector = pointerPosOnCanvas - _lookPointerPosPre;
        // 操作量に応じてカメラを回転
        LookRotate(new Vector2(-vector.y, vector.x));
        _lookPointerPosPre = pointerPosOnCanvas;
    }

    private void LookRotate(Vector2 angles)
    {
        Vector2 deltaAngles = angles * _angularPerPixel;
        transform.eulerAngles += new Vector3(0f, deltaAngles.y);
        _camera.transform.localEulerAngles += new Vector3(deltaAngles.x, 0f);
    }
    #endregion
}