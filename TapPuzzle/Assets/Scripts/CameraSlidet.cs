using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.InputSystem;
using Common; 

public class CameraSlidet : MonoBehaviour
{
    // カメラの回転中心
    public Transform rotationCenter;
    // カメラの回転速度
    public float rotationSpeed = GrovalConst.DefaultRotationSpeed;
    // カメラの垂直回転範囲
    public float verticalRotationRange = GrovalConst.DefaultVerticalRotationRange;
    // カメラと回転中心の距離
    public float cameraDistance = GrovalConst.DefaultCameraDistance;

    // カメラが回転中かどうか
    private bool isRotating = false;

    // 水平方向の回転角度
    private float horizontalRotation = 0f;
    // 垂直方向の回転角度
    private float verticalRotation = 0f;

    // TitleSceneのインスタンス
    private TitleScene _title;


    [SerializeField]
    private Camera _cam;

    [SerializeField]
    private GameObject _hitPrefab; //クリックした時のエフェクト

    private void Start()
    {
        // TitleSceneのインスタンスを取得
        _title = FindObjectOfType<TitleScene>();
    }


    private void Update()
    {
        // カメラの操作処理
        PlayCameraControler();
    }

    // カメラの操作処理
    void PlayCameraControler()
    {
        // マウスの位置を取得し、Z座標を設定
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = GrovalConst.DefaultMousePosZ;
        // マウスの位置をワールド座標に変換
        Vector3 worldPos = _cam.ScreenToWorldPoint(mousePos);

        if (Input.GetMouseButtonDown(0))
        {
            // プレイモードの場合、ヒットエフェクトを生成
            if (_title.isplayMode == true)
            {
                _hitPrefab.SetActive(true);
                Instantiate(_hitPrefab, worldPos, Quaternion.identity);
            }

            // カメラの回転を開始
            isRotating = true;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            // カメラの回転を停止
            isRotating = false;
        }

        // カメラが回転中の場合
        if (isRotating)
        {
            // マウスの水平方向の入力を取得
            float horizontalInput = Input.GetAxis("Mouse X");
            // マウスの垂直方向の入力を取得
            float verticalInput = Input.GetAxis("Mouse Y");

            // 水平方向の回転角度を更新
            horizontalRotation += horizontalInput * rotationSpeed;
            // 垂直方向の回転角度を更新
            verticalRotation -= verticalInput * rotationSpeed;
            // 垂直方向の回転角度を制限
            verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationRange, verticalRotationRange);

            // 回転の計算
            Quaternion rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
            // カメラの位置を計算し更新
            Vector3 offset = rotation * new Vector3(0f, 0f, -cameraDistance);
            transform.position = rotationCenter.position + offset;
            // カメラを回転中心に向ける
            transform.LookAt(rotationCenter);
        }
    }
}
