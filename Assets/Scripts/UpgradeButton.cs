using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

enum UpgradeType
{

}
public class UpgradeButton : MonoBehaviour
{
    static GameObject[] objects;
    static readonly List<BaseClick> objectss = new List<BaseClick>();

    Button button;
    public TMP_Text buttonText;

    BaseClick upgradedClicker;
    float upgradeSize = 0f;

    void Awake()
    {
        if (objectss.Count == 0)
        {
            objects = GameObject.FindGameObjectsWithTag("Clicker");
            foreach (var obj in objects)
            {
                objectss.Add(obj.GetComponent<BaseClick>());
            }
        }
        button = GetComponent<Button>();
        button.onClick.AddListener(UpgradeClick);

        int randomClicker = Random.Range(0, objects.Length);
        Debug.Log("Random  " + randomClicker);
        if (randomClicker == objects.Length)
        {
            upgradedClicker = null;
        }
        else
        {
            upgradedClicker = objects[randomClicker].GetComponent<BaseClick>();

        }
    }
    void Start()
    {
        float chance = Random.Range(0f, 1f);
        if (chance <= 0.75f)
        {
            upgradeSize = 3;
        }
        else if (chance <= 0.50f)
        {
            upgradeSize *= Random.Range(1f, 2f);
        }
        else if (chance <= 0.23f)
        {
            upgradeSize *= Random.Range(5f, 10f);
        }
        else if (chance <= 0.01f)
        {
            upgradeSize *= Random.Range(50f, 100f);
        }
        else
        {
            upgradeSize = Random.Range(1f, 1.4f);
        }

        if (!upgradedClicker)
        {
            buttonText.text = string.Format("All x{0:0.0}", upgradeSize);
        }
        else
        {
            buttonText.text = string.Format("{0} x{1:0.0}", upgradedClicker.name, upgradeSize);
        }

    }

    void UpgradeClick()
    {
        if (!upgradedClicker)
        {
            foreach (var obj in objectss)
            {
                obj.baseIncomePerProduce *= upgradeSize;
            }
        }
        else
        {
            upgradedClicker.baseIncomePerProduce *= upgradeSize;
            upgradedClicker.UpdateStats();
        }

    }


}
