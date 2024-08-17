using UnityEngine;

public class UIScreen : MonoBehaviour,IScreen
{
    [SerializeField]
    private GameObject screen;

    [SerializeField] private bool showAtStart;

    [SerializeField] 
    public bool isActive => screen != null? screen.activeSelf : gameObject.activeSelf;

    public virtual void Start()
    {
        if (showAtStart)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    public void Show()
    {
        screen.SetActive(true);
    }

    public void Hide()
    {
        screen.SetActive(false);
    }
}
