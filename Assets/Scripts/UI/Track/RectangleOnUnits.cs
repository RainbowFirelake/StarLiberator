using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RectangleOnUnits : MonoBehaviour
{
    [SerializeField]
    private Tracker _trackerPrefab;

    private List<Tracker> _trackers = new();

    public void RegisterNewTrackable(TrackableUnit trackable)
    {
        trackable.OnDisableTracking += UnregisterTrackable;
        SetOrCreateTrackerIfNeed(trackable);
    }

    private void UnregisterTrackable(TrackableUnit trackable)
    {
        trackable.OnDisableTracking -= UnregisterTrackable;
    }

    private void SetOrCreateTrackerIfNeed(TrackableUnit trackable)
    {
        var idleTracker = _trackers.FirstOrDefault(tracker => !tracker.IsBusy);

        if (idleTracker == null)
        {
            idleTracker = Instantiate(_trackerPrefab, this.transform);
        }

        idleTracker.InitTrackable(trackable);
    }
}
