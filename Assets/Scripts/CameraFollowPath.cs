using UnityEngine;
using TMPro;
using System.Collections;

public class CameraFollowPath : MonoBehaviour {

  [SerializeField] private AnimationCurve _animationCurve; 
  [SerializeField] private TextMeshProUGUI TimerText;
  [SerializeField] private bool _Move_ = false; 
  private bool Move = false; 

  public float speed = 0f;
  public float waitFor = 5f; 
  public Transform pathParent;
  Transform targetPoint;
  int index;

  void OnDrawGizmos()
  {
    Vector3 from;
    Vector3 to;
    for (int a=0; a<pathParent.childCount; a++)
    {
      from = pathParent.GetChild(a).position;
      to = pathParent.GetChild((a+1) % pathParent.childCount).position;
      Gizmos.color = new Color (1, 0, 0);
      Gizmos.DrawLine (from, to);
    }
  }
  void Start () {
    index = 0;
    targetPoint = pathParent.GetChild (index);
  }

  // Update is called once per frame
  void FixedUpdate () {
    if(waitFor <= 0)
    {
      disableTimer(); 
      if(_Move_ && Move && !PlayerConfigurationManager.Instance.isPaused){
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, _animationCurve.Evaluate(speed));
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f) 
        {
          index++;
          index %= pathParent.childCount;
          targetPoint = pathParent.GetChild (index);
          speed++; 
        }
      }
    }
    else{
      waitFor -= Time.deltaTime; 
      updateTimer(waitFor); 
    }

  }

  public void disableTimer(){
    TimerText.text = "Run! You must to survive."; 
    Move = true;
  }

  public void updateTimer(float time_){
    if (TimerText) TimerText.enabled=true;
    time_ += 1;
    float minutes = Mathf.FloorToInt(time_ / 60); 
    float seconds = Mathf.FloorToInt(time_ % 60); 

    TimerText.text = string.Format("Steady: {00}", seconds);
  }
}
