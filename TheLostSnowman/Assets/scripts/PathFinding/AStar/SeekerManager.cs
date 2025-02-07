using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;
using System.Threading.Tasks;
using UnityEngine.UIElements;
using System.Threading;

public class SeekerManager : MonoBehaviour
{
    static private SeekerManager instance =  null;
    private const int maxSeekers = 1000;   // pool mérete
    [SerializeField] private GameObject seeker;
    [SerializeField] private List<GameObject> spawnables = new List<GameObject>();
    public static SeekerManager Instance { get => instance ; private set => instance = value; }
    private ISeeker[] seekers = new ISeeker[maxSeekers];
    private SeekerData[] seekerDatas = new SeekerData[maxSeekers];
    [SerializeField] private Transform target;
    private List<int> free2 = new List<int>();
    [SerializeField] private int deadSeekers = 0;
    [SerializeField] private int currentSeekers = 0;
    private Quaternion rotation;
    private int iterations = 0;

    private  int Iterations
    {
        get
        {
            return iterations;
        }
       set
        {
            if (value > maxSeekers-1)
            {
                iterations = 0;
            }
            else
            {
                iterations = value;
            }
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            rotation = this.transform.rotation;
        }
        else
        {
            Debug.LogWarning("multiple seeker managers, destroying self");
            Destroy(this);
        }
    }
    
    private void Start()
    {
        // object pooling   "objektum medencézés xdddd"
        for (int i = 0; i < maxSeekers; i++)
        {
            if (i % 2 == 0)
            {
                seeker = spawnables[0];
            }
            else
            {
                seeker = spawnables[1];
            }
            GameObject go = Instantiate(seeker, this.transform.position, this.transform.rotation);
            go.name = ("seeker" + i).ToString();
            seekers[i] = go.GetComponent<ISeeker>();
            seekerDatas[i] = new SeekerData(go.transform, target, seekers[i]);
            seekers[i].SetTarget(target);
            seekers[i].SetActive(false);
        }
        
    }
    
    public void AddSeeker(Vector3 posisiton, int spawnableNum)
    {     // kiveszünk egyet a pool-ból
        int seekerIndex = -10;
        int startNum;
        if (spawnableNum == 0)
        {
            startNum = 0;
        }
        else
        {
            startNum = 1;
        }
        for (int i = startNum; i < maxSeekers; i+=2)
        {
            if (!seekers[i].IsActive())
            {
                seekerIndex = i;
            }
        }
        if (seekerIndex == -10)
        {
            return;
        }
        seekers[seekerIndex].SetActive(true);
        seekerDatas[seekerIndex].TeleportToPosition(posisiton);
    }

    public void DestroySeeker(ISeeker seeker)
    {
     // vissza tesszük a pool-ba   
        for (int i = 0; i < maxSeekers; i++)
        {
            if (seekers[i].Equals(seeker))
            {
                seekers[i].SetActive(false);
                //UIManager.Instance.AddPoint();
                return;
            }
        }  
    }

    public void TargetPathChange()
    {
        for (int i = 0; i < maxSeekers; i++)
        {
            seekerDatas[i].requestPath = true;
        } 
    }

    private void Update()
    { 
        for (int i = 0; i < maxSeekers; i++)
        {
            if (seekers[i].IsActive())
            {
                seekers[i].Poll();
            }
        }
        
        for (int i = 0; i < 100; i++)
        {
            if (seekers[Iterations].IsActive())
            {
                seekerDatas[Iterations].Update();
            }
            Iterations++;
        }
    }
}
