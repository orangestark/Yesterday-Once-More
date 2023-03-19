using System.Collections;
using System.Collections.Generic;
using Gamekit2D;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class TimeBack : MonoBehaviour
{   
    // Necessary parameters for timeback
    private Stack<ObjectStage> TimeBackData;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private CharacterController2D cc2D;
    private Rigidbody2D m_Rigidbody2D;
    private ObjectStage LoadStageData = new ObjectStage();
    private bool CheckKeyDown = false;
    private bool isRecording = false;
    private bool isRewinding = false;
    public float maxTime = 10f;
    private float timeRemaining;
    
    // Necessary parameters for timeforward
    private List<ObjectStage> TimeForwardData;
    private List<ObjectStage> TempTimeForwardData;
    private GameObject playerDup;
    private bool isForwarding = false;
    private bool newForwarding = false;
    private SpriteRenderer spriteRendererDup;
    private Rigidbody2D m_Rigidbody2DDup;
    private int forwardCounter = 0;
    [SerializeField] private GameObject prefab;
    [SerializeField] private AudioSource rewindSource;
    [SerializeField] private AudioClip rewindClip;
    [SerializeField] private Image rewindFilter;
    private Color filterColor = Color.black;

    // Start is called before the first frame update
    void Start()
    {
        TimeBackData = new Stack<ObjectStage>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cc2D = GetComponent<CharacterController2D>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        timeRemaining = maxTime;

        TimeForwardData = new List<ObjectStage>();
        TempTimeForwardData = new List<ObjectStage>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check whether player has started or ended the timeback
        CheckKeyDown = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
        if (!(isRewinding) && CheckKeyDown)
        {
            if (isRecording)
            {
                isRecording = false;
                isRewinding = true;
                Destroy(playerDup);
                isForwarding = false;
                timeRemaining = maxTime;
                StartRewindEffect();
                //Debug.Log("End Recording; Start Rewinding");
            }
            else
            {
                isRecording = true;
                //Debug.Log("Start Recording");
            }
        }
        
        //If timeback enabled, update time
        if (isRecording && (timeRemaining > 0))
        {
            timeRemaining -= Time.deltaTime;
        }
        else if (isRecording)
        {
            isRecording = false;
            isRewinding = true;
            Destroy(playerDup);
            isForwarding = false;
            timeRemaining = maxTime;
            StartRewindEffect();
            //Debug.Log("Time's up; End Recording; Start Rewinding");
        }
    }
    
    void FixedUpdate()
    {
        //The main function to record player's data and perform timeback
        if (isRewinding)
        {
            LoadStageData = LoadData();
            if (LoadStageData != null)
            {
                ShowData(LoadStageData);
                filterColor.a = Random.Range(0.65f, 0.8f);
                rewindFilter.color = filterColor;
                //Debug.Log("Rewinding "+ TimeBackData.Count);
            }
            else
            {
                isRewinding = false;
                EndRewindEffect();
                //Debug.Log("End of Rewinding");
                
                newForwarding = true;
                
                animator.enabled = true;
                m_Rigidbody2D.simulated = true;
                
            }
        }
        else if (isRecording)
        {
            SaveData();
        }

        if (newForwarding)
        {
            TimeForwardData = new List<ObjectStage>(TempTimeForwardData);
            TempTimeForwardData.Clear();
            UpdateDuplicatePlayer(TimeForwardData[0].Position);
            //Debug.Log("Player Duplicated");
            forwardCounter = 0;
            isForwarding = true;
            newForwarding = false;
        }
        
        if (isForwarding)
        {
            if (forwardCounter >= TimeForwardData.Count)
            {
                forwardCounter = 0;
            }
            ShowForwardData(TimeForwardData[forwardCounter]);
            ++forwardCounter;
        }
    }
    
    void SaveData()
    {
        ObjectStage stage = new ObjectStage();
        stage.Position = transform.position;
        stage.Sprite = spriteRenderer.sprite;
        stage.IsRight = !(spriteRenderer.flipX);
        stage.Velocity = m_Rigidbody2D.velocity;
        TimeBackData.Push(stage);
        
        TempTimeForwardData.Add(stage);
    }
    
    ObjectStage LoadData()
    {
        if (TimeBackData.Count > 0)
        {
            return TimeBackData.Pop();
        }
        else
        {
            return null;
        }
        
    }
    
    void ShowData(ObjectStage stage)
    {
        animator.enabled = false;
        transform.position = stage.Position;
        spriteRenderer.sprite = stage.Sprite;
        spriteRenderer.flipX = !(stage.IsRight);
        m_Rigidbody2D.simulated = false;
        m_Rigidbody2D.velocity = stage.Velocity;
    }

    void ShowForwardData(ObjectStage stage)
    {
        playerDup.transform.position = stage.Position;
        spriteRendererDup.sprite = stage.Sprite;
        spriteRendererDup.flipX = !(stage.IsRight);
        m_Rigidbody2DDup.velocity = stage.Velocity;
    }
    void UpdateDuplicatePlayer(Vector2 pos)
    {
        //Destroy(playerDup);
        playerDup = Instantiate(prefab, pos, prefab.transform.rotation);
        
        spriteRendererDup = playerDup.GetComponent<SpriteRenderer>();
        spriteRendererDup.color = Color.black;
        m_Rigidbody2DDup = playerDup.GetComponent<Rigidbody2D>();
    }

    void StartRewindEffect()
    {
        rewindSource.clip = rewindClip;
        rewindSource.Play();
        rewindFilter.enabled = true;
    }
    
    void EndRewindEffect()
    {
        rewindSource.Stop();
        rewindFilter.enabled = false;
    }
}
