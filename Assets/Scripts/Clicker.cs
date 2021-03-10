using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clicker : MonoBehaviour
{

    public Text playerMoney;


    void Update()
    {
        playerMoney.text = $"Balance: {BaseClick.AllClickerStats.ToEngeneeringString()} $";
    }
    private void FixedUpdate()
    {
    }
}

