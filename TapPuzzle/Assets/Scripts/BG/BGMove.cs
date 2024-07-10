using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMove : MonoBehaviour
{
    private const float MAX_OFFSET = 1f;
    private const string PROPERTY_NAME = "_MainTex";

    [SerializeField] private Vector2 _offsetSpeed;
    [SerializeField] private Material _material;

    private void Reset()
    {
        // �R���|�[�l���g���A�^�b�`���ꂽ�^�C�~���O�Ń}�e���A�����擾����
        if (TryGetComponent(out Image image))
        {
            _material = image.material;
        }
    }

    private void Update()
    {
        if (_material != null)
        {
            // ���ԂɊ�Â��ăe�N�X�`���̃I�t�Z�b�g���v�Z
            var x = Mathf.Repeat(Time.time * _offsetSpeed.x, MAX_OFFSET);
            var y = Mathf.Repeat(Time.time * _offsetSpeed.y, MAX_OFFSET);
            var offset = new Vector2(x, y);
            // �e�N�X�`���̃I�t�Z�b�g���V�F�[�_�[�ɐݒ�
            _material.SetTextureOffset(PROPERTY_NAME, offset);
        }
    }

    private void OnDestroy()
    {
        // �I�u�W�F�N�g���j�������^�C�~���O�Ɉʒu�����Z�b�g����
        if (_material != null)
        {
            _material.SetTextureOffset(PROPERTY_NAME, Vector2.zero);
        }
    }
}
