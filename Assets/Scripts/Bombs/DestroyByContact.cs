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
            Destroy(other.gameObject);
            lifes--;
        }
	}

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }

    public int getLifes()
    {
        return lifes;
    }

    public void setLifes(int lifes)
    {
        this.lifes = lifes;
    }

    
}
