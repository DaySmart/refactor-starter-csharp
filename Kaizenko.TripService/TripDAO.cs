using System.Collections.Generic;

namespace Kaizenko.TripService
{
    public class TripDAO
    {
        public virtual List<Trip> FindTripsByUser(User user)
        {
            throw new DependendClassCallDuringUnitTestException(
                        "TripDAO should not be invoked on an unit test.");
        }
    }
}
