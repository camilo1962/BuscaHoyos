//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class PlayerMovement : MonoBehaviour
//{
//public float duration = 0.3f;
//Vector3 scale;
//
//public bool isRotating = false;
//float directionX = 0;
//float directionZ = 0;
//
//float startAngleRad = 0;
//Vector3 startPos;
//float rotationTime = 0;
//float radius = 1;
//Quaternion preRotation;
//Quaternion postRotation;
//
//public bool isGrounded = false;
//void Start()
//{
//    scale = transform.lossyScale;
//    this.transform.GetComponent<Rigidbody>().velocity = new Vector3(0, -50, 0);
//}


//void Update()
//{
//    if (!isRotating & isGrounded)
//    {
//        float x = Input.GetAxisRaw("Horizontal");
//        float y = 0;
//
//        if (x == 0)
//            y = Input.GetAxisRaw("Vertical");
//
//        if ((x != 0 || y != 0) && !isRotating)
//        {
//            directionX = y;
//            directionZ = x;
//            startPos = transform.position;
//            preRotation = transform.rotation;
//            transform.Rotate(directionZ * 90, 0, directionX * 90, Space.World);
//            postRotation = transform.rotation;
//            transform.rotation = preRotation;
//            SetRadius();
//            rotationTime = 0;
//            isRotating = true;
//            GameManager.moves += 1;
//        }
//    }
//    else
//    {
//        this.transform.position += new Vector3(0, 0.1f, 0);
//        this.transform.position -= new Vector3(0, 0.1f, 0);
//    }
//}
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float duration = 0.3f;
    Vector3 scale;

    public bool isRotating = false;
    float directionX = 0;
    float directionZ = 0;

    float startAngleRad = 0;
    Vector3 startPos;
    float rotationTime = 0;
    float radius = 1;
    Quaternion preRotation;
    Quaternion postRotation;

    public bool isGrounded = false;
    private GameManager gameManager;

    void Start()
    {
        scale = transform.lossyScale;
        this.transform.GetComponent<Rigidbody>().velocity = new Vector3(0, -50, 0);
    }

    void Update()
    {
        if (!isRotating && isGrounded)
        {
            float xInput = Input.GetAxisRaw("Horizontal");
            float zInput = Input.GetAxisRaw("Vertical");

            if (xInput != 0 || zInput != 0)
            {
                Vector3 moveDirection = new Vector3(xInput, 0, zInput).normalized;
                directionX = moveDirection.z;
                directionZ = -moveDirection.x;
                startPos = transform.position;
                preRotation = transform.rotation;
                transform.Rotate(directionZ * 90, 0, directionX * 90, Space.World);
                postRotation = transform.rotation;
                transform.rotation = preRotation;
                SetRadius();
                rotationTime = 0;
                isRotating = true;
                GameManager.moves += 1;
            }
        }
    }

    void FixedUpdate()
    {
        if (isRotating)
        {
            rotationTime += Time.fixedDeltaTime;
            float ratio = Mathf.Lerp(0, 1, rotationTime / duration);

            float rotAng = Mathf.Lerp(0, Mathf.PI / 2f, ratio);
            float distanceX = -directionX * radius * (Mathf.Cos(startAngleRad) - Mathf.Cos(startAngleRad + rotAng));
            float distanceY = radius * (Mathf.Sin(startAngleRad + rotAng) - Mathf.Sin(startAngleRad));
            float distanceZ = directionZ * radius * (Mathf.Cos(startAngleRad) - Mathf.Cos(startAngleRad + rotAng));
            transform.position = new Vector3(startPos.x + distanceX, startPos.y + distanceY, startPos.z + distanceZ);

            transform.rotation = Quaternion.Lerp(preRotation, postRotation, ratio);

            if (ratio == 1)
            {
                isRotating = false;
                directionX = 0;
                directionZ = 0;
                rotationTime = 0;
            }
        }
    }
    public void MoveUp()
    {
        // Lógica para mover hacia arriba
        if (!isRotating)
        {
            directionX = 1;
            directionZ = 0;
            StartRotation();
        }
    }

    public void MoveDown()
    {
        // Lógica para mover hacia abajo
        if (!isRotating)
        {
            directionX = -1;
            directionZ = 0;
            StartRotation();
        }
    }

    public void MoveRight()
    {
        // Lógica para mover hacia la derecha
        if (!isRotating)
        {
            directionX = 0;
            directionZ = 1;
            StartRotation();
        }
    }

    public void MoveLeft()
    {
        // Lógica para mover hacia la izquierda
        if (!isRotating)
        {
            directionX = 0;
            directionZ = -1;
            StartRotation();
        }
    }

    void StartRotation()
    {
        // Iniciar la rotación del jugador
        startPos = transform.position;
        preRotation = transform.rotation;
        transform.Rotate(directionZ * 90, 0, directionX * 90, Space.World);
        postRotation = transform.rotation;
        transform.rotation = preRotation;
        SetRadius();
        rotationTime = 0;
        isRotating = true;
        GameManager.moves += 1;
    }

    void SetRadius()
    {
        Vector3 dirVec = new Vector3(0, 0, 0);
        Vector3 nomVec = Vector3.up;

        if (directionX != 0)
            dirVec = Vector3.right;
        else if (directionZ != 0)
            dirVec = Vector3.forward;

        if (Mathf.Abs(Vector3.Dot(transform.right, dirVec)) > 0.99)
        {                       // La dirección del movimiento es la misma que x del objeto.
            if (Mathf.Abs(Vector3.Dot(transform.up, nomVec)) > 0.99)
            {                   // El eje y de global es el mismo que el y del objeto.
                radius = Mathf.Sqrt(Mathf.Pow(scale.x / 2f, 2f) + Mathf.Pow(scale.y / 2f, 2f));
                startAngleRad = Mathf.Atan2(scale.y, scale.x);
            }
            else if (Mathf.Abs(Vector3.Dot(transform.forward, nomVec)) > 0.99)
            {       //  El eje y de global es el mismo que el z del objeto.
                radius = Mathf.Sqrt(Mathf.Pow(scale.x / 2f, 2f) + Mathf.Pow(scale.z / 2f, 2f));
                startAngleRad = Mathf.Atan2(scale.z, scale.x);
            }

        }
        else if (Mathf.Abs(Vector3.Dot(transform.up, dirVec)) > 0.99)
        {                   // La dirección de movimiento es la misma que y del objeto.
            if (Mathf.Abs(Vector3.Dot(transform.right, nomVec)) > 0.99)
            {                   // y de global es lo mismo que x de objeto
                radius = Mathf.Sqrt(Mathf.Pow(scale.y / 2f, 2f) + Mathf.Pow(scale.x / 2f, 2f));
                startAngleRad = Mathf.Atan2(scale.x, scale.y);
            }
            else if (Mathf.Abs(Vector3.Dot(transform.forward, nomVec)) > 0.99)
            {       // El eje y de global es el mismo que el z del objeto.
                radius = Mathf.Sqrt(Mathf.Pow(scale.y / 2f, 2f) + Mathf.Pow(scale.z / 2f, 2f));
                startAngleRad = Mathf.Atan2(scale.z, scale.y);
            }
        }
        else if (Mathf.Abs(Vector3.Dot(transform.forward, dirVec)) > 0.99)
        {           // La dirección de movimiento es la misma que z del objeto.
            if (Mathf.Abs(Vector3.Dot(transform.right, nomVec)) > 0.99)
            {                   // y de global es lo mismo que x de objeto
                radius = Mathf.Sqrt(Mathf.Pow(scale.z / 2f, 2f) + Mathf.Pow(scale.x / 2f, 2f));
                startAngleRad = Mathf.Atan2(scale.x, scale.z);
            }
            else if (Mathf.Abs(Vector3.Dot(transform.up, nomVec)) > 0.99)
            {               // El eje y de global es el mismo que el y del objeto.
                radius = Mathf.Sqrt(Mathf.Pow(scale.z / 2f, 2f) + Mathf.Pow(scale.y / 2f, 2f));
                startAngleRad = Mathf.Atan2(scale.y, scale.z);
            }
        }
    }
    // Este método verifica si el jugador golpeó el suelo y permite el movimiento si lo hizo.
    void OnCollisionEnter(Collision theCollision)
    {
        //Debug.Log("Enter: " + theCollision.transform.tag);

        if (theCollision.gameObject.tag == "Tile")
            isGrounded = true;
        if(theCollision.gameObject.tag == "frame")
        {
            isGrounded = false;
            gameManager.repetir.gameObject.SetActive(true);
        }
    }
    
}






