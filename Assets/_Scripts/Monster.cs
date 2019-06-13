using UnityEngine;
using UnityEngine.SceneManagement;


public class Monster : MonoBehaviour {

    private float direction;

	// Use this for initialization
	void Start () {
        var rand = Random.Range(0,1);
		switch(rand)
        {
            case 0:
                this.direction = -1;
                break;
            case 1:
                this.direction = 1;
                break;
            default:
                this.direction = 1;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, 6 * this.direction * Time.deltaTime);
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Bullet")) {
            Dead();
        } else if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("PlayerFace")) {
            EndGame();
        } else {
            ChangeDirection();
        }
    }

    void EndGame()
    {
        SceneManager.LoadScene(1);
    }

    void ChangeDirection()
    {
        this.direction *= -1;
    }

    void Dead()
    {
        Destroy(gameObject, 1);
    }
}
