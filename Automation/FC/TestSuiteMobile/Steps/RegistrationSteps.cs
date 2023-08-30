using AppiumCore.Base;
using CommonTestSteps;
using NUnit.Framework;
using System.Threading;
using TechTalk.SpecFlow;
using UIMappings.Screens;
using UIMappings.Screens.Dashboard;
using UIMappings.Screens.Login;
using UIMappings.Screens.Registration;
using Utilities;

namespace TestSuiteMobile.Steps
{
    [Binding]
    public class RegistrationSteps
    {
        [When(@"Enter random email address")]
        public void WhenEnterRandomEmail()
        {
            EmailServiceTests.ObtainRandomEmailAddress();
            ScreenFactory.Instance.CurrentPage.As<LoginScreen>().EnterEmailAddress(TestInMemoryParametersShared.Instance.GeneratedEmailAddress);
        }

        [Then(@"Registration screen appears")]
        public void ThenRegistrationScreenAppears()
        {
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("RegistrationScreen");
            Assert.True(ScreenFactory.Instance.CurrentPage.As<RegistrationScreen>().IsRegistrationScreenDisplayed(), "Registration screen is not shown");
        }

        [When(@"First name (.*) is entered")]
        public void WhenFirstNameFranchiIsEntered(string firstName)
        {
            ScreenFactory.Instance.CurrentPage.As<RegistrationScreen>().EnterFirstName(firstName);
        }

        [When(@"Last name (.*) is entered")]
        public void WhenLastNameTestIsEntered(string lastName)
        {
            ScreenFactory.Instance.CurrentPage.As<RegistrationScreen>().EnterLastName(lastName);
        }

        [When(@"Date of birth (.*) is entered")]
        public void WhenDateOfBirthIsEntered(string dateOfBirth)
        {
            ScreenFactory.Instance.CurrentPage.As<RegistrationScreen>().EnterDateOfBirth(dateOfBirth);
        }

        [When(@"Phone number (.*) is entered")]
        public void WhenPhoneNumberIsEntered(string phoneNumber)
        {
            ScreenFactory.Instance.CurrentPage.As<RegistrationScreen>().EnterPhoneNumber(phoneNumber);
        }

        [When(@"Terms and Conditions are agreed")]
        public void WhenTermsAndConditionsAreAgreed()
        {
            ScreenFactory.Instance.CurrentPage.As<RegistrationScreen>().AgreeWithTermsAndsConditions();
            System.Threading.Thread.Sleep(1000);
        }

        [When(@"Profile picture is added")]
        public void WhenProfilePictureIsAdded()
        {
            ScreenFactory.Instance.CurrentPage.As<RegistrationScreen>().TakeAProfilePicture();
        }

        [When(@"Next button is clicked")]
        public void WhenNextButtonIsClicked()
        {
            ScreenFactory.Instance.CurrentPage.As<RegistrationScreen>().ClickNextButton();
            System.Threading.Thread.Sleep(5000);
        }

        [Then(@"Find the gyms near you screen appears")]
        public void ThenFindTheGymsNearYouScreenAppears()
        {
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("FindTheGymsNearYou");
            Assert.True(ScreenFactory.Instance.CurrentPage.As<FindTheGymsNearYou>().IsFindTheGymsNearYouScreenDisplayed(), "Find The Gyms Near You screen is not shown");
        }

        [Then(@"Use Current Location link is clicked")]
        public void ThenUseCurrentLocationLinkIsClicked()
        {
            Thread.Sleep(2000);
            ScreenFactory.Instance.CurrentPage.As<FindTheGymsNearYou>().ClickUseCurrentLocationLink();
            Thread.Sleep(5000);
        }

        [Then(@"Custom location (.*) is entered")]
        public void ThenCustomLocationPearlandIsEntered(string location)
        {
            ScreenFactory.Instance.CurrentPage.As<FindTheGymsNearYou>().EnterCustomLocation(location);
            System.Threading.Thread.Sleep(5000);
        }

        [Then(@"Allow access to the device's location while using the app")]
        public void ThenAllowAccessToTheDevicesLocationWhileUsingTheApp()
        {
            ScreenFactory.Instance.CurrentPage.As<FindTheGymsNearYou>().ClickAllowWhileUsingTheApp();
            System.Threading.Thread.Sleep(2000);
        }


        [Then(@"First available gym is selected")]
        public void ThenFirstAvailableGymIsSelected()
        {
            ScreenFactory.Instance.CurrentPage.As<FindTheGymsNearYou>().ClickFirstGymNear();
        }

        [Then(@"Continue button is clicked")]
        public void ThenContinueButtonIsClicked()
        {
            ScreenFactory.Instance.CurrentPage.As<FindTheGymsNearYou>().ClickContinueButton();
            System.Threading.Thread.Sleep(5000);
        }


        [Then(@"Verify Your Email Address screen appears")]
        public void ThenVerifyYourEmailAddressScreenAppears()
        {
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("VerifyEmail");
            Assert.True(ScreenFactory.Instance.CurrentPage.As<VerifyEmail>().IsVerifyEmailScreenDisplayed(), "Verify Email screen is not shown");
        }

