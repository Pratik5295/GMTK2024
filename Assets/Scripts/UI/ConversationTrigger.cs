using UnityEngine;

public class ConversationTrigger : TriggerBox
{
    [SerializeField] private string message;

    [SerializeField] private bool showMultiple = false;
    [SerializeField] private bool isShown = false;
    public override void FireTrigger()
    {
        base.FireTrigger();
        PopulateMessage();
    }

    public void PopulateMessage()
    {
        if (!isShown)
        {
            DialogScreen.Instance.PopulateMessage(message);
            isShown = true;
        }
        else
        {
            if (showMultiple)
            {
                DialogScreen.Instance.PopulateMessage(message);
            }
        }
    }
}
