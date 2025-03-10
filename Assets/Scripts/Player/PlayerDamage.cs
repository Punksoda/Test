using System.Collections;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public Renderer playerRenderer; // 플레이어의 Renderer 컴포넌트
    public float flashSpeed = 1f;   // 깜빡이는 속도
 
    private Material playerMaterial; // 플레이어 머티리얼 참조
    private Color originalColor;     // 원래 색상을 저장
    private Coroutine coroutine;    // 코루틴 호출

    private void Start()
    {
        playerMaterial = playerRenderer.material; // 플레이어 머테리얼 가져오기
        originalColor = playerMaterial.color; // 원래 색상 저장

        // 데미지 이벤트에 Flash 메서드 연결
        CharacterManager.Instance.Player.condition.onTakeDamage += () => Flash(Color.red);
        CharacterManager.Instance.Player.condition.onHeal += () =>Flash(Color.green);
    }

    public void Flash(Color flashColor)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine); // 이전 코루틴 중지
        }

        coroutine = StartCoroutine(ColorEffect(flashColor));
    }


    private IEnumerator ColorEffect(Color flashColor)
    { 
        playerMaterial.color = flashColor;

        float elapsedTime = 0f;

        while (elapsedTime < flashSpeed)
        {
            elapsedTime += Time.deltaTime;
            playerMaterial.color = Color.Lerp(flashColor, originalColor, elapsedTime / flashSpeed); // lerp를 사용해서 빛나는 색깔을 천천히 돌아오게 함
            yield return null;
        }

        playerMaterial.color = originalColor; // 원래 색으로 돌아감

       
    }
}
