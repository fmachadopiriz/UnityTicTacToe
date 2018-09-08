using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] buttonList;

    private string playerSide;

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    void Awake()
    {
        this.SetGameControllerReferenceOnButtons();
        this.playerSide = "X";
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public bool CheckButtons(int one, int two, int three)
    {
        return (buttonList[one].text == playerSide && buttonList[two].text == playerSide && buttonList[three].text == playerSide);
    }

    public void EndTurn()
    {
        if (CheckButtons(0, 1, 2) || CheckButtons(3, 4, 5) || CheckButtons(6, 7, 8) ||
            CheckButtons(0, 3, 6) || CheckButtons(1, 4, 7) || CheckButtons(2, 5, 8) ||
            CheckButtons(0, 4, 8) || CheckButtons(2, 4, 6))
        {
            this.GameOver();
        }
        ChangeSides();
    }

    void GameOver()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = false;
        }
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
    }
}
