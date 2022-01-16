using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    private int score;
    private TextMeshProUGUI ui;

    void Start()
    {
        score = 0;
        ui = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetScore() {
        score = 0;
        updateScore();
    }

    public void increaseScore() {
        score++;
        updateScore();
    }

    private void updateScore() {
        ui.text = score.ToString();
    }
}
