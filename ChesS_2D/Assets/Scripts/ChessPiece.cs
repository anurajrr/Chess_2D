using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public GameObject controller; // Reference to the game controller
    public GameObject movePlate; // Reference to the move plate prefab

    private int xBoard = -1; // X-coordinate of the piece on the board
    private int yBoard = -1; // Y-coordinate of the piece on the board
    private string player; // Player of the piece ("black" or "white")

    // Sprites for different chess pieces
    public Sprite black_queen, black_knight, black_bishop, black_king, black_rook, black_pawn;
    public Sprite white_queen, white_knight, white_bishop, white_king, white_rook, white_pawn;

    // Method to initialize the chess piece
    public void Activate()
    {
        // Find the game controller object
        controller = GameObject.FindGameObjectWithTag("GameController");

        // Set the initial coordinates of the piece
        SetCoords();

        // Assign the appropriate sprite based on the piece's name
        switch (this.name)
        {
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; break;
            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; break;
        }
    }

    // Method to set the coordinates of the chess piece on the board
    public void SetCoords()
    {
        // Calculate the actual position of the piece on the board
        float x = xBoard * 0.66f - 2.3f;
        float y = yBoard * 0.66f - 2.3f;

        // Set the position of the chess piece
        this.transform.position = new Vector3(x, y, -1.0f);
    }

    // Getter methods for the x and y coordinates of the piece
    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    // Setter methods for the x and y coordinates of the piece
    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    // Method called when the mouse is released over the chess piece
    private void OnMouseUp()
    {
        // Check if the game is not over and it's the current player's turn
        if (!controller.GetComponent<Game>().IsGameOver() && controller.GetComponent<Game>().GetCurrentPlayer() == player)
        {
            // Destroy existing move plates and initiate move plates for the selected piece
            DestroyMovePlates();
            InitiateMovePlates();
        }
    }

    // Method to destroy existing move plates
    public void DestroyMovePlates()
    {
        // Find all move plates in the scene and destroy them
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MoveHighLighter");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    // Method to initiate move plates for the selected piece
    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            // Handle different types of chess pieces
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "black_knight":
            case "white_knight":
                LMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                break;
            case "black_king":
            case "white_king":
                SurroundMovePlate();
                break;
            case "black_rook":
            case "white_rook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_pawn":
                if (yBoard == 6)
                {
                    PawnMovePlate(xBoard, yBoard - 1);
                    PawnMovePlate(xBoard, yBoard - 2);
                }
                else
                {
                    PawnMovePlate(xBoard, yBoard - 1);
                }
                break;
            case "white_pawn":
                if (yBoard == 1)
                {
                    PawnMovePlate(xBoard, yBoard + 1);
                    PawnMovePlate(xBoard, yBoard + 2);
                }
                else
                {
                    PawnMovePlate(xBoard, yBoard + 1);
                }
                break;
        }
    }

    // Method to generate move plates along a line
    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();
        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;
        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }
        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y).GetComponent<ChessPiece>().player != player)
        {
            MovePlateAttackSpawn(x, y);
        }
    }

    // Method to generate move plates for knight movement
    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    // Method to generate move plates surrounding the king
    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 0);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard + 0);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }

    // Method to generate a move plate at a specific point
    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);
            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (cp.GetComponent<ChessPiece>().player != player)
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    // Method to generate move plates for pawn movement
    // public void PawnMovePlate(int x, int y)
    // {
    //     Game sc = controller.GetComponent<Game>();
    //     if (sc.PositionOnBoard(x, y))
    //     {
    //         if (sc.GetPosition(x, y) == null)
    //         {
    //             MovePlateSpawn(x, y);
    //         }
    //         if (sc.PositionOnBoard(x + 1, y) && sc.GetPosition(x + 1, y) != null && sc.GetPosition(x + 1, y).GetComponent<ChessPiece>().player != player)
    //         {
    //             MovePlateAttackSpawn(x + 1, y);
    //         }
    //         if (sc.PositionOnBoard(x - 1, y) && sc.GetPosition(x - 1, y) != null && sc.GetPosition(x - 1, y).GetComponent<ChessPiece>().player != player)
    //         {
    //             MovePlateAttackSpawn(x - 1, y);
    //         }
    //     }
    // }

    // Method to generate move plates for pawn movement
public void PawnMovePlate(int x, int y)
{
    Game sc = controller.GetComponent<Game>();

    // Check if the position in front of the pawn is empty
    if (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
    {
        MovePlateSpawn(x, y);

        // Check if it's the pawn's initial position and if so, allow it to move two squares forward
        if ((player == "black" && yBoard == 6) || (player == "white" && yBoard == 1))
        {
            int y2 = (player == "black") ? yBoard - 2 : yBoard + 2;
            if (sc.PositionOnBoard(x, y2) && sc.GetPosition(x, y2) == null)
            {
                MovePlateSpawn(x, y2);
            }
        }
    }

    // Check if the diagonal positions contain opponent's pieces for possible attack moves
    int diagonalX1 = xBoard - 1;
    int diagonalX2 = xBoard + 1;
    int diagonalY = (player == "black") ? yBoard - 1 : yBoard + 1;

    if (sc.PositionOnBoard(diagonalX1, diagonalY) && sc.GetPosition(diagonalX1, diagonalY) != null && sc.GetPosition(diagonalX1, diagonalY).GetComponent<ChessPiece>().player != player)
    {
        MovePlateAttackSpawn(diagonalX1, diagonalY);
    }
    if (sc.PositionOnBoard(diagonalX2, diagonalY) && sc.GetPosition(diagonalX2, diagonalY) != null && sc.GetPosition(diagonalX2, diagonalY).GetComponent<ChessPiece>().player != player)
    {
        MovePlateAttackSpawn(diagonalX2, diagonalY);
    }
}


    // Method to spawn a move plate at a given position
    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX * 0.66f - 2.3f;
        float y = matrixY * 0.66f - 2.3f;
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        HighLight mpScript = mp.GetComponent<HighLight>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    // Method to spawn a move plate for an attack move at a given position
    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX * 0.66f - 2.3f;
        float y = matrixY * 0.66f - 2.3f;
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
        HighLight mpScript = mp.GetComponent<HighLight>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}
