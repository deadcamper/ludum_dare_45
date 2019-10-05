using UnityEngine;

public class GameWin : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Player>())
        {
            Debug.Log("You win!!!");
        }
    }


}
