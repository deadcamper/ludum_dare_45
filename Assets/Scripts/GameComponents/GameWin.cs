using UnityEngine;

public class GameWin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Player>())
        {
            Debug.Log("You win!!!");
        }
    }


}
