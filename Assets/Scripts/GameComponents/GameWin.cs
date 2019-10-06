using UnityEngine;

public class GameWin : MonoBehaviour
{
    Game game;

    public void Start()
    {
        game = FindObjectOfType<Game>();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Player>())
        {
            game.HandleGameWin();
        }
    }


}
