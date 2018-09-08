using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text[] buttonList;

    private string playerSide;

    public GameObject gameOverPanel;

    public Text gameOverText;
    private int moveCount;

    public GameObject restarButton;

    public Player playerX;

    public Player playerO;

    public PlayerColor activePlayerColor;

    public PlayerColor inactivePlayerColor;

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
        this.gameOverPanel.SetActive(false);
        this.moveCount = 0;
        this.restarButton.SetActive(false);
        this.SetPlayerColors(playerX, playerO);
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
        this.moveCount++;

        if (CheckButtons(0, 1, 2) || CheckButtons(3, 4, 5) || CheckButtons(6, 7, 8) ||
            CheckButtons(0, 3, 6) || CheckButtons(1, 4, 7) || CheckButtons(2, 5, 8) ||
            CheckButtons(0, 4, 8) || CheckButtons(2, 4, 6))
        {
            this.GameOver(playerSide);
        }
        else if (this.moveCount >= 9)
        {
            this.GameOver("draw");
        }
        else
        {
            ChangeSides();
        }
    }

    void GameOver(string winningPlayer)
    {
        this.SetBoardInteractable(false);
        if (winningPlayer == "draw")
        {
            this.SetGameOverText("It's a draw");
        }
        else
        {
            this.SetGameOverText(playerSide + " Wins!");
        }
        this.restarButton.SetActive(true);
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
        if (playerSide == "X")
        {
            this.SetPlayerColors(this.playerX, this.playerO);
        }
        else
        {
            this.SetPlayerColors(this.playerO, this.playerX);
        }
    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    public void RestartGame()
    {
        this.playerSide = "X";
        this.moveCount = 0;
        this.gameOverPanel.SetActive(false);
        this.SetBoardInteractable(true);
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
        this.restarButton.SetActive(false);
    }

    public void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }
}
