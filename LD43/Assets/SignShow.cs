using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignShow : MonoBehaviour {

    public bool show = false;

    public GameObject textbox;
    private SpriteRenderer sr_textbox;
    private Vector2 osize_textbox;
    private TextMeshPro text;

    public float lerp = .5f;

	// Use this for initialization
	void Start () {
        sr_textbox = textbox.GetComponent<SpriteRenderer>();
        osize_textbox = sr_textbox.size;
        text = textbox.GetComponentInChildren<TextMeshPro>();
    }
	
	// Update is called once per frame
	void Update () {
		if (!show)
        {
            sr_textbox.size = Vector2.Lerp(sr_textbox.size, new Vector2 (0,0), lerp);
            text.color = Color.Lerp(text.color, new Color (text.color.r, text.color.g, text.color.b, 0), lerp);

        }
        else
        {
            sr_textbox.size = Vector2.Lerp(sr_textbox.size, osize_textbox, lerp);
            text.color = Color.Lerp(text.color, new Color(text.color.r, text.color.g, text.color.b, 1), lerp);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        show = !show;
    }

    private void OnTriggerExit(Collider other)
    {
        show = !show;
    }
}
