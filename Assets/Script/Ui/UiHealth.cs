using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHealth : MonoBehaviour
{
    [SerializeField] Image imageFill;
    [SerializeField] Vector3 offset;
    private float hp;
    private float maxhp;
    private Transform target;
    void Start()
    {
        
    }

    
    void Update()
    {
        UpdateImageFill();
        UpdatePosTarget();
    }
    public void OnInit(float maxhp , Transform target)
    {
        this.maxhp = maxhp;
        this.target = target;
        hp = maxhp;
        imageFill.fillAmount = 1;
    }
    public void SetNewHp(float hp)
    {
        this.hp = hp;
    }
    public void  UpdateImageFill()
    {
        imageFill.fillAmount = Mathf.Lerp(imageFill.fillAmount, hp / maxhp, Time.deltaTime * 5f);
    }
    public void UpdatePosTarget()
    {
        transform.position = target.position + offset;
    }
}
