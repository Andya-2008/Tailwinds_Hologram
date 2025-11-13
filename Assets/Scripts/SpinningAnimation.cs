using UnityEngine;

public class SpinningAnimation : MonoBehaviour
{
    public bool spin;
    public bool inAnimation;
    [SerializeField] float spinSpeed;
    [SerializeField] float liftSpeed;
    [SerializeField] Transform scalar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(inAnimation)
        {
            transform.Translate(0, liftSpeed * Time.deltaTime * 100 * scalar.localScale.x, 0);
        }
        if (spin)
        {
            Spin();
        }
    }

    public void Spin()
    {
        transform.Rotate(0, spinSpeed * 100 * Time.deltaTime, 0);
    }
}
