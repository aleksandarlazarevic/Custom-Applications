package testSuites.web.steps;

import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import uiMappings.pages.woolSocks.YouTubeHomepage;


public class YouTubeSteps extends Utilities {
    @When("Search for song {string}")
    public void searchForSong(String songName) {
        getPage(YouTubeHomepage.class).searchForSong(songName);
    }

    @Then("Play the first found song")
    public void playTheFirstFoundSong() {
        getPage(YouTubeHomepage.class).playTheFirstSong();
    }
}