        [When(@"Send Verification Code button is clicked")]
        public void WhenSendVerificationCodeButtonIsClicked()
        {
            ScreenFactory.Instance.CurrentPage.As<VerifyEmail>().ClickSendVerificationCodeButton();
        }

        [When(@"Verification code is received via email")]
        public void WhenVerificationCodeIsReceivedViaEmail()
        {
            EmailServiceTests.GetVerificationCode();
        }

        [When(@"Email Verification Code is entered")]
        public void WhenEmailVerificationCodeIsEntered()
        {
            ScreenFactory.Instance.CurrentPage.As<VerifyEmail>().EnterVerificationCode(TestInMemoryParametersShared.Instance.VerificationCode);
        }

        [When(@"Verify Code button is clicked")]
        public void WhenVerifyCodeButtonIsClicked()
        {
            ScreenFactory.Instance.CurrentPage.As<VerifyEmail>().ClickVerifyCodeButton();
            Thread.Sleep(3000);
        }

        [Then(@"Sms verification is successfull")]
        public void ThenSmsVerificationIsSuccessfull()
        {
            //var text = ScreenFactory.Instance.CurrentPage.As<MultiFactorAuthentication>().SuccessIndicatorLabel.Text;

        }


        [Then(@"Email Verification is successfull")]
        public void ThenEmailVerificationIsSuccessfull()
        {
            var textOutcome = ScreenFactory.Instance.CurrentPage.As<VerifyEmail>().SuccessIndicatorLabel.Text;
            ScreenFactory.Instance.CurrentPage.As<VerifyEmail>().ClickContinueButton();
            System.Threading.Thread.Sleep(3000);
        }


        [When(@"Multi-factor authentication screen appears")]
        public void WhenMultiFactorAuthenticationScreenAppears()
        {
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("MultiFactorAuthentication");
            Assert.True(ScreenFactory.Instance.CurrentPage.As<MultiFactorAuthentication>().IsMultiFactorAuthenticationScreenDisplayed(), "Multi Factor Authentication screen is not shown");
        }

        [When(@"Country Code is chosen")]
        public void WhenCountryCodeIsChosen()
        {
            //ScreenFactory.Instance.CurrentPage.As<MultiFactorAuthentication>().SelectUSCountryCode();
        }

        [When(@"Country Code for (.*) is chosen")]
        public void WhenSpecificCountryCodeIsChosen(string country)
        {
            ScreenFactory.Instance.CurrentPage.As<RegistrationScreen>().SelectSpecificCountryCode(country);
        }

        [When(@"Send Code button is clicked")]
        public void WhenSendCodeButtonIsClicked()
        {
            ScreenFactory.Instance.CurrentPage.As<MultiFactorAuthentication>().ClickSendCodeButton();
        }

        [Then(@"Sms verification code is retrieved")]
        public void ThenSmsVerificationCodeIsRetrieved()
        {
            Thread.Sleep(20000);
            ScreenFactory.Instance.CurrentPage.As<MultiFactorAuthentication>().RetrieveSmsVerificationCode();
        }

        [When(@"Sms verification code is entered")]
        public void WhenSmsVerificationCodeIsEntered()
        {
            ScreenFactory.Instance.CurrentPage.As<MultiFactorAuthentication>().EnterVerificationCode(TestInMemoryParametersShared.Instance.SmsVerificationCode);
        }

        [When(@"Verify SMS Code button is clicked")]
        public void WhenVerifySMSCodeButtonIsClicked()
        {
            ScreenFactory.Instance.CurrentPage.As<MultiFactorAuthentication>().ClickVerifyCodeButton();
            Thread.Sleep(3000);
        }

        [Then(@"Create Password screen appears")]
        public void ThenCreatePasswordScreenAppears()
        {
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("CreatePassword");
            Assert.True(ScreenFactory.Instance.CurrentPage.As<CreatePassword>().IsCreatePasswordScreenDisplayed(), "Create Password screen is not shown");
        }

        [When(@"New password is entered")]
        public void WhenNewPasswordIsEntered()
        {
            ScreenFactory.Instance.CurrentPage.As<CreatePassword>().EnterNewPassword("FranchiCzar!");
        }

        [When(@"New password is confirmed")]
        public void WhenNewPasswordIsConfirmed()
        {
            ScreenFactory.Instance.CurrentPage.As<CreatePassword>().EnterConfirmNewPassword("FranchiCzar!");
        }

        [When(@"Continue button is clicked")]
        public void WhenContinueButtonIsClicked()
        {
            ScreenFactory.Instance.CurrentPage.As<CreatePassword>().ClickContinueButton();
        }


        [Then(@"Congratulations screen appears")]
        public void ThenCongratulationsScreenAppears()
        {
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("Congratulations");
            Assert.True(ScreenFactory.Instance.CurrentPage.As<Congratulations>().IsCongratulationsScreenDisplayed(), "Congratulations screen is not shown");
        }

        [When(@"Continue link is clicked")]
        public void WhenContinueLinkIsClicked()
        {
            ScreenFactory.Instance.CurrentPage.As<Congratulations>().ClickContinueLink();
        }

