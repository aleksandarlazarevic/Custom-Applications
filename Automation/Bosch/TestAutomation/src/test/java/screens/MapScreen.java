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
    public MobileElement IdValue = Helpers.getScreenElement(By.id("textId"));
    public MobileElement DimensValue = Helpers.getScreenElement(By.id("textDimens"));
    public MobileElement ViewportValue = Helpers.getScreenElement(By.id("textViewport"));

    public MapScreen findChargingStation() {
        Range<Integer> horizontalStationLocation = Range.between(1680, 1710);
        Range<Integer> verticalStationLocation = Range.between(1530, 1560);

        Helpers.tapElement(Map);

        // Scroll left until reaching the station's position
        while (!(horizontalStationLocation.contains(Integer.parseInt(ViewportValue.getText().split(",")[0].substring(1))))) {
            Helpers.horizontalSwipeByPercentage(0.2, 0.8, 0.7);
        }

        // Scroll down until reaching the station's position
        while (!(verticalStationLocation.contains(Integer.parseInt(ViewportValue.getText().split(",")[0].substring(1))))) {
            Helpers.verticalSwipeByPercentages(0.8, 0.5, 0.5);
        }

        // Tap the middle of the screen to display station's ID (since it has no accessor)
        Helpers.tapByPercentageCoordinates(0.5, 0.6);

        return getInstance(MapScreen.class);
    }

    public void verifyChargingStationData() {
        assertTrue(IdValue.isDisplayed(), "Station ID not visible");

        String idValue = IdValue.getText();
        String regex = "\\*";
        Pattern pattern = Pattern.compile(regex);
        Matcher matcher = pattern.matcher(idValue);
        long numberOfMatches = matcher.results().count();

        assertTrue((numberOfMatches == 2), String.format("Unexpected number of * signs: {0}", numberOfMatches));

        assertTrue(idValue.matches("(?<=\\*)\\d{3}$"), "ID does not end with 3 digits");
    }

    public boolean isMapScreenDisplayed() {
        return ScreenTitle.isDisplayed();
    }
}
