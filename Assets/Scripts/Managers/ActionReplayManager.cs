using Assets.Data.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReplayManager : MonoBehaviour
{
    private List<ActionReplayRecord> replayRecords = new List<ActionReplayRecord>();
    private Rigidbody2D objectRigidbody;
    private bool inReplayMode;
    private int currentReplayIndex;
    private float interval = 0.5f;
    private float nextTime = 0;

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            inReplayMode = !inReplayMode;
            Debug.Log("in replayMode " + inReplayMode);
            if (inReplayMode)
            {
                SetTransform(0);
                objectRigidbody.isKinematic = true;
            }
            else
            {
                SetTransform(replayRecords.Count - 1);
                objectRigidbody.isKinematic = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (inReplayMode == false)
        {
            //if (Time.time >= nextTime)
           // {
                SerializableVector2 pos = new SerializableVector2(transform.position.x, transform.position.y);
            SerializableQuaternion rot = new SerializableQuaternion(transform.rotation.x, transform.rotation.y,
                                                                    transform.rotation.z, transform.rotation.w);
            ActionReplayRecord replayRecord = new ActionReplayRecord(pos, rot);

            replayRecords.Add(replayRecord);
                Debug.Log("count now " + replayRecords.Count);

                nextTime += interval;
           // }


        }
        else
        {


            StartCoroutine(ReplayNextRecord());
        }
    }

    private void SetTransform(int index)
    {
        Debug.Log("index " + index);
        Debug.Log("count " + replayRecords.Count);
        currentReplayIndex = index;
        ActionReplayRecord replayRecord = replayRecords[index];

        transform.position = replayRecord.position.GetVector2();
        transform.rotation = replayRecord.rotation.GetQuaternion();
    }

    private IEnumerator ReplayNextRecord()
    {
        yield return new WaitForSeconds(interval);

        int nextIndex = currentReplayIndex + 1;

        if (nextIndex < replayRecords.Count)
        {
            SetTransform(nextIndex);
        }
    }
}
