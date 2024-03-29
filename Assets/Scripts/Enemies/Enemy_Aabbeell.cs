﻿using System.Collections;
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

    //Pisicion A de aabbeell
	public float positionAX = -10.5f;
	public float positionAY = -5.51f;
	
	//Pisicion B de aabbeell
	public float positionBX = -0.5f;
	public float positionBY = 5.06f;
	
	//Pisicion C de aabbeell
	public float positionCX = 10.5f;
	public float positionCY = -5.51f;

	//Posicion inicial de ICE 1 vertical
	public float positionXIceVertical1 = 8f;
	public float positionYIceVertical1 = 4.5f;

	//Posicion inicial de ICE 2 vertical
	public float positionXIceVertical2 = -6f;
	public float positionYIceVertical2 = 4.5f;

	//Posicion inicial de Fire
	public float positionXFire = 21.24f;
	public float positionYFire = -4.95f;
	
	//Escudo
	public int shield;

	//Jugador
    GameObject jugador;

	//Transform del enemigo
    Transform enemyTransform;
    
    //Para las animaciones
    private Animator enemyAnimator;
    
    //animacion de Ice
    private static readonly int Ice = Animator.StringToHash("Ice");
    
    //animacion de Fire
    private static readonly int Fire = Animator.StringToHash("Fire");
    
    //animacion de Wind
    private static readonly int Wind = Animator.StringToHash("Wind");
    
    //animacion de Idle
    private static readonly int Idle = Animator.StringToHash("Idle");

    private SFXController sfx;

    // Start is called before the first frame update
    void Start()
    {
		jugador = GameObject.Find("Chawa");
		enemyTransform = GetComponent<Transform> ();
		enemyAnimator = GetComponent<Animator>();

        sfx = GameObject.Find("SFX Controller").GetComponent<SFXController>();

        StartCoroutine(cambiarEstado(2));
    }

    // Update is called once per frame
    void Update()
    {
	    updateLocalScale();
    }

    void updateLocalScale()
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
		updateLocalScale();
		
		// Tipo de escudo
		//shield:
		//         1: fire
		//         2: ice
		//         3: wind
		shield = (int)Random.Range(1, 3.99f);
		switch(shield){
			case 1:
				fireShield();
				break;
			case 2:
				iceShield();
				break;
			case 3:
				windShield();
				break;
			default:
				idleShield();
				break;
				
		}
		
		// Tipo de ataque
		int attack = (int)Random.Range(1, 3.99f);
		switch(attack){
			case 1:
				iceAttackHorizaontal();
                sfx.PlayEnemyAttack(gameObject.transform.position);
				break;
			case 2:
				iceAttackVertical();
                sfx.PlayEnemyAttack(gameObject.transform.position);
                break;
			case 3:
				fireAttack();
                sfx.PlayEnemyAttack(gameObject.transform.position);
				break;
		}

		//Debug.Log("ataque: "+ attack)

		StartCoroutine(cambiarEstado(3));
    }

	void idleShield()
	{
		enemyAnimator.ResetTrigger(Ice);
		enemyAnimator.ResetTrigger(Wind);
		enemyAnimator.ResetTrigger(Fire);
		enemyAnimator.SetTrigger(Idle);
	}

	void iceShield()
	{
		enemyAnimator.ResetTrigger(Idle);
		enemyAnimator.ResetTrigger(Wind);
		enemyAnimator.ResetTrigger(Fire);
		enemyAnimator.SetTrigger(Ice);
	}
	
	void windShield()
	{
		enemyAnimator.ResetTrigger(Ice);
		enemyAnimator.ResetTrigger(Idle);
		enemyAnimator.ResetTrigger(Fire);
		enemyAnimator.SetTrigger(Wind);
	}
	
	void fireShield()
	{
		enemyAnimator.ResetTrigger(Ice);
		enemyAnimator.ResetTrigger(Wind);
		enemyAnimator.ResetTrigger(Idle);
		enemyAnimator.SetTrigger(Fire);
	}

	void positionA(){
		gameObject.transform.localPosition = new Vector2(positionAX, positionAY);
	}

	void positionB(){
		gameObject.transform.localPosition = new Vector2(positionBX, positionBY);
	}

	void positionC(){
		gameObject.transform.localPosition = new Vector2(positionCX, positionCY);
	}

	private float angleAabbeellToChawa(Vector2 chawa, Vector2 aabbeell)
	{
		Vector2 vector = chawa - aabbeell;
		vector.Normalize();
		float angle = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
		return angle;
	}

	void iceAttackHorizaontal()
    {
        nextIceballTime = Time.time + iceballCoolDown;
        var position = attackPos.position;
            
        GameObject spell = Instantiate(iceball, new Vector2(position.x, position.y ), Quaternion.identity);
        PlayerIce spell1Controller = spell.GetComponent<PlayerIce>();
        spell1Controller.enemy = "Player";
        GameObject spell2 = Instantiate(iceball, new Vector2(position.x, position.y ), Quaternion.identity);
        PlayerIce spell2Controller = spell2.GetComponent<PlayerIce>();
        spell2Controller.enemy = "Player";
        GameObject spell3 = Instantiate(iceball, new Vector2(position.x, position.y ), Quaternion.identity);
        PlayerIce spell3Controller = spell3.GetComponent<PlayerIce>();
        spell3Controller.enemy = "Player";

        float angle = angleAabbeellToChawa(GameObject.Find("Chawa").transform.position, position);
        
        spell.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,angle);
        spell2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, angle + iceAngle);
        spell3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, angle - iceAngle);
    }

	void iceAttackVertical()
	{
        nextIceballTime = Time.time + iceballCoolDown;
        var position = attackPos.position;
        
        
        GameObject spellA1 = Instantiate(iceball, new Vector2(positionXIceVertical1 + Random.Range(-2.5f, 2.5f), positionYIceVertical1), Quaternion.identity);
        PlayerIce spellA1Controller = spellA1.GetComponent<PlayerIce>();
        spellA1Controller.enemy = "Player";
        GameObject spellA2 = Instantiate(iceball, new Vector2(positionXIceVertical1 + Random.Range(-2.5f, 2.5f), positionYIceVertical1), Quaternion.identity);
        PlayerIce spellA2Controller = spellA2.GetComponent<PlayerIce>();
        spellA2Controller.enemy = "Player";
        GameObject spellA3 = Instantiate(iceball, new Vector2(positionXIceVertical1 + Random.Range(-2.5f, 2.5f), positionYIceVertical1), Quaternion.identity);
        PlayerIce spellA3Controller = spellA3.GetComponent<PlayerIce>();
        spellA3Controller.enemy = "Player";
        spellA1.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,270);
        spellA2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 + iceAngle);
        spellA3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 - iceAngle);
        


        GameObject spellB1 = Instantiate(iceball, new Vector2(positionXIceVertical2 + Random.Range(-2.5f, 2.5f), positionYIceVertical2), Quaternion.identity);
        PlayerIce spellB1Controller = spellB1.GetComponent<PlayerIce>();
        spellB1Controller.enemy = "Player";
        GameObject spellB2 = Instantiate(iceball, new Vector2(positionXIceVertical2 + Random.Range(-2.5f, 2.5f), positionYIceVertical2), Quaternion.identity);
        PlayerIce spellB2Controller = spellB2.GetComponent<PlayerIce>();
        spellB2Controller.enemy = "Player";
        GameObject spellB3 = Instantiate(iceball, new Vector2(positionXIceVertical2 + Random.Range(-2.5f, 2.5f), positionYIceVertical2), Quaternion.identity);
        PlayerIce spellB3Controller = spellB3.GetComponent<PlayerIce>();
        spellB3Controller.enemy = "Player";
        spellB1.GetComponent<Transform>().rotation = Quaternion.Euler(0,0,270);
        spellB2.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 + iceAngle);
        spellB3.GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 270 - iceAngle);
    }

    void fireAttack()
    {
        GameObject spell1 = Instantiate(fireball, new Vector2(positionXFire, positionYFire), Quaternion.identity);
    }
}
