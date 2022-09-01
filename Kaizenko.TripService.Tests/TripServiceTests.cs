using ApprovalTests.Reporters;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace Kaizenko.TripService.Tests
{
    public class TripServiceTests
    {
        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void GetTripsByUser_Legacy()
        {
            using (StringWriter sw = new StringWriter())
            {
                TripService tripService;
                User loggedInUser = new User();
                User friendOfLoggedInUser = new User();
                friendOfLoggedInUser.AddFriend(loggedInUser);

                // User not logged in
                tripService = new TripService(new TripDAOTester(null));
                try
                {
                    _ = tripService.GetTripsByUser(new User(), null);
                }
                catch (Exception ex)
                {
                    sw.WriteLine(ex.ToString());
                }

                // User logged in, no friends
                tripService = new TripService(null);
                List<Trip> list = tripService.GetTripsByUser(new User(), new User());
                sw.WriteLine((list != null && list.Count > 0 ? String.Join<Trip>(",", list.ToArray()) : "No Trips"));

                // User logged in, is friend with no trips
                tripService = new TripService(new TripDAOTester(new List<Trip>()));
                list = tripService.GetTripsByUser(friendOfLoggedInUser, loggedInUser);
                sw.WriteLine((list != null && list.Count > 0 ? String.Join<Trip>(",", list.ToArray()) : "No Trips"));

                // User logged in, is friend with one trip
                tripService = new TripService(new TripDAOTester(new List<Trip>() { new Trip() }));
                list = tripService.GetTripsByUser(friendOfLoggedInUser, loggedInUser);
                sw.WriteLine((list != null && list.Count > 0 ? String.Join<Trip>(",", list.ToArray()) : "No Trips"));

                // User logged in, is friend with multiple trips
                tripService = new TripService(new TripDAOTester(new List<Trip>() { new Trip(), new Trip() }));
                list = tripService.GetTripsByUser(friendOfLoggedInUser, loggedInUser);
                sw.WriteLine((list != null && list.Count > 0 ? String.Join<Trip>(",", list.ToArray()) : "No Trips"));

                ApprovalTests.Approvals.Verify(sw);
            }
        }

        private class TripDAOTester : TripDAO
        {
            private List<Trip> _trips;
            public TripDAOTester(List<Trip> trips)
            {
                _trips = trips;
            }

            public override List<Trip> GetTripsByUser(User user)
            {
                return _trips;
            }
        }
    }
}
