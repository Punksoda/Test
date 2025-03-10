using System;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount); // 데미지의 양만큼 UI의 상태창을 변경하는 함수 지정
}

public interface IHealable
{
    void Heal(int healAmount); // 회복의 양만큼 UI의 상태창을 변경하는 함수 지정
}

public class PlayerCondition : MonoBehaviour, IDamagable, IHealable
{

    public UICondition uiCondition;
    public event Action onTakeDamage;
    public event Action onHeal;

    Condition health { get { return uiCondition.health; } }
    Condition special { get { return uiCondition.special; } }
 
  public void Heal(int healAmount) // UI에서 해당 회복양만큼의 상호작용 실행
    {
        health.Add(healAmount); 
        onHeal?.Invoke();
    }

    public void TakePhysicalDamage(int damageAmount) // UI에서 해당 피해만큼의 상호작용 실행 후, 다시 초기화
    {
        health.Decrease(damageAmount);
        onTakeDamage?.Invoke();
    }

}
