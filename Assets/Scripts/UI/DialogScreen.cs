using TMPro;
using UnityEngine;

public class DialogScreen : UIScreen
{
    public static DialogScreen Instance = null;

    [SerializeField] private TextMeshProUGUI messageText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(MetaConstants.readDialog))
            {
                Hide();
            }
        }
    }

    public void PopulateMessage(string Message)
    {
        messageText.text = Message;

        Show();
    }

}
