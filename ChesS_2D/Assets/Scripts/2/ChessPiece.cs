using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    [SerializeField] private GameObject controller;
    [SerializeField] private GameObject moveHighlight;  
    private int xBoard = -1;
    private int yBoard = -1;

    private string player;

    [SerializeField]
    private Sprite black_King,black_Queen,black_Pawn,black_Bishop,black_Knight,black_Rook;
    [SerializeField]
    private Sprite white_King,white_Queen,white_Pawn,white_Bishop,white_Knight,white_Rook;

    public void Activate()
    {
        controller  = GameObject.FindGameObjectWithTag("GameController");
        setCordinates();
        switch (this.name)
        {
            case "black_King": this.GetComponent<SpriteRenderer>().sprite = black_King;break;            
            case "black_Queen": this.GetComponent<SpriteRenderer>().sprite = black_Queen;break;            
            case "black_Pawn": this.GetComponent<SpriteRenderer>().sprite = black_Pawn;break;            
            case "black_Bishop": this.GetComponent<SpriteRenderer>().sprite = black_Bishop;break;            
            case "black_Knight": this.GetComponent<SpriteRenderer>().sprite = black_Knight;break;            
            case "black_Rook": this.GetComponent<SpriteRenderer>().sprite = black_Rook;break; 

            case "white_King": this.GetComponent<SpriteRenderer>().sprite = white_King;break;            
            case "white_Queen": this.GetComponent<SpriteRenderer>().sprite = white_Queen;break;            
            case "white_Pawn": this.GetComponent<SpriteRenderer>().sprite = white_Pawn;break;            
            case "white_Bishop": this.GetComponent<SpriteRenderer>().sprite = white_Bishop;break;            
            case "white_Knight": this.GetComponent<SpriteRenderer>().sprite = white_Knight;break;            
            case "white_Rook": this.GetComponent<SpriteRenderer>().sprite = white_Rook;break;            
            
        }
    }

    private void setCordinates()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 0.66f;
        y *= 0.66f;

        x+= -2.3f;
        y+= -2.3f;

        this.transform.position = new Vector3(x,y,-1);
    }

    public int GetXBoard()
    {
        return xBoard;
    }
    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }
    public void SetYBoard(int y)
    {
        yBoard = y;
    }
}
