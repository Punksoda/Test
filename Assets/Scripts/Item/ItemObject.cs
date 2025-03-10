using UnityEngine;

public interface IIteractable
{
    public string GetInteractPrompt();
    public void OnInteract();
}
public class ItemObject : MonoBehaviour, IIteractable
{
    public ItemData data;
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";
        return str ;
    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
}
