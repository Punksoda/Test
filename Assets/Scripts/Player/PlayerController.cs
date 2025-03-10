using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; // 움직이는 속도
    private Vector2 curMoveInput; // 현재 입력받은 방향
    public float jumpForce; // 점프하는 힘
    public LayerMask groundCheck; // 공중에서 점프를 방지하기 위한 그라운드 레이어 감지

    public Transform cameraContainer; // 부착된 카메라 컨테이너에 따라 transform 값 할당
    public float minLook; // 최소시야
    public float maxLook; // 최대시야 
    private float camCurLook; // 현재 보는 값
    public float lookSensertivity; // 시야회전 속도

    private Vector2 mouseDelta; // 마우스 방향 값

    private Rigidbody rigid; // 리지드 바디 

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서를 중앙에 고정하고, 움직이지 못하게함
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    public void OnLookInput(InputAction.CallbackContext context) // InputSystem에 마우스 이동처리 입력
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context) // InputSystem에 움직이는 입력값 받기
    {
        if(context.phase == InputActionPhase.Performed)
        {
            curMoveInput = context.ReadValue<Vector2>();
        }
        else if(context.phase == InputActionPhase.Canceled)
        {
            curMoveInput = Vector2.zero;
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context) // InputSystem에 점프하는 입력값 받기
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void Move() // 움직이는 방향 값을 지정하고, 움직이는 속도를 그만큼 지정, 점프의 수직 속도를 지정, 그리고 Rigidbody에 이동을 적용시킴
    {
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;
        dir *= moveSpeed;
        dir.y = rigid.velocity.y;

        rigid.velocity = dir;
    }

    private void CameraLook() // 마우스의 y축 값을 기준으로 카메라의 상하 회전 각을 조절한다, 카메라를 가지고 있는 카메라 컨트롤러의 회전 값을 지정한다
    {
        camCurLook += mouseDelta.y * lookSensertivity;
        camCurLook = Mathf.Clamp(camCurLook, minLook, maxLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurLook, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensertivity, 0);
        // 플레이어 본체의 y축(좌, 우) 회전을 마우스 x축 이동으로 조절하여 방향을 전환한다
        // FPS 기준 이동방향을 조절할때, 사용하기 적합하다
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4] // 앞, 뒤, 오른쪽, 왼쪽에서 레이를 발사하여, 바닥을 체크한다
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 1.6f, groundCheck)) // 트랜스폼 위치에 따른 레이값 변경
            {
                Debug.Log("그라운드 쳌");
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos() // 아래 방향의 땅 체크 위한 디버그
    {
        Debug.DrawRay(transform.position + (transform.forward * 1.0f), Vector3.down * 2.0f, Color.magenta);
        Debug.DrawRay(transform.position + (-transform.forward * 1.0f), Vector3.down * 2.0f, Color.magenta);
        Debug.DrawRay(transform.position + (transform.right * 1.0f), Vector3.down * 2.0f, Color.magenta);
        Debug.DrawRay(transform.position + (-transform.right * 1.0f), Vector3.down * 2.0f, Color.magenta);
    }

}
