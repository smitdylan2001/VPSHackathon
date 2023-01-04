using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] LayerMask _mask;
    [SerializeField] float _mailMoveSpeed = 1, _mailDistance = 0.7f;
    [SerializeField] Vector3 _mailOffset;

    private Mail _heldMail;
    private Rigidbody _mailRb;
    private Camera _mainCam;
    private Vector3 _touchPos;
    private Vector3 _mailVelocity;
    private bool _hasTouched;

    private void Start()
    {
        _mainCam = Camera.main;
    }

    void Update()
    {
        GetTouchPos();
        if(_hasTouched) GetMail();

        if (_heldMail) MoveMail();
    }

    void GetTouchPos()
    {
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _touchPos = touch.position;
                _hasTouched = true;

                return;
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            _touchPos = Input.mousePosition;
            _hasTouched = true;

            return;
        }

        _hasTouched = false;
    }

    void GetMail()
    {
        Ray ray = _mainCam.ScreenPointToRay(_touchPos);
        RaycastHit hitInfo;
        if (!Physics.Raycast(ray, out hitInfo, 50f, _mask))
        {
            return;
        }

        var mailBox = hitInfo.collider.GetComponent<MailBox>();
        if (!mailBox)
        {
            return;
        }

        if (!mailBox.IsFull)
        {
            //Give warning box is full

            return;
        }

        if (_heldMail)
        {
            //Give warning hand is full

            return;
        }

        _heldMail = mailBox.EmptyBox();

        if (!_heldMail) return;

        mailBox.GetComponent<MeshRenderer>().material.color = Color.white;
        _heldMail.transform.position = hitInfo.point;
        _heldMail.transform.LookAt(gameObject.transform);
        _heldMail.GetComponent<MeshRenderer>().enabled = true;
        _mailRb = _heldMail.GetComponent<Rigidbody>();
    }

    void MoveMail()
    {
        _mailRb.MovePosition(Vector3.SmoothDamp(_mailRb.position, ((transform.forward * _mailDistance) + transform.position) + _mailOffset, ref _mailVelocity, _mailMoveSpeed));
        _heldMail.transform.LookAt(gameObject.transform);
    }
}