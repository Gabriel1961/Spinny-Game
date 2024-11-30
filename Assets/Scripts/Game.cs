using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] Image stick;
    [SerializeField] Image area;

    [SerializeField] float areaStartAngle = 0;
    [SerializeField] float areaWidthInDeg = 30;

    [SerializeField] float spinSpeed = 10;


    bool IsAngleBetween(float angle, float angleStart, float angleEnd)
    {
        // normalizeaza unghiurile intre [0, 360)
        angle = NormalizeAngle(angle);
        angleStart = NormalizeAngle(angleStart);
        angleEnd = NormalizeAngle(angleEnd);

        // Verifica cele doua cazuri cand unghiul trece de 360
        if (angleStart <= angleEnd)
        {
            return angle >= angleStart && angle <= angleEnd;
        }
        else
        {
            return angle >= angleStart || angle <= angleEnd;
        }
    }

    float NormalizeAngle(float angle)
    {
        angle %= 360f;
        return angle < 0 ? angle + 360f : angle;
    }

    void Start()
    {
        
    }

    void Update()
    {
        area.fillAmount = areaWidthInDeg / 360;
        // offsetul de 90 de grade este necesar pentru ca aria rosie sa inceapa din cadranul I al cercului geometric cand area start angle este 0
        area.transform.rotation = Quaternion.Euler(0, 0, areaStartAngle + 90);  
 
        stick.transform.Rotate(new Vector3(0, 0, spinSpeed * Time.deltaTime));

        float areaEndAngle = areaStartAngle + areaWidthInDeg;

        // offset de 90 de grade pentru ca stickul este in scena la 90 de grade 
        float stickRotation = stick.transform.eulerAngles.z + 90; 


        // daca apesi space in aria rosie  
        if(Input.GetKeyDown(KeyCode.Space) && IsAngleBetween(stickRotation, areaStartAngle, areaEndAngle))
        {
            spinSpeed = -spinSpeed * 1.1f;
            areaWidthInDeg *= .9f;
            areaStartAngle += 120;
            return; 
        }

        if(spinSpeed > 0) // daca merge in sensul initial 
        {
            // aria de moarte este de 10 grade 
            if (IsAngleBetween(stickRotation, areaEndAngle, areaEndAngle + 10)) 
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
        } 
        else // daca merge in sens contrar 
        {
            // aria de moarte este de 10 grade 
            if (IsAngleBetween(stickRotation, areaStartAngle - 10, areaStartAngle))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
