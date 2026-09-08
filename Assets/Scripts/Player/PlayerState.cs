using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [Header("플레이어 이동 속도")]
    [Tooltip("플레이어의 이동 속도를 입력하세요")]
    [SerializeField] private float _moveSpeed;
    public float MoveSpeed => _moveSpeed;

    [Header("플레이어 점프력")]
    [Tooltip("플레이어의 점프력을 입력하세요")]
    [SerializeField] private float _jumpForce;
    public float JumpForce => _jumpForce;

    [Header("플레이어 비행 속도")]
    [Tooltip("플레이어의 비행 속도를 입력하세요")]
    [SerializeField] private float _flySpeed;
    public float FlySpeed => _flySpeed;

    [Header("플레이어 비행 높이")]
    [Tooltip("플레이어의 비행 높이를 입력하세요")]
    [SerializeField] private float _flyHeight;
    public float FlyHeight => _flyHeight;
}
