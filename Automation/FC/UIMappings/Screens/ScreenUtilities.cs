using AppiumCore.Base;
using UIMappings.Screens.Dashboard;
using UIMappings.Screens.Login;
using UIMappings.Screens.Registration;

namespace UIMappings.Screens
{
    public class ScreenUtilities
    {
        public static BaseScreen GetScreen(string screen)
        {
            var returnScreen = new BaseScreen();
            switch (screen)
            {
                #region Dashboard screens
                case "HomeScreen":
                    returnScreen = new Home();
                    break;
                case "AboutThisApp":
                    returnScreen = new AboutThisApp();
                    break;
                case "AccountAndProfile":
                    returnScreen = new AccountAndProfile();
                    break;
                case "Help":
                    returnScreen = new Help();
                    break;
                case "MoreScreen":
                    returnScreen = new More();
                    break;
                case "Subscriptions":
                    returnScreen = new Subscriptions();
                    break;
                #endregion
                #region Login screens
                case "LoginScreen":
                    returnScreen = new LoginScreen();
                    break;
                case "MicrosoftLogin":
                    returnScreen = new MicrosoftLogin();
                    break;
                case "SupportRequest":
                    returnScreen = new SupportRequest();
                    break;
                #endregion
                #region Registration screens
                case "RegistrationScreen":
                    returnScreen = new RegistrationScreen();
                    break;
                case "CreatePassword":
                    returnScreen = new CreatePassword();
                    break;
                case "FindTheGymsNearYou":
                    returnScreen = new FindTheGymsNearYou();
                    break;
                case "MultiFactorAuthentication":
                    returnScreen = new MultiFactorAuthentication();
                    break;
                case "Congratulations":
                    returnScreen = new Congratulations();
                    break;
                case "PaymentMethod":
                    returnScreen = new PaymentMethod();
                    break;
                case "IsThisCorrect":
                    returnScreen = new IsThisCorrect();
                    break;
                case "WelcomeTo":
                    returnScreen = new WelcomeTo();
                    break;                    
                case "ReadyToWorkout":
                    returnScreen = new ReadyToWorkout();
                    break;
                case "AddMembers":
                    returnScreen = new AddMembers();
                    break;
                case "AddNewMember":
                    returnScreen = new AddNewMember();
                    break;                    
                case "SelectYourMembership":
                    returnScreen = new SelectYourMembership();
                    break;
                case "VerifyEmail":
                    returnScreen = new VerifyEmail();
                    break;
                #endregion
                default:
                    break;
            }

            return returnScreen;
        }
    }
}