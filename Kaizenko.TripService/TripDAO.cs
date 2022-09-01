using System.Collections.Generic;

namespace Kaizenko.TripService
{
    public class TripDAO
    {
        public static List<Trip> FindTripsByUser(User user)
        {
            throw new DependendClassCallDuringUnitTestException(
                        "TripDAO should not be invoked on an unit test.");
        }

        public virtual List<Trip> GetTripsByUser(User user)
        {
            return FindTripsByUser(user);
        }
    }
}
