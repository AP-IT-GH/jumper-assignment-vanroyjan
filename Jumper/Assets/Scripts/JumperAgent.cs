using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class JumperAgent : Agent
{
    Rigidbody rb;
    public GameObject currentPrefab;
    public TextMeshPro scoreBoard;

    public Vector3 jump;
    public float jumpForce = 2f;
    public bool isGrounded;
    public bool hasTouchedEnemy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 1.2f, 0.0f);
        hasTouchedEnemy = false;
        AddReward(1f);
    }

    // Update is called once per frame
    void Update()
    {
        currentPrefab = GameObject.Find("active");
        scoreBoard.text = GetCumulativeReward().ToString("f4");

        if (GetCumulativeReward() > 5)
        {
            EndEpisode();
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(hasTouchedEnemy);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log(actions.DiscreteActions[0]);
        var vectorAction = actions.DiscreteActions;

        if (vectorAction[0] == 0)
        {
            agentJump();
            AddReward(-0.01f);

        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        
    }

    private void agentJump()
    {
        if (isGrounded)
        {
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionStay()
    {
        isGrounded = true;
    }

    private void OnCollisionExit()
    {
        isGrounded = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            AddReward(-1f);
            Destroy(currentPrefab);
            hasTouchedEnemy = true;
            EndEpisode();
        }

        if (other.transform.CompareTag("Bonus"))
        {
            AddReward(1f);
            Destroy(currentPrefab);
        }
    }
}
