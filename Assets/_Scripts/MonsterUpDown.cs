using UnityEngine;
using UnityEngine.SceneManagement;


public class MonsterUpDown : MonoBehaviour {

    private float direction;

	// Use this for initialization
	void Start () {
        var rand = Random.Range(0,2);
        Debug.Log("MonRan : " + rand);
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
        transform.Translate(0, 6 * direction * Time.deltaTime, 0 );
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Bullet")) {
            Dead();
        } else if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("PlayerFace")) {
            EndGame();
        } else {
            Debug.Log("hit wall!!");
            ChangeDirection();
        }
    }

    void EndGame()
    {
        SceneManager.LoadScene(2);
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
