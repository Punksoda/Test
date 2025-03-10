using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed; // �����̴� �ӵ�
    private Vector2 curMoveInput; // ���� �Է¹��� ����
    public float jumpForce; // �����ϴ� ��
    public LayerMask groundCheck; // ���߿��� ������ �����ϱ� ���� �׶��� ���̾� ����

    public Transform cameraContainer; // ������ ī�޶� �����̳ʿ� ���� transform �� �Ҵ�
    public float minLook; // �ּҽþ�
    public float maxLook; // �ִ�þ� 
    private float camCurLook; // ���� ���� ��
    public float lookSensertivity; // �þ�ȸ�� �ӵ�

    private Vector2 mouseDelta; // ���콺 ���� ��

    private Rigidbody rigid; // ������ �ٵ� 

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ���� �߾ӿ� �����ϰ�, �������� ���ϰ���
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    public void OnLookInput(InputAction.CallbackContext context) // InputSystem�� ���콺 �̵�ó�� �Է�
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnMoveInput(InputAction.CallbackContext context) // InputSystem�� �����̴� �Է°� �ޱ�
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

    public void OnJumpInput(InputAction.CallbackContext context) // InputSystem�� �����ϴ� �Է°� �ޱ�
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        }
    }
    private void Move() // �����̴� ���� ���� �����ϰ�, �����̴� �ӵ��� �׸�ŭ ����, ������ ���� �ӵ��� ����, �׸��� Rigidbody�� �̵��� �����Ŵ
    {
        Vector3 dir = transform.forward * curMoveInput.y + transform.right * curMoveInput.x;
        dir *= moveSpeed;
        dir.y = rigid.velocity.y;

        rigid.velocity = dir;
    }

    private void CameraLook() // ���콺�� y�� ���� �������� ī�޶��� ���� ȸ�� ���� �����Ѵ�, ī�޶� ������ �ִ� ī�޶� ��Ʈ�ѷ��� ȸ�� ���� �����Ѵ�
    {
        camCurLook += mouseDelta.y * lookSensertivity;
        camCurLook = Mathf.Clamp(camCurLook, minLook, maxLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurLook, 0, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensertivity, 0);
        // �÷��̾� ��ü�� y��(��, ��) ȸ���� ���콺 x�� �̵����� �����Ͽ� ������ ��ȯ�Ѵ�
        // FPS ���� �̵������� �����Ҷ�, ����ϱ� �����ϴ�
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4] // ��, ��, ������, ���ʿ��� ���̸� �߻��Ͽ�, �ٴ��� üũ�Ѵ�
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 1.6f, groundCheck)) // Ʈ������ ��ġ�� ���� ���̰� ����
            {
                Debug.Log("�׶��� �n");
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos() // �Ʒ� ������ �� üũ ���� �����
    {
        Debug.DrawRay(transform.position + (transform.forward * 1.0f), Vector3.down * 2.0f, Color.magenta);
        Debug.DrawRay(transform.position + (-transform.forward * 1.0f), Vector3.down * 2.0f, Color.magenta);
        Debug.DrawRay(transform.position + (transform.right * 1.0f), Vector3.down * 2.0f, Color.magenta);
        Debug.DrawRay(transform.position + (-transform.right * 1.0f), Vector3.down * 2.0f, Color.magenta);
    }

}
