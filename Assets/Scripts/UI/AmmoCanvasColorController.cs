using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class AmmoCanvasColorController : MonoBehaviour
{
    [SerializeField] GameObject enlargeUI;
    [SerializeField] GameObject shrinkUI;
    [SerializeField] Color enlargeColor;
    [SerializeField] Color shrinkColor;
    [SerializeField] TMP_Text ammoText;

    public void PrimaryFireUI()
    {
        enlargeUI.SetActive(true);
        shrinkUI.SetActive(false);
        ammoText.color = enlargeColor;
    }

    public void SecondaryFireUI()
    {
        enlargeUI.SetActive(false);
        shrinkUI.SetActive(true);
        ammoText.color = shrinkColor;
    }

}
