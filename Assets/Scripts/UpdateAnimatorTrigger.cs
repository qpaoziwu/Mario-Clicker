using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class UpdateAnimatorTrigger : MonoBehaviour
{
    public Sprite thisSprite;
    public Image targetImage;
    public Animator thisAnimator;
    public Animator targetAnimator;

    public TMP_Text TitleText;

    void UpdateTitleText()
    {
        TitleText.text = thisSprite.name;

    }

    void UpdateTargetImage()
    {
        if  (thisSprite||targetImage == null) 
        
        { 
            Debug.Log(this.name + " UpdateTargetImage() failed");


        }
        
        targetImage.sprite = thisSprite;
        UpdateTitleText();
    }
    void TriggerTargetAnimatorSelected()
    {
        if  (thisAnimator||targetAnimator == null) 

        { 
            Debug.Log(this.name + " TriggerTargetAnimatorSelected() failed");


        }
        
        targetAnimator.SetBool("Selected", true);
    }

    void TriggerTargetAnimatorDisabled()
    {
        if  (thisAnimator||targetAnimator == null) 

        { 
            Debug.Log(this.name + " TriggerTargetAnimatorDisabled() failed");


        }
        
        targetAnimator.SetBool("Disabled", true);
    }

        void TriggerTargetAnimatorNormal()
    {
        if  (thisAnimator||targetAnimator == null) 

        { 
            Debug.Log(this.name + " TriggerTargetAnimatorNormal() failed");


        }
        
        targetAnimator.SetBool("Normal", true);
    }
        void TriggerTargetAnimatorHighlighted()
    {
        if  (thisAnimator||targetAnimator == null) 

        { 
            Debug.Log(this.name + " TriggerTargetAnimatorHighlighted() failed");


        }
        
        targetAnimator.SetBool("Disabled", true);
    }
        void TriggerTargetAnimatorPressed()
    {
        if  (thisAnimator||targetAnimator == null) 

        { 
            Debug.Log(this.name + " TriggerTargetAnimatorPressed() failed");


        }
        
        targetAnimator.SetBool("Pressed", true);
    }
    void TriggerTargetAnimatorWithString(string state)
    {
        targetAnimator.SetBool(state, true);
    }

}