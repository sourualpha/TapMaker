using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    #region �ϐ�
    /// <summary> 
    /// �ړ�������󂯕t����^�b�`�G���A 
    /// </summary>
    [SerializeField]
    private DragHandler _moveController;

    
    /// <summary> 
    /// �ړ����x�im/�b�j 
    /// </summary>
    [SerializeField]
    private float _movePerSecond = 7f;

    /// <summary> 
    /// �ړ�����̃^�b�`�ʒu�|�C���^ 
    /// </summary>
    [SerializeField]
    private Sprite _leftPointer;

    [SerializeField]
    private GameObject _camera;

    /// <summary> �J��������̃^�b�`�ʒu�|�C���^ </summary>
    [SerializeField]
    private Sprite _rightPointer;
    /// <summary> 
    /// �J����������󂯕t����^�b�`�G���A 
    /// </summary>
    [SerializeField]
    private DragHandler _lookController;

    /// <summary> 
    /// �J�������x�i��/px�j 
    /// </summary>
    [SerializeField]
    private float _angularPerPixel = 1f;

    /// <summary>
    /// �W���C�X�e�B�b�N
    /// </summary>
    [SerializeField]
    private VariableJoystick _moveControllers;


    /// <summary> �J��������Ƃ��đO�t���[���Ƀ^�b�`�����L�����o�X��̍��W </summary>
    private Vector2 _lookPointerPosPre;
    /// <summary> 
    /// �ړ�����Ƃ��ă^�b�`�J�n�����X�N���[�����W 
    /// </summary>
    private Vector2 _movePointerPosBegin;

    private Vector3 _moveVector;
    #endregion
    /// <summary> 
    /// �N���� 
    /// </summary>
    private void Awake()
    {
        _moveController.OnBeginDragEvent += OnBeginDragMove;
        _moveController.OnDragEvent += OnDragMove;
        _moveController.OnEndDragEvent += OnEndDragMove;
        _lookController.OnBeginDragEvent += OnBeginDragLook;
        _lookController.OnDragEvent += OnDragLook;
    }

    /// <summary> �X�V���� </summary>
    private void Update()
    {
        UpdateMove(_moveVector);
    }


    // �ړ�����
    #region Move

    /// <summary> �h���b�O����J�n�i�ړ��p�j </summary>
    private void OnBeginDragMove(PointerEventData eventData)
    {
        // �^�b�`�J�n�ʒu��ێ�
        _movePointerPosBegin = eventData.position;
    }

    /// <summary> �h���b�O���쒆�i�ړ��p�j </summary>
    private void OnDragMove(PointerEventData eventData)
    {
        // �^�b�`�J�n�ʒu����̃X���C�v�ʂ��ړ��x�N�g���ɂ���
        var vector = eventData.position - _movePointerPosBegin;
        _moveVector = new Vector3(vector.x, 0f, vector.y);

    }

    private void UpdateMove(Vector3 vector)
    {
        // ���݌�������ɁA���͂��ꂽ�x�N�g���Ɍ������Ĉړ�
        transform.position += transform.rotation * vector.normalized * _movePerSecond * Time.deltaTime;
    }

    /// <summary> �h���b�O����I���i�ړ��p�j </summary>
    private void OnEndDragMove(PointerEventData eventData)
    {
        // �ړ��x�N�g��������
        _moveVector = Vector3.zero;
    }
    #endregion


    // �J��������
    #region Look
    /// <summary> �h���b�O����J�n�i�J�����p�j </summary>
    private void OnBeginDragLook(PointerEventData eventData)
    {
        _lookPointerPosPre = _lookController.GetPositionOnCanvas(eventData.position);
    }

    /// <summary> �h���b�O���쒆�i�J�����p�j </summary>
    private void OnDragLook(PointerEventData eventData)
    {
        var pointerPosOnCanvas = _lookController.GetPositionOnCanvas(eventData.position);
        // �L�����o�X��őO�t���[�����牽px���삵�������v�Z
        var vector = pointerPosOnCanvas - _lookPointerPosPre;
        // ����ʂɉ����ăJ��������]
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