using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstrumentManager : MonoBehaviour
{

    public bool showDrum = false;

    public GameObject lh;
    public GameObject rh;
    public GameObject drum;

    public AudioClip[] notesL;
    public AudioClip[] notesR;

    public List<int> melody;
    public List<string> availableMelodies;
    public float melodyWaitTime = 5f;
    private Coroutine melodyTiming = null;

    private AudioSource speakerL;
    private AudioSource speakerR;

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
    private void Awake()
    {
        speakerL = gameObject.AddComponent<AudioSource>();
        speakerR = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        baseLhRot = lh.transform.rotation.eulerAngles;
        baseRhRot = rh.transform.rotation.eulerAngles;
        drumBasePosition = drum.transform.position;

        showOffset = Screen.height;

        ShowDrum(showDrum);
    }

    // Update is called once per frame
    void Update()
    {

        ShowDrum(showDrum);
        if (Input.GetAxis("DpadY") == 1)
        {
            showDrum = true;
        } else if (Input.GetAxis("DpadY") == -1)
        {
            showDrum = false;
        }

    }

    private void ShowDrum(bool show)
    {
        if (show == true)
        {
            drum.transform.position = Vector3.Lerp(drum.transform.position, new Vector3(drumBasePosition.x, drumBasePosition.y, drumBasePosition.z), showLerp);
            PlayInstrument();
        }

        if (show == false)
        {
            drum.transform.position = Vector3.Lerp(drum.transform.position, new Vector3(drumBasePosition.x, drumBasePosition.y - showOffset, drumBasePosition.z), showLerp);
        }

    }

    private void PlayInstrument()
    {
        int leftNote = GetCurrentNote(Input.GetAxis("Vertical"));
        int rightNote = GetCurrentNote(Input.GetAxis("VerticalR"));

        targetLRot = Mathf.Lerp(targetLRot, baseLhRot.z + (leftNote * -angleVariation), lerpValue);
        lh.transform.eulerAngles = new Vector3(0, 0, targetLRot);

        targetRRot = Mathf.Lerp(targetRRot, baseRhRot.z + (rightNote * angleVariation), lerpValue);
        rh.transform.eulerAngles = new Vector3(0, 0, targetRRot);

        if (Input.GetButtonDown("Y"))
        {
            PlayNote(leftNote, notesL, speakerL);
        }

        if (Input.GetButtonDown("B"))
        {
            PlayNote(rightNote, notesR, speakerR);
        }

    }

    private int GetCurrentNote(float axis)
    {
        if (axis >= .5f)
        {
            return 2;
        }
        else if (axis <= -.5f)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    private void PlayNote(int note, AudioClip[] notes, AudioSource speaker)
    {
        speaker.clip = notes[note];
        speaker.Play();

        if(melody.Count <= 5)
        {
            melody.Add(note);
        }
        else
        {
            if(CheckMelody(melody))
            {
                Debug.Log("RealMelody");
                melody.Clear();
            }
            else
            {
                melody.Clear();
                melody.Add(note);
            }
            
        }

        if (melodyTiming == null)
        {
            melodyTiming = StartCoroutine(MelodyTiming());
        }
        else
        {
            StopCoroutine(melodyTiming);
            melodyTiming = StartCoroutine(MelodyTiming());
        }

    }

    IEnumerator MelodyTiming()
    {
        yield return new WaitForSeconds(melodyWaitTime);
        melody.Clear();
    }

    private bool CheckMelody(List<int> list)
    {
        List<string> mString = melody.ConvertAll<string>(x => x.ToString());
        string convertedString = string.Join("", mString.ToArray());
        Debug.Log(convertedString);
        if (availableMelodies.Any(c => c == convertedString))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
