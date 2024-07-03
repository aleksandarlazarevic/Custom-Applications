package uiMappings.pages.woolSocks;

import engines.selenium.base.BasePageActions;
import engines.selenium.driverInitialization.WebDriverFactory;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

public class YouTubeHomepage extends BasePageActions {
    // region Fields and Properties
    @FindBy(xpath = "//input[@id='search']")
    public WebElement searchTextBox;

    @FindBy(xpath = "//button[@id='search-icon-legacy']")
    public WebElement searchButton;

    @FindBy(xpath = "//ytd-video-renderer[@class='style-scope ytd-item-section-renderer'][1]")
    public WebElement firstVideoFromTheList;
    // endregion
    // region Methods
    public YouTubeHomepage() {
        PageFactory.initElements(WebDriverFactory.getThreadSafeInstance().driver, this);
    }

    public void searchForSong(String songName) {
        sendKeysEx(searchTextBox, songName, "searchTextBox", false, false);
        clickEx(searchButton, "searchButton", false);
    }

    public void playTheFirstSong() {
        clickEx(firstVideoFromTheList, "firstVideoFromTheList", false);
    }
    // endregion
}
