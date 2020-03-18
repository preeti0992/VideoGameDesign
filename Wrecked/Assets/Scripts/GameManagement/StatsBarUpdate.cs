using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsBarUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    // public GameObject player;
    GameManager GM;
    // PlayerController pc;
    // Text xpText, hpText;
    Text nameText;

    public SimpleHealthBar xpBar1;
    public SimpleHealthBar xpBar2;
    public SimpleHealthBar hpBar1;
    public SimpleHealthBar hpBar2;

    void Start()
    {
        // pc= player.GetComponent<PlayerController>();
        GM = GameManager.Instance;
        nameText = this.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        // nameText.text =  "Burdell";
        nameText.text =  GM.playerName;
        // healthBar = (SimpleHealthBar)this.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject;
        // xpText = this.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        // hpText = this.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("GM.playerHP : " + GM.playerHP);
    	// xpText.text = "XP : " + GM.playerXP.ToString ();
    	// hpText.text = "HP : " + GM.playerHP.ToString ();

        xpBar1.UpdateBar( GM.playerXP, GameManager.MAXEXP );
        xpBar2.UpdateBar( GM.playerXP, GameManager.MAXEXP );
        hpBar1.UpdateBar( GM.playerHP, GameManager.MAXHEALTH );
        hpBar2.UpdateBar( GM.playerHP, GameManager.MAXHEALTH );
    }
}
