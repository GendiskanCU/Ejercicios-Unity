using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacle1 : MonoBehaviour
{
    public float rotationSpeed = 40f;
    public float translateSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.left * translateSpeed * Time.deltaTime);
        //Hacemos que se mueva en su posición local, ya que en ella no cambia de posición el vector left al ir rotando
        //Con translate sin embargo lo moveríamos con respecto a la posición global, y en ella sí que va cambiando
        //el left cuando rota, lo que provoca que no siempre vaya hacia donde queremos
        transform.localPosition += Vector3.left * translateSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
