using Unity.MLAgents;
using UnityEngine;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class CubeAgent : Agent
{
    // Start is called once before the first execution of Update after the Agent is created
    private Rigidbody rBody;

    public float forceMultiplier = 20f;
    public Transform Target;

    void Start()
    {
        rBody = this.GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
       // If the Agent fell, zero its momentum
        if (this.transform.localPosition.y < 0)
        {
            this.rBody.angularVelocity = Vector3.zero;
            this.rBody.linearVelocity = Vector3.zero;
            this.transform.localPosition = new Vector3( 0, 1.5f, 0);
        }

        // Move the target to a new spot
        Target.localPosition = new Vector3(Random.value * 8 - 4, 1.5f, Random.value * 8 - 4);
        Target.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        Target.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // Target and Agent positions
        sensor.AddObservation(Target.localPosition);
        sensor.AddObservation(this.transform.localPosition);

        // Agent velocity
        sensor.AddObservation(rBody.linearVelocity.x);
        sensor.AddObservation(rBody.linearVelocity.z);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Actions, size = 2
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        rBody.AddForce(controlSignal * forceMultiplier);

        // Rewards
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, Target.localPosition);
        forceMultiplier = 20f;

        // Reached target
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }

        // Fell off platform
        else if (this.transform.localPosition.y < 0)
        {
            EndEpisode();
        }

        else if (Target.transform.localPosition.y < 0)
        {
            EndEpisode();
        }
    }
}