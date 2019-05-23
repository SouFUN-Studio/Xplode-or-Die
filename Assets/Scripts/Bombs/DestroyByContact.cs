using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	public int lifes;

   
	void OnTriggerEnter2D(Collider2D other) {

		if(other.tag == "Ally")
        {
            return;
        }
        if (other.tag == "Enemy")
        {
            GameObject.Find("GameController").GetComponent<GameController>().ResetCombo();
            other.GetComponent<HazzardMover>().currentSpeed = 0.0f;
            other.GetComponent<Animator>().SetBool("destroy", true);
            lifes--;
            GameObject.Find("GameController").GetComponent<GameController>().DestroyAllies(lifes);
        }
	}

    public int GetLifes()
    {
        return lifes;
    }

    public void SetLifes(int lifes)
    {
        this.lifes = lifes;
    }

    
}
