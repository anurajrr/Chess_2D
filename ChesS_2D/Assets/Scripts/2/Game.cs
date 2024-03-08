using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Game : MonoBehaviour
{   
    [SerializeField] private GameObject chessPiece;
    private GameObject[,] positions = new GameObject[8,8];
    private GameObject[] player_Black = new GameObject[16];
    private GameObject[] player_White = new GameObject[16];
    // Start is called before the first frame update

    private string currentPlayer = "white";
    private bool gameOver = false;
    void Start()
    {   
        player_White = new GameObject[]
        {
            Create("white_King",4,0),Create("white_Queen",3,0),Create("white_Pawn",0,1),Create("white_Pawn",1,1),
            Create("white_Pawn",2,1),Create("white_Pawn",3,1),Create("white_Pawn",4,1),Create("white_Pawn",5,1),
            Create("white_Pawn",6,1),Create("white_Pawn",7,1),Create("white_Bishop",2,0),Create("white_Bishop",5,0),
            Create("white_Knight",1,0),Create("white_Knight",6,0),Create("white_Rook",0,0),Create("white_Rook",7,0)
        };
        player_Black = new GameObject[]
        {
            Create("black_King",4,7),Create("black_Queen",3,7),Create("black_Pawn",0,6),Create("black_Pawn",1,6),
            Create("black_Pawn",2,6),Create("black_Pawn",3,6),Create("black_Pawn",4,6),Create("black_Pawn",5,6),
            Create("black_Pawn",6,6),Create("black_Pawn",7,6),Create("black_Bishop",2,7),Create("black_Bishop",5,7),
            Create("black_Knight",1,7),Create("black_Knight",6,7),Create("black_Rook",0,7),Create("black_Rook",7,7)
        };

        for (int i = 0; i < player_Black.Length; i++)
        {
            setPosition(player_Black[i]);
            setPosition(player_White[i]);
        }

        
    }

    public GameObject Create(string name,int x,int y)
    {
        GameObject obj = Instantiate(chessPiece,new Vector3(0,0,-1),Quaternion.identity);
        ChessPiece piece = obj.GetComponent<ChessPiece>();
        piece.name = name;
        piece.SetXBoard(x);
        piece.SetYBoard(y);
        piece.Activate();
        return obj;
    }

    public void setPosition(GameObject obj)
    {
        ChessPiece piece = obj.GetComponent<ChessPiece>();
        positions[piece.GetXBoard(),piece.GetYBoard()] = obj;
    }

  
}
