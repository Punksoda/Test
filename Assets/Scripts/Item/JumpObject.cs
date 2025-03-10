using UnityEngine;

public class JumpObject : MonoBehaviour
{
    public PlayerController player;
    public float jumpF = 5f;

    public void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(Vector3.up * jumpF, ForceMode.Impulse); // ���� �� ��ŭ ���ؼ� ���������� �� ����
        }
        else
        {
            Debug.Log("�÷��̾�� ������ٵ� �Ҵ�Ǿ� ���� �ʽ��ϴ�!");
        }
    }
}
