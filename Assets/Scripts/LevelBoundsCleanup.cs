using UnityEngine;

public class LevelBoundsCleanup : MonoBehaviour
{
    public Game game;

    // Start is called before the first frame update
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.parent == transform)
        {
            bool doPostMortemPlayerCheck = collision.gameObject.GetComponent<Player>();

            collision.transform.SetParent(null);
            Destroy(collision.gameObject);

            if (doPostMortemPlayerCheck)
            {
                Player play = transform.GetComponentInChildren<Player>();

                if (play == null)
                {
                    game.HandleGameLoss();
                }
            }
        }
    }
}
