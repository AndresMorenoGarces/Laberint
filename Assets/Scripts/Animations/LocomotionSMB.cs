using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocomotionSMB : StateMachineBehaviour
{
    float m_Damping = -10f;
    readonly int m_HashHorizontalParam = Animator.StringToHash("Horizontal");
    readonly int m_HashVerticalParam = Animator.StringToHash("Vertical");

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(horizontal, vertical).normalized;
        animator.SetFloat(m_HashHorizontalParam, input.x, m_Damping, Time.deltaTime);
        animator.SetFloat(m_HashVerticalParam, input.y, m_Damping, Time.deltaTime);

    }
}
