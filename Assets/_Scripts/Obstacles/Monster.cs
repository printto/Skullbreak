using UnityEngine;
using UnityEngine.SceneManagement;


public class Monster : MonoBehaviour {

    private float direction;

	// Use this for initialization
	void Start () {
        var rand = Random.Range(0,2);
		switch(rand)
        {
            case 0:
                direction = -1f;
                break;
            case 1:
                direction = 1f;
                break;
            default:
                direction = 1f;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, 6 * direction * Time.deltaTime);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Bullet"))
        {
            Dead();
        } else if (!Player.isDamaged)
        {
            if ((collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("PlayerFace")) && GameMaster.lifePoint > 0)
            {
                if (!Player.isTeleporting)
                {
                    GameMaster.removeLife(1);
                    Dead();
                }
            }
            else if ((collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("PlayerFace")) && GameMaster.lifePoint <= 0)
            {
                if (!Player.isTeleporting)
                {
                    Dead();
                    EndGame();
                }
            } else
            {
                ChangeDirection();
            }
        } else
        {
            if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("PlayerFace"))
            {
                Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
            }
        }
    }

    void EndGame()
    {
        //SceneManager.LoadScene(2);
        Initiate.Fade("DeadScene", Color.black, 4f);

    }

    void ChangeDirection()
    {
        direction *= -1f;
    }

    void Dead()
    {
        Destroy(gameObject);
    }
}
