using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition special;
    void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;

        // 현재 스크립트가 적용된, 객체의 컴포넌트를 의미한다
    }

}
