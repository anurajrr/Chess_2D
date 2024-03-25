using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Import the UnityEngine.UI namespace for accessing UI components

public class Game : MonoBehaviour
{
    // Reference to the Timer script
    public Timer timer;

    // Reference to the images attached to the panels
    public Image whitePanelImage;
    public Image blackPanelImage;

    // Reference to the chesspiece prefab
    public GameObject chesspiece;

    // Matrices needed, positions of each of the GameObjects
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];


    // Current turn
    private string currentPlayer = "white";

    //winner panel 
    [SerializeField] private GameObject winnerPanel;
    [SerializeField] private TextMeshProUGUI winnerNameText;
    

    // Game Ending
    private bool gameOver = false;

    void Start()
    {
        playerWhite = new GameObject[] { Create("white_rook", 0, 0), Create("white_knight", 1, 0),
            Create("white_bishop", 2, 0), Create("white_queen", 3, 0), Create("white_king", 4, 0),
            Create("white_bishop", 5, 0), Create("white_knight", 6, 0), Create("white_rook", 7, 0),
            Create("white_pawn", 0, 1), Create("white_pawn", 1, 1), Create("white_pawn", 2, 1),
            Create("white_pawn", 3, 1), Create("white_pawn", 4, 1), Create("white_pawn", 5, 1),
            Create("white_pawn", 6, 1), Create("white_pawn", 7, 1) };
        playerBlack = new GameObject[] { Create("black_rook", 0, 7), Create("black_knight",1,7),
            Create("black_bishop",2,7), Create("black_queen",3,7), Create("black_king",4,7),
            Create("black_bishop",5,7), Create("black_knight",6,7), Create("black_rook",7,7),
            Create("black_pawn", 0, 6), Create("black_pawn", 1, 6), Create("black_pawn", 2, 6),
            Create("black_pawn", 3, 6), Create("black_pawn", 4, 6), Create("black_pawn", 5, 6),
            Create("black_pawn", 6, 6), Create("black_pawn", 7, 6) };

        // Set all piece positions on the positions board
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }

        // Start the first player's timer
        StartPlayerTimer();
        winnerPanel.SetActive(false);
        // Set the initial panel colors
        SetPanelColors();
    }
    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        ChessPiece cm = obj.GetComponent<ChessPiece>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        ChessPiece cm = obj.GetComponent<ChessPiece>();
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    void StartPlayerTimer()
    {
        if (GetCurrentPlayer() == "white")
        {
            timer.StartWhiteTimer();
        }
        else
        {
            timer.StartBlackTimer();
        }
    }

    void StopPlayerTimer()
    {
        if (GetCurrentPlayer() == "white")
        {
            timer.StopWhiteTimer();
        }
        else
        {
            timer.StopBlackTimer();
        }
    }

    public void NextTurn()
    {
        StopPlayerTimer(); // Stop the timer for the current player

        // Switch players
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }

        StartPlayerTimer(); // Start the timer for the next player

        SetPanelColors(); // Update the panel colors
    }

    // Method to set the panel colors based on whose turn it is
    void SetPanelColors()
    {
        if (GetCurrentPlayer() == "white")
        {
            whitePanelImage.color = Color.white; // Change the color of the white panel image to white
            blackPanelImage.color = new Color32(51, 51, 51, 255); // Change the color of the black panel image to specific RGB (51, 51, 51)
        }
        else
        {
            whitePanelImage.color = new Color32(51, 51, 51, 255); // Change the color of the white panel image to gray
            blackPanelImage.color = Color.white; // Change the color of the black panel image to black
        }
    }

        public void ShowWinner(string playerName)
    {   
        gameOver = true;
        winnerNameText.text = playerName + " Wins";
        winnerPanel.SetActive(true); // Activate the winner panel
    }


    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
        winnerPanel.SetActive(false);
        StartPlayerTimer();

    }
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quiting");
    }

}
