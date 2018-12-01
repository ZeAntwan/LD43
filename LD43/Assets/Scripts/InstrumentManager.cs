using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstrumentManager : MonoBehaviour {

    public bool showDrum = false;

    public GameObject lh;
    public GameObject rh;
    public GameObject drum;

    public float angleVariation = 60f;
    public float lerpValue = .5f;
    public float showOffset = 300f;
    public float showLerp = .5f;

    private Vector3 baseLhRot;
    private Vector3 baseRhRot;
    private Vector3 drumBasePosition;

    private float targetLRot;
    private float targetRRot;

    // Use this for initialization
    void Start () {
        baseLhRot = lh.transform.rotation.eulerAngles;
        baseRhRot = rh.transform.rotation.eulerAngles;
        drumBasePosition = drum.transform.position;

        ShowDrum(showDrum);
    }
	
	// Update is called once per frame
	void Update () {
        
        ShowDrum(showDrum);
        PlayInstrument();
        
	}

    private void PlayInstrument()
    {
        int leftNote = GetCurrentNote(Input.GetAxis("LT"));
        int rightNote = GetCurrentNote(Input.GetAxis("RT"));

        targetLRot = Mathf.Lerp(targetLRot, baseLhRot.z+(leftNote * -angleVariation), lerpValue);
        lh.transform.eulerAngles = new Vector3(0, 0, targetLRot);

        targetRRot = Mathf.Lerp(targetRRot, baseRhRot.z + (rightNote * angleVariation), lerpValue);
        rh.transform.eulerAngles = new Vector3(0, 0, targetRRot);

        
    }

    private int GetCurrentNote(float axis)
    {
        if (axis > .8f)
        {
            return 2;
        }
        else if (axis >= .3f && axis <= .8f)
        {
            return 1;
        } else
        {
            return 0;
        }
    }

    private void ShowDrum(bool show)
    {
        if (show == true)
        {
            drum.transform.position = Vector3.Lerp(drum.transform.position, new Vector3 (drumBasePosition.x, drumBasePosition.y, drumBasePosition.z), showLerp);
        }
        
        if (show == false)
        {
            drum.transform.position = Vector3.Lerp(drum.transform.position, new Vector3(drumBasePosition.x, drumBasePosition.y - showOffset, drumBasePosition.z), showLerp);
        }

    }

    private void PlayNote(int note)
    {

    }
}
