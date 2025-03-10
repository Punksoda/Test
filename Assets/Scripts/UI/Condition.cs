using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float maxValue;
    public float startValue;
    public float passiveValue;
    public Image uiBar;

    void Start()
    {
        curValue = startValue;
    }

    
    void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

   float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        Debug.Log("체력증가!");
        curValue = Mathf.Min(curValue + value, maxValue);
    }

    public void Decrease(float value)
    {
        Debug.Log("체력감소!");
        curValue = Mathf.Max(curValue - value, 0.0f);
    }
}
