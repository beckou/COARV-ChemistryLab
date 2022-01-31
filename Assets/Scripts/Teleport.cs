using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Timers;
using UnityEngine;
using Leap.Unity;
using Valve.VR.InteractionSystem;

public class Teleport : MonoBehaviour
{
    public RiggedHand[] hands; // list of leftHand,rightHand
    public float teleportPointSelectionGracePeriod = 0.2f; // time in seconds to consider a teleportation point as still
                                                           // selected after it was "unselected" (to compensate for Leap
                                                           // Motion errors)
    
    private PinchGesture[] handPinchGestures; // pinching gesture for each hand to trigger teleportation
    private TriangleGesture triangleGesture; // triangle gesture with both hand to enter teleporter selection mode
    
    private Player player; // active steamVR player
    private TeleportPoint[] teleportPoints; // list of steamVR teleportation points
    
    private TeleportPoint activeTeleportPoint; // currently selected teleportation point
    private float clearActiveTeleportPointIn = 0; // timeout in seconds before removing current teleporation point

    public void Awake()
    {
        teleportPoints = GameObject.FindObjectsOfType<TeleportPoint>();
    }

    // Start is called before the first frame update
    public void Start()
    {
        ShowMarkers(false);
        
        // init gestures
        handPinchGestures = new PinchGesture[hands.Length];
        for (var i = 0; i < hands.Length; i++) handPinchGestures[i] = new PinchGesture(hands[i]);

        triangleGesture = new TriangleGesture(hands[0], hands[1]);
        
        // get player
        player = Valve.VR.InteractionSystem.Player.instance;
        if (player == null)
        {
            Debug.LogError("Teleport: No Player instance found in map.");
            Destroy(this.gameObject);
            return;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        var triangle = triangleGesture;
        var pinch = handPinchGestures[0]; // only use one hand for simplicity for now (the gesture is expected to be symmetrical)
        pinch.Update();
        triangle.Update();
        
        // Stop teleport selection mode
        if (triangle.Stop)
            ShowMarkers(false);
        
        // Enter teleport selection mode
        if (triangle.Start)
            ShowMarkers(true);
    
        // In teleport selection mode: select teleporter
        if (triangle.Hold)
        {
            var ray = new Ray(triangle.center, triangle.direction);
            RaycastHit hit;
            
            // Unselect current teleporter
            if (activeTeleportPoint != null)
            {
                if (!activeTeleportPoint.GetComponentInChildren<Collider>().Raycast(ray, out hit, Mathf.Infinity))
                {
                    activeTeleportPoint.Highlight(false);
                    
                    // Start/Update grace period timer
                    if (clearActiveTeleportPointIn == -1)
                        clearActiveTeleportPointIn = teleportPointSelectionGracePeriod;
                    else
                        clearActiveTeleportPointIn -= Time.deltaTime;
                    
                    // Cancel selection after grace period
                    if (clearActiveTeleportPointIn <= 0)
                        activeTeleportPoint = null;
                }
            }
            
            // Find new teleporter to select
            foreach (var teleportMarker in teleportPoints)
            {
                if (teleportMarker.markerActive)
                {
                    if (teleportMarker.GetComponentInChildren<Collider>().Raycast(ray, out hit, Mathf.Infinity))
                    {
                        teleportMarker.Highlight(true);
                        activeTeleportPoint = teleportMarker;
                        clearActiveTeleportPointIn = -1;
                        break;
                    }
                }
            }
        }
        
        // Teleport!
        if (pinch.Start)
        {
            if (activeTeleportPoint != null)
            {
                var teleportPosition = activeTeleportPoint.transform.position;
                var playerFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
                player.trackingOriginTransform.position = teleportPosition + playerFeetOffset;
            }
        }
    }
    
    /*
     * Hide and show teleportations point markers.
     */
    public void ShowMarkers(bool visible)
    {
        foreach (var teleportMarker in teleportPoints)
        {
            if (teleportMarker.markerActive)
            {
                if (!visible)
                    teleportMarker.gameObject.SetActive(false);
                else if (teleportMarker.ShouldActivate(player.feetPositionGuess))
                {
                    teleportMarker.gameObject.SetActive(true);
                    teleportMarker.Highlight(false);
                }
            }
        }
        
    }
}
