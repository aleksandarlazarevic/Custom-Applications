package screens;

import base.BaseScreen;
import io.appium.java_client.MobileElement;
import org.apache.commons.lang3.Range;
import org.openqa.selenium.By;
import utilities.Helpers;

import java.util.regex.Matcher;
import java.util.regex.Pattern;

import static org.junit.jupiter.api.Assertions.assertTrue;

public class MapScreen extends BaseScreen {
    public MobileElement ScreenTitle = Helpers.getScreenElement(By.xpath("//*[(@class='android.widget.TextView') and (@text='Map')]"));
    public MobileElement BackArrow = Helpers.getScreenElement(By.xpath("//android.widget.ImageButton[@content-desc='Navigate up']"));
    public MobileElement SettingsMenu = Helpers.getScreenElement(By.xpath("//android.widget.ImageView[@content-desc='More options']"));
    public MobileElement Map = Helpers.getScreenElement(By.id("viewMap"));

    public MapScreen findChargingStation() {
        Range<Integer> horizontalStationLocation = Range.between(1670, 1720);
        Range<Integer> verticalStationLocation = Range.between(1660, 1900);

        Helpers.pressElement(Map, 1);
        Helpers.tapElement(Map);

        Helpers.horizontalSwipeByPercentage(0.4, 0.3, 0.7);

        MobileElement ViewportValue = Helpers.getScreenElement(By.id("textViewport"));

        // Scroll left until reaching the station's position
        while (!(horizontalStationLocation.contains(Integer.parseInt(ViewportValue.getText().split(",")[0].substring(1))))) {
            Helpers.horizontalSwipeByPercentage(0.4, 0.37, 0.7);
        }

        // Scroll down until reaching the station's position
        while (!(verticalStationLocation.contains(Integer.parseInt(ViewportValue.getText().split(",")[1].trim())))) {
            Helpers.verticalSwipeByPercentages(0.4, 0.35, 0.5);
        }

        // Tap the middle of the screen to display station's ID (since it has no accessor)
        Helpers.tapByPercentageCoordinates(0.5, 0.6);

        return getInstance(MapScreen.class);
    }

    public void verifyChargingStationData() {
        MobileElement IdValue = Helpers.getScreenElement(By.id("textId"));
        assertTrue(IdValue.isDisplayed(), "Station ID not visible");

        String idValue = IdValue.getText();
        Pattern pattern = Pattern.compile("\\*");
        Matcher matcher = pattern.matcher(idValue);
        int numberOfMatches = 0;
        while (matcher.find()) {
            numberOfMatches++;
        }
        assertTrue((numberOfMatches == 2), String.format("Unexpected number of * signs: {0}", numberOfMatches));

        Pattern pattern2 = Pattern.compile("(?<=\\*)\\d{3}$");
        Matcher matcher2 = pattern2.matcher(idValue);

        assertTrue(matcher2.find(), "ID does not end with 3 digits");
    }

    public boolean isMapScreenDisplayed() {
        return ScreenTitle.isDisplayed();
    }
}
