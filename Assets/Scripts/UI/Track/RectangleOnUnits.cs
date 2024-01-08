using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RectangleOnUnits : MonoBehaviour
{
    [SerializeField]
    private Tracker _trackerPrefab;

    private readonly List<Tracker> _trackers = new();

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
        var idleTracker = _trackers.FirstOrDefault(tracker => tracker.IsFree);

        if (idleTracker == null)
        {
            idleTracker = Instantiate(_trackerPrefab, this.transform);
            _trackers.Add(idleTracker);
        }

        idleTracker.InitTrackable(trackable);
    }
}
