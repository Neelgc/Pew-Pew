using UnityEngine;

public class PointManager : MonoBehaviour
{
    static PointManager _instance;
    public static PointManager Instance { get { return _instance; } }

    int _points = 0; 


    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
        _points = SavingLocal.Instance.LocalSave.Points;
    }


    public void AddPoint(int point) 
    {
        _points += point; 
        SavingLocal.Instance.LocalSave.Points = _points;
    }
    public void RemovePoint(int point)  => _points -= point;
    public int GetPoints() => _points;
    public bool HaveEnoughPoint(int neededPoint) => _points > neededPoint;
    
}
