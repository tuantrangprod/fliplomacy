using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloppyTail : MonoBehaviour
{
   public float rotationSpeed;
   private Vector2 direction;

   public float moveSpeed;
   
   //Tail
   public int length;
   public LineRenderer LineRend;
   public Vector3[] segmentPoses;

   public Transform targetDir;
   public float targetDist;

   private void Start()
   {
      LineRend.positionCount = length;
      segmentPoses = new Vector3[length];
   }

   private void Update()
   {
      direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
      Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
      transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

      Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
      transform.position = Vector2.MoveTowards(transform.position, cursorPos, moveSpeed * Time.deltaTime);

      segmentPoses[0] = targetDir.position;

      for (int i = 0; i < segmentPoses.Length; i++)
      {
        // segmentPoses[i] = Vector3.SmoothDamp(segmentPoses[i], segmentPoses[i - 1] + targetDir.right * targetDist);
      }
   }
}
