using UnityEngine;

public class FlowerAnimations : MonoBehaviour, IRayEventReceiver
{
    public Animator flowerAnimator; // Reference to the flower animator
    private bool isBeingWatched = false; // Track if the flower is being watched

    private void Update()
    {
        if (isBeingWatched)
        {
            flowerAnimator.SetBool("seen", true);
            flowerAnimator.SetBool("lookaway", false);
        }
        else
        {
            flowerAnimator.SetBool("seen", false);
            flowerAnimator.SetBool("lookaway", true);
        }
    }

    // Methods for raycast events
    public void OnRaycastEnter()
    {
        isBeingWatched = true;
    }

    public void OnRaycastExit()
    {
        isBeingWatched = false;
    }
}
