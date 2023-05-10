using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gamekit2D;
using UnityEngine;

public class TimeBackObject : MonoBehaviour
{
    private static bool pause;
    
    private Stack<ObjectStage> TimeBackData;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D m_Rigidbody2D;
    private ObjectStage LoadStageData = new ObjectStage();
    private bool CheckKeyDown = false;
    private bool isRecording = false;
    private bool isRewinding = false;
    public float maxTime = 10f;
    private float timeRemaining;
    private bool hasBeenMoved = false;
    
    public List<ObjectStage> TimeForwardData;
    public bool isForwarding = false;
    private bool isFreezing = false;
    private bool goHome = false;
    public int forwardCounter = 0;

    private Pushable _pushable;

    private ObjectStage _initStage = new ObjectStage();

    private GameObject _player;
    private TimeBack _timeBack;

    // Start is called before the first frame update
    void Start()
    {
        TimeBackData = new Stack<ObjectStage>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        timeRemaining = maxTime;
        
        TimeForwardData = new List<ObjectStage>();

        _pushable = GetComponent<Pushable>();
        
        _initStage.Position = transform.position;
        _initStage.Sprite = spriteRenderer.sprite;
        _initStage.IsRight = !(spriteRenderer.flipX);
        _initStage.Velocity = m_Rigidbody2D.velocity;
        
        _player = GameObject.Find("Ellen");
        _timeBack = _player.GetComponent<TimeBack>();
    }

    // Update is called once per frame
    void Update()
    {
        //Check whether player has started or ended the timeback
        //CheckKeyDown = PlayerInput.Instance.Rewind1.Down || PlayerInput.Instance.Rewind2.Down;
        CheckKeyDown = Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift);
        if (!(isRewinding) && CheckKeyDown && !(DieRoutine.isDead || DieRoutine.isLocked))
        {
            if (isFreezing)
            {
                goHome = true;
                isFreezing = false;
                isForwarding = false;
                timeRemaining = maxTime;
            }
            else if (isForwarding)
            {
                isFreezing = true;
            }
            else if (isRecording)
            {
                if (TimeForwardData.Any())
                {
                    isRecording = false;
                    isRewinding = true;
                    timeRemaining = maxTime;
                    Debug.Log("End Recording; Start Rewinding");
                }
            }
            else if (!_timeBack.isRewinding)
            {
                isRecording = true;
                Debug.Log("Start Recording");
            }
        }
        
        //If timeback enabled, update time
        if ((isRecording || isFreezing) && (timeRemaining > 0) && !(DieRoutine.isDead || DieRoutine.isLocked))
        {
            if (!pause)
            {
                timeRemaining -= Time.deltaTime;
            }
        }
        else if (isFreezing && !(DieRoutine.isDead || DieRoutine.isLocked))
        {
            goHome = true;
            isFreezing = false;
            isForwarding = false;
            timeRemaining = maxTime;
        }
        else if (isRecording && !(DieRoutine.isDead || DieRoutine.isLocked))
        {
            isRecording = false;
            isRewinding = true;
            timeRemaining = maxTime;
            Debug.Log("Time's up; End Recording; Start Rewinding");
        }
    }
    
    void FixedUpdate()
    {
        if (!pause)
        {
            //The main function to record player's data and perform timeback
            if (isRewinding)
            {
                if (!hasBeenMoved)
                {
                    isRewinding = false;
                    TimeBackData.Clear();
                    TimeForwardData.Clear();
                }
                else
                {
                    LoadStageData = LoadData();
                    if (LoadStageData != null)
                    {
                        ShowData(LoadStageData);
                        Debug.Log("Rewinding " + TimeBackData.Count);
                    }
                    else
                    {
                        isRewinding = false;
                        hasBeenMoved = false;
                        //m_Rigidbody2D.simulated = true;
                        _pushable.enabled = false;

                        isForwarding = true;
                        Debug.Log("End of Rewinding");
                    }
                }
            }
            else if (isRecording)
            {
                SaveData();
            }

            if (goHome)
            {
                ShowData(TimeForwardData[0]);
                TimeForwardData.Clear();
                forwardCounter = 0;
                _pushable.enabled = true;
                goHome = false;
            }

            if (isForwarding)
            {
                if (forwardCounter >= TimeForwardData.Count)
                {
                    forwardCounter = 0;
                }

                ShowData(TimeForwardData[forwardCounter]);
                //Debug.Log(TimeForwardData[forwardCounter].Position);
                ++forwardCounter;
            }
        }
    }
    
    void SaveData()
    {
        ObjectStage old;
        ObjectStage stage = new ObjectStage();
        stage.Position = transform.position;
        stage.Sprite = spriteRenderer.sprite;
        stage.IsRight = !(spriteRenderer.flipX);
        stage.Velocity = m_Rigidbody2D.velocity;

        if (!hasBeenMoved && TimeBackData.Count > 0)
        {
            old = TimeBackData.Peek();
            if ((old.Position != stage.Position) && ((Math.Abs(old.Position.y - stage.Position.y) > 0.01) || (Math.Abs(old.Position.x - stage.Position.x) > 0.01)))
            {
                hasBeenMoved = true;
                //Debug.Log("WHY1");
            }
        }
        
        TimeBackData.Push(stage);
        
        TimeForwardData.Add(stage);
    }
    
    ObjectStage LoadData()
    {
        if (TimeBackData.Count > 0)
        {
            return (ObjectStage)TimeBackData.Pop();
        }
        else
        {
            return null;
        }
    }
    
    void ShowData(ObjectStage stage)
    {
        transform.position = stage.Position;
        spriteRenderer.sprite = stage.Sprite;
        spriteRenderer.flipX = !(stage.IsRight);
        //m_Rigidbody2D.simulated = false;
        m_Rigidbody2D.velocity = stage.Velocity;
    }
    
    public void Reset()
    {
        isRecording = false;
        isRewinding = false;
        timeRemaining = maxTime;
        TimeBackData.Clear();
        hasBeenMoved = false;
        goHome = false;
        isFreezing = false;
    }
    
    public void Restart()
    {
        isForwarding = false;
        TimeForwardData.Clear();
        forwardCounter = 0;
        isFreezing = false;
        hasBeenMoved = false;
        goHome = false;
        ShowData(_initStage);
        _pushable.enabled = true;
    }
    
    public static void PauseRewind()
    {
        pause = true;
    }

    public static void PlayRewind()
    {
        pause = false;
    }
}
