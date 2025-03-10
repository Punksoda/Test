using UnityEngine;

public class HealObject : MonoBehaviour
{
    public int healAmount = 20; // ȹ��� �߰��Ǵ� ȸ����
    private void OnTriggerEnter(Collider other)  // �ش� Ÿ���� ������Ʈ�� ã��, IHealable�� ������ �ִ� ������Ʈ�� ����Ʈ�� �߰��ϰ�, �ߵ� ���� �ı���
    {
        if (other.TryGetComponent(out IHealable healable))
        {
            healable.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
