using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using Gamekit2D;
using TMPro;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{
    public GameObject target;
    public CinemachineVirtualCamera cam;
    public float duration;
    public float FOV;
    private Vector3 _original;
    //private Vector3 _current;
    private float _fovOriginal;
    private Transform _follow;
    private bool _isFirstTime;
    private bool _isWorking;
    private bool _isAvailable = false;
    private bool _goBack = false;
    private bool _arrived = false;
    private int _phase = 0;
    private float _temp;
    
    private GameObject _dieManager;
    private DieRoutine _dieRoutine;

    [SerializeField] private TimeBack timeBack;
    [SerializeField] private GameObject canvas;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject player;
    private Animator animator;
    private Rigidbody2D m_Rigidbody2D;
    
    [SerializeField] private GameObject[] timeBackObjects;
    [SerializeField] private GameObject[] timeBackLiftables;
    private List<Rigidbody2D> objectRigidbody2Ds = new List<Rigidbody2D>();
    private List<Pushable> objectPushables = new List<Pushable>();
    private List<bool> objectRigidbody2DBools = new List<bool>();
    private List<bool> objectPushableBools = new List<bool>();

    private void Start()
    {
        _dieManager = GameObject.Find("DieManager");
        _dieRoutine = _dieManager.GetComponent<DieRoutine>();
        animator = player.GetComponent<Animator>();
        m_Rigidbody2D = player.GetComponent<Rigidbody2D>();
        foreach (var timeBackObject in timeBackObjects)
        {
            objectRigidbody2Ds.Add(timeBackObject.GetComponent<Rigidbody2D>());
            objectPushables.Add(timeBackObject.GetComponent<Pushable>());
        }
    }

    private void Update()
    {
        if (_isFirstTime && !DieRoutine.isDead)
        {
            if (!_isWorking)
            {
                _isWorking = true;
                ++_phase;
                if (_phase == 1)
                {
                    StartCoroutine(LerpFromTo(_original, target.transform.position, duration, 0));
                } 
                else if (_phase == 2)
                {
                    StartCoroutine(LerpFromTo(target.transform.position, _original, duration, 0));
                }
                else
                {
                    _phase = 0;
                    _isFirstTime = false;
                    _isWorking = false;
                    cam.Follow = _follow;
                    PlayerInput.Instance.GainControl();
                    _dieRoutine.StartUnlockRoutine();
                    animator.enabled = true;
                    m_Rigidbody2D.simulated = true;
                    jumpPush.PlayLifting();
                    foreach (var objectPushable in objectPushables)
                    {
                        objectPushable.enabled = objectPushableBools[0];
                        objectPushableBools.RemoveAt(0);
                    }
                    foreach (var objectRigidbody2D in objectRigidbody2Ds)
                    {
                        objectRigidbody2D.simulated = objectRigidbody2DBools[0];
                        objectRigidbody2DBools.RemoveAt(0);
                    }
                    StartSpecialEffect();
                }
            }
        } 
        else if (_isAvailable && (Input.GetKeyDown(KeyCode.M) || Input.GetButtonDown("Y")) && !DieRoutine.isDead && !timeBack.isRewinding)
        {
            if (!_isWorking)
            {
                ++_phase;
                _isWorking = true;
                _original = cam.transform.position;
                cam.Follow = null; 
                PlayerInput.Instance.ReleaseControl(true);
                _dieRoutine.StartLockRoutine();
                animator.enabled = false;
                m_Rigidbody2D.simulated = false;
                jumpPush.PauseLifting();
                foreach (var objectPushable in objectPushables)
                {
                    objectPushableBools.Add(objectPushable.enabled);
                    objectPushable.enabled = false;
                }
                foreach (var objectRigidbody2D in objectRigidbody2Ds)
                {
                    objectRigidbody2DBools.Add(objectRigidbody2D.simulated);
                    objectRigidbody2D.simulated = false;
                }
                StartCoroutine(LerpFromTo(_original, target.transform.position, duration, 1));
            }
        } 
        else if (_isAvailable && (Input.GetKeyUp(KeyCode.M) || Input.GetButtonUp("Y")) && !DieRoutine.isDead && !timeBack.isRewinding && _isWorking)
        {
            _goBack = true;
            if (_phase == 1)
            {
                _phase = 0;
                _arrived = true; 
                StartCoroutine(LerpFromTo(_original, target.transform.position, duration, 2));
            }
        }
    }

    IEnumerator LerpFromTo(Vector3 pos1, Vector3 pos2, float duration, int id)
    {
        //_current = cam.transform.position;
        if (id != 2)
        {
            for (_temp = 0f; _temp < duration; _temp += Time.deltaTime)
            {
                if ((id == 1) && _goBack)
                {
                    yield break;
                }

                cam.transform.position = Vector3.Lerp(pos1, pos2, _temp / duration);
                //var transformPosition = cam.transform.position;
                //transformPosition.x = pos1.x + (pos1.x + pos2.x)/duration*t;
                //transformPosition.y = pos1.y + (pos1.y + pos2.y)/duration*t;
                yield return 0;
            }
        }
        else
        {
            for (; _temp > 0; _temp -= Time.deltaTime)
            {
                cam.transform.position = Vector3.Lerp(pos1, pos2, _temp / duration);
                yield return 0;
            }
        }

        if (_isFirstTime && _phase == 1)
        {
            yield return new WaitForSeconds (2f);
        }
        cam.transform.position = pos2;
        if (_isFirstTime)
            _isWorking = false;
        if (_arrived)
        {
            _arrived = false;
            _goBack = false;
            cam.Follow = _follow;
            _isWorking = false;
            PlayerInput.Instance.GainControl();
            _dieRoutine.StartUnlockRoutine();
            animator.enabled = true;
            m_Rigidbody2D.simulated = true;
            jumpPush.PlayLifting();
            foreach (var objectPushable in objectPushables)
            {
                objectPushable.enabled = objectPushableBools[0];
                objectPushableBools.RemoveAt(0);
            }
            foreach (var objectRigidbody2D in objectRigidbody2Ds)
            {
                objectRigidbody2D.simulated = objectRigidbody2DBools[0];
                objectRigidbody2DBools.RemoveAt(0);
            }
        }
    }
    
    public void CameraEffectSmooth(GameObject targ, float fov, float d)
    {
        target = targ;
        FOV = fov;
        duration = d;
        _follow = cam.Follow;
        _original = cam.transform.position;
        _fovOriginal = cam.m_Lens.FieldOfView;
        cam.Follow = null;
        _isFirstTime = true;
        _isAvailable = true;
        PlayerInput.Instance.ReleaseControl(true);
        _dieRoutine.StartLockRoutine();
        animator.enabled = false;
        m_Rigidbody2D.simulated = false;
        jumpPush.PauseLifting();
        foreach (var objectPushable in objectPushables)
        {
            objectPushableBools.Add(objectPushable.enabled);
            objectPushable.enabled = false;
        }
        foreach (var objectRigidbody2D in objectRigidbody2Ds)
        {
            objectRigidbody2DBools.Add(objectRigidbody2D.simulated);
            objectRigidbody2D.simulated = false;
        }
    }

    public void StartSpecialEffect()
    {
        StartCoroutine(SpecialEffect());
    }

    IEnumerator SpecialEffect()
    {
        if (Controller.usingController)
        {
            text.text = "Hold <font=\"Rajdhani-Bold SDF\">Y</font> to view again";
        }
        else
        {
            text.text = "Hold <font=\"Rajdhani-Bold SDF\">M</font> to view again";
        }
        canvas.SetActive(true); ;
        for(int i = 0; i < 2; i++)
        {
            text.color = new Color32(239, 245, 82, 255);
            for (var t = 0f; t < 1f; t += Time.deltaTime)
            {
                byte a = Convert.ToByte(255 - t * 200);
                text.color = new Color32(239, 245, 82, a);
                yield return 0;
            }

            text.color = new Color32(239, 245, 82, 55);
            
            for (var t = 0f; t < 1f; t += Time.deltaTime)
            {
                byte a = Convert.ToByte(55 + t * 200);
                text.color = new Color32(239, 245, 82, a);
                yield return 0;
            }
        }
        
        text.color = new Color32(239, 245, 82, 255);
        canvas.SetActive(false);
    }
}
