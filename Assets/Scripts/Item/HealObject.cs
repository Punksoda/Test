using UnityEngine;

public class HealObject : MonoBehaviour
{
    public int healAmount = 20; // 획득시 추가되는 회복양
    private void OnTriggerEnter(Collider other)  // 해당 타입의 컴포넌트를 찾고, IHealable을 가지고 있는 컴포넌트를 리스트에 추가하고, 발동 이후 파괴함
    {
        if (other.TryGetComponent(out IHealable healable))
        {
            healable.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
