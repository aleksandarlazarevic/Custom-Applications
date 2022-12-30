using NUnit.Framework;
using RestSharp;
using System.Net;
using TechTalk.SpecFlow;
using Utilities;

namespace TestSuiteApi.Steps
{
    [Binding]
    public class BookingSteps
    {
        private RestResponse? _response;

        [Then(@"Get all bookings")]
        public void ThenGetAllBookings()
        {
            _response = RestApiManager.GetData("booking");
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK, "No bookings available");
        }

        [Then(@"Get booking with (.*)")]
        public void ThenGetBookingWith(string id)
        {
            _response = RestApiManager.GetData("booking/" + id);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK, string.Format("No bookings with id: {0} available", id));
        }

        [Then(@"Create a booking with (.*) (.*) (.*) (.*)(.*)(.*) (.*)")]
        public void ThenCreateABooking(string firstname, string lastname, string totalprice, string depositpaid,
                                       string checkin, string checkout, string additionalneeds)
        {
            string BookingInfo = string.Empty;
            _response = RestApiManager.PostData("booking", BookingInfo);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK, "Booking not created");
        }

        [Then(@"Update a booking with (.*) (.*) (.*) (.*) (.*)(.*)(.*) (.*)")]
        public void ThenUpdateABooking(string id, string firstname, string lastname, string totalprice, string depositpaid,
                                       string checkin, string checkout, string additionalneeds)
        {
            object BookingInfo = new object();
            _response = RestApiManager.PutData("booking", id, BookingInfo);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK, "Booking not updated");
        }

        [Then(@"Partially update a booking with (.*) (.*) (.*)")]
        public void ThenPartiallyUpdateABookingWithJimBrown(string id, string firstname, string lastname)
        {
            object BookingInfo = new object();
            _response = RestApiManager.PatchData("booking", id, BookingInfo);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.OK, "Unable to partially update booking");
        }

        [Then(@"Delete booking with (.*)")]
        public void ThenDeleteBookingWith(string id)
        {
            _response = RestApiManager.DeleteData("booking", id);
            Assert.AreEqual(_response.StatusCode, HttpStatusCode.Created, "Unable to delete booking");
        }

    }
}
