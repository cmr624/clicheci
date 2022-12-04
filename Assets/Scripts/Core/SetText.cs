using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetText : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI _tmp;
    void Start()
    {
        _tmp = GetComponent<TextMeshProUGUI>();
        _tmp.text = "Score: " + GameFlowManager.Instance.score;
    }

}
