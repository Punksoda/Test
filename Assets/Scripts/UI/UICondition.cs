using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition special;
    void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;

        // ���� ��ũ��Ʈ�� �����, ��ü�� ������Ʈ�� �ǹ��Ѵ�
    }

}
