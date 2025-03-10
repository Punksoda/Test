using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public int damage; // ������ ��
    public float damageTime; // ���������� �������� �޴� ���� �ð�

    List<IDamagable> damagethings  = new List<IDamagable>(); // ����Ʈ�� �������� �޴� ������Ʈ�� �̸� ����

    void Start()
    {
        InvokeRepeating("DealDamage", 0f, damageTime); // IvokeReapeating�� �����, �ݺ��ð��� ������ ��, ���ؽð����� �ʱ�ȭ�Ͽ� �ݺ���
    }

    void DealDamage() // �������� �޴� �Լ�
    {
        for (int i = 0; i < damagethings.Count; i++)
        {
            damagethings[i].TakePhysicalDamage(damage); // �������� �ִ� ������Ʈ�� �޾ƿͼ� UI ����â�� ����޴� �Լ� ȣ��
        }
    }

    private void OnTriggerEnter(Collider other) // �ش� Ÿ���� ������Ʈ�� ã�Ҵٸ�, IDamagable�� ��ϵǾ� �ִ��� Ȯ���ϰ�, ��ü�� �߰���
    {
        if(other.TryGetComponent(out IDamagable damagable))
        {
            damagethings.Add(damagable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out IDamagable damagable)) // �ش� Ÿ���� ������Ʈ�� ã�Ҵٸ�,  IDamagable�� ��ϵ� ����Ʈ �� damagable�� ������
        {
            damagethings.Remove(damagable);
        }
    }
}
