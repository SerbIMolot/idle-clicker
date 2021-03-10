using UnityEditor;

using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class BaseClick : MonoBehaviour
{
    public static LargeNumber AllClickerStats = new LargeNumber();

    public string name;

    public TextMesh basesText;
    public TextMesh CostText;
    public TextMesh NameText;

    public float countdownTime = 10;

    public LargeNumber baseIncomePerProduce = new LargeNumber(5, 3);

    public LargeNumber TotalBaseIncome { get { return number; } }

    public int numOfBases = 0;

    public LargeNumber cost = new LargeNumber(1f);

    LargeNumber number = new LargeNumber();

    ClickTimer timer;

    void Start()
    {
        if (!string.IsNullOrEmpty(name))
        {
            NameText.text = name;
        }
        if (!EditorApplication.isPlaying)
        {
            return;
        }
        basesText.text = numOfBases.ToString();
        timer = GetComponentInChildren<ClickTimer>();

        UpdateStats();
        timer.startTimer(countdownTime);
    }
    void IncreaceBase()
    {
        if (AllClickerStats >= cost)
        {
            numOfBases++;

            AllClickerStats -= cost;
            cost *= 1.2f;
            timer.text.text = timer.GetProductionPerTime();
            UpdateStats();
        }

    }

    public void UpdateStats()
    {
        number = baseIncomePerProduce * numOfBases;
        basesText.text = numOfBases.ToString();
        CostText.text = "Cost: " + cost.ToEngeneeringString();
    }
    private void Update()
    {
        if (EditorApplication.isPlaying)
        {
            return;
        }
        NameText.text = name;
        UpdateStats();

        //timer.text.text = $"{baseIncomePerProduce.ToEngeneeringString()}/{countdownTime}";
    }
    public void Produce()
    {
        AllClickerStats += number;
    }
    private void OnMouseDown()
    {
        IncreaceBase();
    }
}
