using CoreServices;
using IOTests.Utils;
using NUnit.Framework;
using System;

namespace IOTests.TestSteps
{
    public class SharedSteps
    {
        public void VerifyStatusCode(int expectedStatus)
        {
            var statusCode = (int)RestManager.Instance.GetResponse().StatusCode;
            Assert.That(statusCode, Is.EqualTo(expectedStatus));
        }

        public static void CheckIfValuesAreReturnedAsExpected(object coinDataObject)
        {
            try
            {
                // Check all of the returned values for missing data
                Assert.That(HelperFunctions.IsObjectPropertyValueMissing(coinDataObject), Is.Not.EqualTo(true), "Some coin values were not returned!");
            }
            catch (Exception exc)
            {
                throw new Exception("Failed converting response to expected values: " + exc.Message);
            }
        }
    }
}