        [Then(@"Ready to workout screen appears")]
        public void ThenReadyToWorkoutScreenAppears()
        {
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("ReadyToWorkout");
            Assert.True(ScreenFactory.Instance.CurrentPage.As<ReadyToWorkout>().IsReadyToWorkoutScreenDisplayed(), "Ready To Workout screen is not shown");
        }

        [When(@"Let's go button is clicked")]
        public void WhenLetsGoButtonIsClicked()
        {
            ScreenFactory.Instance.CurrentPage.As<ReadyToWorkout>().ClickLetsGoButton();
        }


        [Then(@"Select your membership screen appears")]
        public void ThenSelectYourMembershipScreenAppears()
        {
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("SelectYourMembership");
            Assert.True(ScreenFactory.Instance.CurrentPage.As<SelectYourMembership>().IsSelectYourMembershipScreenDisplayed(), "Select Your Membership screen is not shown");
        }

        [When(@"First available membership is selected")]
        public void WhenFirstAvailableMembershipIsSelected()
        {
            ScreenFactory.Instance.CurrentPage.As<SelectYourMembership>().ClickSelectButton();
        }

        [Then(@"Payment method screen is shown")]
        public void ThenPaymentMethodScreenIsShown()
        {
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("PaymentMethod");
            Assert.True(ScreenFactory.Instance.CurrentPage.As<PaymentMethod>().IsPaymentMethodScreenDisplayed(), "Payment Method screen is not shown");
        }

        [Then(@"Add members screen is shown")]
        public void ThenAddMembersScreenIsShown()
        {
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("AddMembers");
            Assert.True(ScreenFactory.Instance.CurrentPage.As<AddMembers>().IsAddMembersScreenDisplayed(), "Add Members screen is not shown");
        }

        [When(@"Skip this step link is clicked")]
        public void WhenSkipThisStepLinkIsClicked()
        {
            ScreenFactory.Instance.CurrentPage.As<AddMembers>().ClickSkipThisStepLink();
        }

        [When(@"Add members button is clicked")]
        public void WhenAddMembersButtonIsClicked()
        {
            ScreenFactory.Instance.CurrentPage.As<AddMembers>().ClickAddMembersButton();
        }

        [Then(@"Add new member screen appears")]
        public void ThenAddNewMemberScreenAppears()
        {
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("AddNewMember");
            Assert.True(ScreenFactory.Instance.CurrentPage.As<AddNewMember>().IsAddNewMemberScreenDisplayed(), "Add New Member screen is not shown");
        }

        [When(@"Credit card data is entered")]
        public void WhenCreditCardDataIsEntered()
        {
            ScreenFactory.Instance.CurrentPage.As<PaymentMethod>()
                .EnterCardNumber("4242424242424242")
                .EnterExpirationDate("1224")
                .EnterCardholder("Franchi Test")
                .EnterCvcCvvNumber("456")
                .ClickContinueButton();
        }

        [Then(@"Is this correct screen appears")]
        public void ThenIsThisCorrectScreenAppears()
        {
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("IsThisCorrect");
            Assert.True(ScreenFactory.Instance.CurrentPage.As<IsThisCorrect>().IsIsThisCorrectScreenDisplayed(), "Is This Correct screen is not shown");
        }

        [When(@"Pay now button is clicked")]
        public void WhenPayNowButtonIsClicked()
        {
            ScreenFactory.Instance.CurrentPage.As<IsThisCorrect>().ClickPayNowButton();
        }

        [Then(@"Welcome to screen appears")]
        public void ThenWelcomeToScreenAppears()
        {
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("WelcomeTo");
            Assert.True(ScreenFactory.Instance.CurrentPage.As<WelcomeTo>().IsWelcomeToScreenDisplayed(), "Is Welcome To screen is not shown");
        }

        [When(@"Go to home screen link is clicked")]
        public void WhenGoToHomeScreenLinkIsClicked()
        {
            ScreenFactory.Instance.CurrentPage.As<WelcomeTo>().ClickGoToHomeScreenLink();
        }


        [Then(@"Home Screen appears")]
        public void ThenHomeScreenAppears()
        {
            CommonSteps.MinimizeUpdateVersionScreen();

            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("HomeScreen");
            ScreenFactory.Instance.CurrentPage.As<Home>().ClickWhileUsingTheAppButton();
            Assert.True(ScreenFactory.Instance.CurrentPage.As<Home>().IsHomeScreenDisplayed(), "Home screen is not shown");
        }

        [Then(@"Doors are shown")]
        public void ThenDoorsAreShown()
        {
        }

        [When(@"First available door is clicked")]
        public void WhenFirstAvailableDoorIsClicked()
        {
        }

        [Then(@"The door is unlocked")]
        public void ThenTheDoorIsUnlocked()
        {
        }

        [When(@"Log out")]
        public void WhenLogOut()
        {
            ScreenFactory.Instance.CurrentPage.As<Home>().ClickMoreButton();
            ScreenFactory.Instance.CurrentPage = ScreenUtilities.GetScreen("MoreScreen");
            ScreenFactory.Instance.CurrentPage.As<More>().ClickSignOutButton();
        }

    }
}
