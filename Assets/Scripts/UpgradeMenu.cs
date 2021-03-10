using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradeMenu : MonoBehaviour
{

    public GameObject prefabButton;
    public RectTransform ParentPanel;

    void Start()
    {
        Vector3 pos = new Vector3(0f, ParentPanel.rect.width - 50, 0f);
        for (int i = 0; i < 10; i++)
        {
            GameObject goButton = (GameObject)Instantiate(prefabButton);
            goButton.transform.position = pos;
            goButton.transform.SetParent(ParentPanel, false);
            goButton.transform.localScale = new Vector3(1, 1, 1);


            pos.y -= 40;
        }


    }


}
