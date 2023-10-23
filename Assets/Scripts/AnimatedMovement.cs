using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimatedMovement : MonoBehaviour
{
    public float rotationDegree = 13f;
    public float rotationOffset = 3f;
    public float rotateTimer;
    public float rotateTimerOffset;
    public bool moving = false;
    private Vector3 initScale;
    public AudioSource footsteps;
    
    public void Awake() {
        initScale = transform.localScale;
        rotateTimer += Random.Range(-rotateTimerOffset, rotateTimerOffset);

        // footsteps = AudioManager.Instance.Sounds[Footsteps];
    }
    public void Move() {
        if (moving) return;
        moving = true;
        Turn();

        // footsteps.Play();
        // footsteps.pitch = Random.Range(0f, 2f);
        // footsteps.time = Random.Range(0f, 1f);
    }
    public void Stop() {
        if (!moving) return;
        moving = false;
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, initScale, rotateTimer*0.3f).setEaseOutQuad();
        LeanTween.moveLocalY(gameObject, 0f, rotateTimer*0.3f);
        LeanTween.rotateLocal(gameObject, Vector3.zero, rotateTimer*0.3f).setEaseOutQuad();

        footsteps.Stop();
    }

    public void Turn(int right=1) {
        //LeanTween.value(gameObject, (float val)=>{transform.localScale=new Vector3(initScale.x,val,initScale.z);},transform.localScale.y,initScale.y*0.85f, 0.1f).setLoopPingPong(2).setEaseOutExpo();
        LeanTween.scaleY(gameObject, initScale.y*0.85f, rotateTimer*0.3f).setLoopPingPong(1);
        LeanTween.moveLocalY(gameObject, 0.2f, rotateTimer*0.4f).setLoopPingPong(1);
        //LeanTween.rotateLocal(gameObject, new Vector3(0f,0f,rotationDegree*right+Random.Range(-rotationOffset, rotationOffset)), rotateTimer).setEaseOutQuad().setOnComplete(()=>Turn(-right));
        LeanTween.rotateZ(gameObject, rotationDegree*right+Random.Range(-rotationOffset, rotationOffset), rotateTimer).setEaseOutQuad().setOnComplete(()=>Turn(-right));
    }

    public void Flip() {
        LeanTween.value(gameObject, (float val)=>{transform.localScale=new Vector3(val, transform.localScale.y, transform.localScale.z);}, initScale.x, 0f, 0.1f).setEaseInQuad().setLoopPingPong(1);
        /*LeanTween.scaleX(gameObject, 0f, 0.15f).setEaseInQuad().setOnComplete(()=>{
            LeanTween.scaleX(gameObject, initScale.x, 0.035f).setEaseInQuad();
        });*/
    }
}
