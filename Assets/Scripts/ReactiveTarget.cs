using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour { 
 
    public void ReactToHit() {
        WanderingAI enemyAi = GetComponent<WanderingAI>();
        if(enemyAi != null ) {
            enemyAi.ChangeState(EnemyStates.dead);
        }

        StartCoroutine (Die());
    }

    private IEnumerator Die() {
        // Enemy falls over and disappears after two seconds
        iTween.RotateAdd (this.gameObject, new Vector3 (-75, -0, 0), 1);
    
        yield return new WaitForSeconds (1);

        Destroy (this.gameObject);
    }
}
