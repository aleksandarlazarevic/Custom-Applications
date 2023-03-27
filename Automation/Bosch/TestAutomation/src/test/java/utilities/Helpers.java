package utilities;

import base.DriverFactory;
import org.openqa.selenium.Dimension;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.openqa.selenium.By;
import io.appium.java_client.MobileElement;
import io.appium.java_client.TouchAction;
import static io.appium.java_client.touch.TapOptions.tapOptions;
import static io.appium.java_client.touch.WaitOptions.waitOptions;
import static io.appium.java_client.touch.offset.ElementOption.element;
import static io.appium.java_client.touch.offset.PointOption.point;
import static java.time.Duration.ofSeconds;

public class Helpers {
    public static MobileElement getScreenElement(By elementLocator)
    {
        MobileElement screenElement = (MobileElement)new WebDriverWait(DriverFactory.getInstance().driver, 60).until(ExpectedConditions.elementToBeClickable(elementLocator));

        return screenElement;
    }

    public static void tapElement (MobileElement mobileElement) {
        new TouchAction(DriverFactory.getInstance().driver)
                .tap(tapOptions().withElement(element(mobileElement)))
                .waitAction(waitOptions(ofSeconds(2))).perform();
    }

    public static void tapByPercentageCoordinates (double horizontalPercentageOfTheScreen, double verticalPercentageOfTheScreen) {
        Dimension size = DriverFactory.getInstance().driver.manage().window().getSize();
        int x = (int) (size.width * horizontalPercentageOfTheScreen);
        int y = (int) (size.height * verticalPercentageOfTheScreen);
        new TouchAction(DriverFactory.getInstance().driver)
                .tap(point(x,y))
                .waitAction(waitOptions(ofSeconds(2))).perform();
    }

    public static void pressElement (MobileElement mobileElement, long seconds) {
        new TouchAction(DriverFactory.getInstance().driver)
                .press(element(mobileElement))
                .waitAction(waitOptions(ofSeconds(seconds)))
                .release()
                .perform();
    }

    public static void horizontalSwipeByPercentage (double startPercentage, double endPercentage, double anchorPercentage) {
        Dimension size = DriverFactory.getInstance().driver.manage().window().getSize();
        int anchor = (int) (size.height * anchorPercentage);
        int startPoint = (int) (size.width * startPercentage);
        int endPoint = (int) (size.width * endPercentage);
        new TouchAction(DriverFactory.getInstance().driver)
                .press(point(startPoint, anchor))
                .waitAction(waitOptions(ofSeconds(1)))
                .moveTo(point(endPoint, anchor))
                .release().perform();
    }
    public static void verticalSwipeByPercentages(double startPercentage, double endPercentage, double anchorPercentage) {
        Dimension size = DriverFactory.getInstance().driver.manage().window().getSize();
        int anchor = (int) (size.width * anchorPercentage);
        int startPoint = (int) (size.height * startPercentage);
        int endPoint = (int) (size.height * endPercentage);
        new TouchAction(DriverFactory.getInstance().driver)
                .press(point(anchor, startPoint))
                .waitAction(waitOptions(ofSeconds(1)))
                .moveTo(point(anchor, endPoint))
                .release().perform();
    }
}
