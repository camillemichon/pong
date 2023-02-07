using UnityEngine;

public class Paddles : MonoBehaviour
{
    public float unitspersec = 100;
    public GameObject paddleleft;
    public GameObject paddleright;
    private void FixedUpdate()
    {
        var axleft = Input.GetAxis("left");
        var axright = Input.GetAxis("right");

        Vector3 forceleft = Vector3.left * unitspersec * axleft * Time.deltaTime;
        Vector3 forceright = Vector3.left * unitspersec * axright * Time.deltaTime;

        paddleleft.GetComponent<Rigidbody>().velocity = forceleft;
        paddleright.GetComponent<Rigidbody>().velocity = forceright;
    }
}
