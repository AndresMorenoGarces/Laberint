using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionSMB_CAballero : StateMachineBehaviour
{
    float m_Damping = 0;
    int m_HashHorizontalParam = Animator.StringToHash("Eje_X");
    int m_HashVerticalParam = Animator.StringToHash("Eje_Y");

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Vector3 knightDiretion = new Vector2(m_HashHorizontalParam, m_HashVerticalParam).normalized;
        animator.SetFloat(m_HashHorizontalParam, /*knightDiretion.x*/0, m_Damping, Time.deltaTime);
        animator.SetFloat(m_HashVerticalParam, /*knightDiretion.y*/1, m_Damping, Time.deltaTime);
    }

}