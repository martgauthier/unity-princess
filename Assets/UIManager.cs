using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {

    public TextMeshProUGUI text;

    public void UpdateLapText(string message)
    {
        text.SetText(message);
    }
}
