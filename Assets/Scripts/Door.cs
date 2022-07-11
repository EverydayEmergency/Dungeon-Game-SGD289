using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isOpen = false;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Collider[] colliders = Physics.OverlapSphere(transform.position, .01f);

        foreach (Collider collider in colliders)
        {            
            if (collider.tag == "Door")
            {
                gameObject.SetActive(false);
                return;
            }
        }

        GetComponent<MeshCollider>().enabled = true;
    }

    public void OnTriggerEnter(Collider collider)
    {     
        if (collider.tag == "Player")
        {
            DoorInteract();
        }
    }

    public void DoorInteract()
    {
        isOpen = !isOpen;

        Vector3 doorTransformDirection = transform.TransformDirection(Vector3.forward);
        Vector3 playerTransformDirection = GameManager.gm.player.transform.position - transform.position;
        float dot = Vector3.Dot(doorTransformDirection, playerTransformDirection);

        anim.SetFloat("dot", dot);
        anim.SetBool("isOpen", isOpen);

        StartCoroutine(AutoClose());
    }

    private IEnumerator AutoClose()
    {
        while (isOpen)
        {
            yield return new WaitForSeconds(3);
            if(Vector3.Distance(transform.position, GameManager.gm.player.transform.position) > 3)
            {
                isOpen = false;
                anim.SetFloat("dot", 0);
                anim.SetBool("isOpen", isOpen);
            }
        }
    }


}
