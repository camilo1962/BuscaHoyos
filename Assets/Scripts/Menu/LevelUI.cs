using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] Text txtLives;
    [SerializeField] Text txtLevel;
    [SerializeField] Text txtMoves;
    void Start()
    {
        
    }

    public void UpdateLevel(int level)
    {
        txtLevel.text = "Nivel: " + level + "";
    }
    public void UpdateLives(int lives)
    {
        txtLives.text = "Vidas: " + lives + "" ;
    }

    public void UpdateMoves(int moves)
    {
        txtMoves.text = "Movimientos: " + moves + "";
    }

    public void GameOver()
    {
        txtLevel.text = "¡Juego Acabado!";
       
    }
}
