using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float strength;
    [SerializeField] GameObject VFX_Hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * strength);

        //Debug.Log(transform.forward);
        //Debug.Log(strength);


        StartCoroutine(DeleteAfterSecond());
    }

    IEnumerator DeleteAfterSecond(float duration = 3f)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if(collision.gameObject.tag == "Target")
        {
            Instantiate(VFX_Hit, transform.position, Quaternion.identity,null);
            AudioManager.Instance.PlaySFX("Hit_Enemy");
            Destroy(collision.gameObject);
            Destroy(gameObject);
            GameManager.Instance.CheckWin();
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Environement"))
        {
            Instantiate(VFX_Hit, transform.position, Quaternion.identity, null);
            AudioManager.Instance.PlaySFX("Hit_Sand");
            Destroy(gameObject);
        }
    }

}
