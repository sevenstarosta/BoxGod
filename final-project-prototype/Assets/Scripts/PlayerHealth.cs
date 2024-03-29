﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Animator anim;
    public bool isDead;
    bool damaged;
    PlayerController playerController;

    public AudioClip hurt;
    private float lowPitchRange = .75F;
    private float highPitchRange = 1.5F;

    // Use this for initialization
    void Awake () {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false; 

		if (playerController.transform.position.y < -6.5f)
			isDead = true;

	}

    public void TakeDamage(int amount)
    {

        damaged = true;
        currentHealth -= amount;
        healthSlider.value = currentHealth;
        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        gameObject.GetComponent<Renderer>().material.color = Color.gray;
        playerController.enabled = false;
    }
}
