using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Aabbeell : MonoBehaviour
{
    public Transform attackPos;
    
    public Transform fireAttackPos;
    
    public float iceballCoolDown = 2f;
    private float nextIceballTime = 0f;
    public GameObject iceball;
    public float iceAngle = 5;
    public float fireballCoolDown = 2f;
    private float nextFireballTime = 0f;
    public GameObject fireball;
    public int prueba = 0;

	//Jugador
    GameObject jugador;

	//Transform del enemigo
    Transform enemyTransform;
    
    // Start is called before the first frame update
    void Start()
    {
		jugador = GameObject.Find("Chawa");
		enemyTransform = GetComponent<Transform> ();
        StartCoroutine(cambiarEstado(0));
    }

    // Update is called once per frame
    void Update()
    {

        float distanciachawaenemigo = jugador.transform.position.x - enemyTransform.position.x;
		
		if(distanciachawaenemigo > 0){
			//Jugador a la derecha
			enemyTransform.localScale = new Vector3(((enemyTransform.localScale.x<0)?enemyTransform.localScale.x:enemyTransform.localScale.x*-1), enemyTransform.localScale.y, 1);
		}else{
			//Jugador a la izquierda
			enemyTransform.localScale = new Vector3(((enemyTransform.localScale.x>0)?enemyTransform.localScale.x:enemyTransform.localScale.x*-1), enemyTransform.localScale.y, 1);
		}
    }

	IEnumerator cambiarEstado(float seconds)
    {
        yield return new WaitForSeconds(seconds);
		//Cambiar de posicion
		int position = (int)Random.Range(1, 3.99f);
		switch(position){
			case 1:
				positionA();
				break;
			case 2:
				positionB();
				break;
			case 3:
				positionC();
				break;
		}
		// Tipo de ataque
		int attack = (int)Random.Range(1, 3.99f);
		switch(attack){
			case 1:
				iceAttackHorizaontal();
				break;
			case 2:
				iceAttackVertical();
				break;
			case 3:
				fireAttack();
				break;
		}
		
        StartCoroutine(cambiarEstado(3));
    }

	void positionA(){
		gameObject.transform.localPosition = new Vector2(59f, 2.99f);
	}

	void positionB(){
		gameObject.transform.localPosition = new Vector2(69f, 13.56f);
	}

	void positionC(){
		gameObject.transform.localPosition = new Vector2(80f, 2.99f);
	}

    void iceAttackHorizaontal()
    {
        nextIceballTime = Time.time + iceballCoolDown;
        var position = attackPos.position;
        //position.x = position.x * -1;
            
        GameObject spell = Instantiate(iceball, new Vector2(position.x, position.y ), Quaternion.identity);
        PlayerIce spell1Controller = spell.GetComponent<PlayerIce>();
        spell1Controller.enemy = "Player";
        GameObject spell2 = Instantiate(iceball, new Vector2(position.x, position.y ), Quaternion.identity);
        PlayerIce spell2Controller = spell2.GetComponent<PlayerIce>();
        spell2Controller.enemy = "Player";
        GameObject spell3 = Instantiate(iceball, new Vector2(position.x, position.y ), Quaternion.identity);
        PlayerIce spell3Controller = spell3.GetComponent<PlayerIce>();
        spell3Controller.enemy = "Player";
            
        if (Mathf.Sign(transform.localScale.x) > 0)
        {
            spell.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,180);
            spell2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180 + iceAngle);
            spell3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 180 - iceAngle);
        }
        else
        {
            spell2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, iceAngle);
            spell3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 360 - iceAngle);
        }
    }

	void iceAttackVertical()
	{
        nextIceballTime = Time.time + iceballCoolDown;
        var position = attackPos.position;
        //position.x = position.x * -1;
            
        GameObject spellA1 = Instantiate(iceball, new Vector2(77.5f + Random.Range(-2.5f, 2.5f), 13), Quaternion.identity);
        PlayerIce spellA1Controller = spellA1.GetComponent<PlayerIce>();
        spellA1Controller.enemy = "Player";
        GameObject spellA2 = Instantiate(iceball, new Vector2(77.5f + Random.Range(-2.5f, 2.5f), 13), Quaternion.identity);
        PlayerIce spellA2Controller = spellA2.GetComponent<PlayerIce>();
        spellA2Controller.enemy = "Player";
        GameObject spellA3 = Instantiate(iceball, new Vector2(77.5f + Random.Range(-2.5f, 2.5f), 13), Quaternion.identity);
        PlayerIce spellA3Controller = spellA3.GetComponent<PlayerIce>();
        spellA3Controller.enemy = "Player";
            
        if (Mathf.Sign(transform.localScale.x) > 0)
        {
            spellA1.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,270);
            spellA2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 + iceAngle);
            spellA3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 - iceAngle);
        }
        else
        {
            spellA2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90 + iceAngle);
            spellA3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 - iceAngle);
        }
            
        Vector3 spellAScale = spellA1.transform.localScale;
        spellA1.transform.localScale = new Vector3(spellAScale.x * Mathf.Sign(gameObject.transform.localScale.x), spellAScale.y, spellAScale.z);



        GameObject spellB1 = Instantiate(iceball, new Vector2(63f + Random.Range(-2.5f, 2.5f), 13), Quaternion.identity);
        PlayerIce spellB1Controller = spellB1.GetComponent<PlayerIce>();
        spellB1Controller.enemy = "Player";
        GameObject spellB2 = Instantiate(iceball, new Vector2(63f + Random.Range(-2.5f, 2.5f), 13), Quaternion.identity);
        PlayerIce spellB2Controller = spellB2.GetComponent<PlayerIce>();
        spellB2Controller.enemy = "Player";
        GameObject spellB3 = Instantiate(iceball, new Vector2(63f + Random.Range(-2.5f, 2.5f), 13), Quaternion.identity);
        PlayerIce spellB3Controller = spellB3.GetComponent<PlayerIce>();
        spellB3Controller.enemy = "Player";
            
        if (Mathf.Sign(transform.localScale.x) > 0)
        {
            spellB1.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,270);
            spellB2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 + iceAngle);
            spellB3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 - iceAngle);
        }
        else
        {
            spellB2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 90 + iceAngle);
            spellB3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 - iceAngle);
        }
    }

    void fireAttack()
    {
        GameObject spell1 = Instantiate(fireball, new Vector2(74.74f, 3.55f), Quaternion.identity);
    }
}
