using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Events;

public class MakeThing : MonoBehaviour
{
    private bool gameStart;
    private Int64 timeStart;
    private TextMeshProUGUI textBox;
    private List<Tuple<int, int>> orderList;
    private int[] currOrder;
    private System.Random random;
    public UnityEvent addPoints, gameOver;

    // Start is called before the first frame update
    void Start()
    {
        orderList = new List<Tuple<int,int>>();
        textBox = GetComponent<TextMeshProUGUI>();
        random = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStart && Math.Abs(timeStart - System.DateTime.UtcNow.Ticks)/TimeSpan.TicksPerSecond >= 4) {
            timeStart = System.DateTime.UtcNow.Ticks;

            //Generate a new pair
            Tuple<int, int> newOrder = new Tuple<int, int>(random.Next(0, 10), random.Next(0, 10));
            orderList.Add(newOrder);

            updateList();
        }
    }

    private void updateList() {
            textBox.text = "";
            for(int i = 0; i < orderList.Count; i++) {
                textBox.text += orderList[i].Item1 + " Red, " + orderList[i].Item2 + " Green";
                if(i != orderList.Count - 1) textBox.text += "\n";
            }
    }

    public void startGame() {
        if(!gameStart) {
            gameStart = true;
            timeStart = System.DateTime.UtcNow.Ticks;
        }
    }

    private void endGame() {
        gameStart = false;
        currOrder = new int[2];
        orderList.Clear();
        updateList();
    }

    public void submitOrder() {
        if(currOrder == null) return;
        Tuple<int, int> order = new Tuple<int, int>(currOrder[0], currOrder[1]);
        currOrder = new int[2];
        if(order.Item1 == orderList[0].Item1 && order.Item2 == orderList[0].Item2) {
            orderList.RemoveAt(0);
            updateList();
            addPoints.Invoke();
            Debug.Log("CORRECT!");
        } else {
            Debug.Log("WRONG!");
            gameOver.Invoke();
            endGame();
        }
    }

    public void addRed() {
        if(currOrder == null) {
            currOrder = new int[2];
        }
        currOrder[0] += 1;
    }

    public void addGreen() {
        if(currOrder == null) {
            currOrder = new int[2];
        }
        currOrder[1] += 1;
    }
}
