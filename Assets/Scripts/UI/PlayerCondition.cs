using System;
using UnityEngine;

public interface IDamagable
{
    void TakePhysicalDamage(int damageAmount); // �������� �縸ŭ UI�� ����â�� �����ϴ� �Լ� ����
}

public interface IHealable
{
    void Heal(int healAmount); // ȸ���� �縸ŭ UI�� ����â�� �����ϴ� �Լ� ����
}

public class PlayerCondition : MonoBehaviour, IDamagable, IHealable
{

    public UICondition uiCondition;
    public event Action onTakeDamage;
    public event Action onHeal;

    Condition health { get { return uiCondition.health; } }
    Condition special { get { return uiCondition.special; } }
 
  public void Heal(int healAmount) // UI���� �ش� ȸ���縸ŭ�� ��ȣ�ۿ� ����
    {
        health.Add(healAmount); 
        onHeal?.Invoke();
    }

    public void TakePhysicalDamage(int damageAmount) // UI���� �ش� ���ظ�ŭ�� ��ȣ�ۿ� ���� ��, �ٽ� �ʱ�ȭ
    {
        health.Decrease(damageAmount);
        onTakeDamage?.Invoke();
    }

}
