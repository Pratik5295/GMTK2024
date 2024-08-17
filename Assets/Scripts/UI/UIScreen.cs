using UnityEngine;

public class UIScreen : MonoBehaviour,IScreen
{
    [SerializeField]
    private GameObject screen;

    [SerializeField] 
    public bool isActive => screen != null? screen.activeSelf : gameObject.activeSelf;

    public void Show()
    {
        screen.SetActive(true);
    }

    public void Hide()
    {
        screen.SetActive(false);
    }
}
