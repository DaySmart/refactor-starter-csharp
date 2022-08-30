using System;
using System.Collections.Generic;
using System.Text;

namespace Kaizenko.TripService
{
    public class TripDAO
    {
        public static List<Trip> FindTripsByUser(User user)
        {
            throw new DependendClassCallDuringUnitTestException(
                        "TripDAO should not be invoked on an unit test.");
        }
    }
}
