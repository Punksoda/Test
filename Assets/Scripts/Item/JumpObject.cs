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
            rb.AddForce(Vector3.up * jumpF, ForceMode.Impulse); // 위로 그 만큼 곱해서 순간적으로 힘 방출
        }
        else
        {
            Debug.Log("플레이어에게 리지드바디가 할당되어 있지 않습니다!");
        }
    }
}
