using System.Collections.Generic;
using UnityEngine;

public class DamageObject : MonoBehaviour
{
    public int damage; // 데미지 양
    public float damageTime; // 지속적으로 데미지를 받는 기준 시간

    List<IDamagable> damagethings  = new List<IDamagable>(); // 리스트에 데미지를 받는 오브젝트를 미리 저장

    void Start()
    {
        InvokeRepeating("DealDamage", 0f, damageTime); // IvokeReapeating을 사용해, 반복시간을 지정한 뒤, 기준시간마다 초기화하여 반복함
    }

    void DealDamage() // 데미지를 받는 함수
    {
        for (int i = 0; i < damagethings.Count; i++)
        {
            damagethings[i].TakePhysicalDamage(damage); // 데미지를 주는 오브젝트를 받아와서 UI 상태창을 변경받는 함수 호출
        }
    }

    private void OnTriggerEnter(Collider other) // 해당 타입의 컴포넌트를 찾았다면, IDamagable에 등록되어 있는지 확인하고, 객체를 추가함
    {
        if(other.TryGetComponent(out IDamagable damagable))
        {
            damagethings.Add(damagable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out IDamagable damagable)) // 해당 타입의 컴포넌트를 찾았다면,  IDamagable에 등록된 리스트 중 damagable를 제거함
        {
            damagethings.Remove(damagable);
        }
    }
}
