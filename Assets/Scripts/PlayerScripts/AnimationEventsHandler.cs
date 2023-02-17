using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    [SerializeField] GameObject attackCheck;
    LevelController lvlControl;

    void Start()
    {
        lvlControl = FindObjectOfType<LevelController>();
    }

    public void PlayerAttack()
    {
        attackCheck.SetActive(true);
        StartCoroutine(DisableAttackCheck());

    }

    IEnumerator DisableAttackCheck()
    {
        yield return new WaitForSeconds(0.25f);
        attackCheck.SetActive(false);
    }

    void Die()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }

}
