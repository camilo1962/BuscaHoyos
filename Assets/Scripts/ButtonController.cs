using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button upButton;
    public Button downButton;
    public Button rightButton;
    public Button leftButton;

    public PlayerMovement playerMovement;

    void Start()
    {
        upButton.onClick.AddListener(MoveUp);
        downButton.onClick.AddListener(MoveDown);
        rightButton.onClick.AddListener(MoveRight);
        leftButton.onClick.AddListener(MoveLeft);
    }

    void MoveUp()
    {
        playerMovement.MoveUp();
    }

    void MoveDown()
    {
        playerMovement.MoveDown();
    }

    void MoveRight()
    {
        playerMovement.MoveRight();
    }

    void MoveLeft()
    {
        playerMovement.MoveLeft();
    }
}
