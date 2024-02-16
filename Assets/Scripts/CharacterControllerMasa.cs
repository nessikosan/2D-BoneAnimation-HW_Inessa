using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.PSD;
using UnityEngine;

public class CharacterControllerMasa : MonoBehaviour
{
    [SerializeField] private Animator _animator;   
    [SerializeField] private float _moveSpeed;         
    [SerializeField] private KeyCode _attackButton;
    [SerializeField] private int _speedMulti = 2;

    private bool facingRight = true;

    private void Update()
    {
        float inputDir = Input.GetAxis("Horizontal");
     
        _animator.SetFloat("movement", Mathf.Abs(inputDir));

        Vector3 movement = new Vector3(inputDir, 0, 0);
        movement.Normalize();

        transform.position = Vector3.MoveTowards(transform.position, transform.position + movement, Time.deltaTime * _moveSpeed);
       
        float calculatedSpeed = Mathf.Clamp(Mathf.Abs(inputDir), 0, 1);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            calculatedSpeed *= _speedMulti;
            _moveSpeed = Mathf.Clamp(_moveSpeed, 2, _moveSpeed * _speedMulti);
            _animator.SetFloat("movement", calculatedSpeed);
        }

        if (Input.GetKeyDown(_attackButton))
        {    
            _animator.SetTrigger("Attack");
        }
      
        if(facingRight ==false && inputDir > 0)
        {
            Flip();
        }

        if (facingRight == true && inputDir < 0)
        {
            Flip();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
