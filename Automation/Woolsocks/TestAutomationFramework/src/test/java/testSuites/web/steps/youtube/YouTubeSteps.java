package testSuites.web.steps.youtube;

import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import testSuites.web.steps.Utilities;
import uiMappings.pages.woolSocks.YouTubeHomepage;


public class YouTubeSteps extends Utilities {
    @When("Search for song {string}")
    public void searchForSong(String songName) {
        runStep(this::searchASong, songName);
    }

    private void searchASong(String songName) {
        getPage(YouTubeHomepage.class).searchForSong(songName);
    }

    @Then("Play the first found song")
    public void playTheFirstFoundSong() {
        runStep(this::playFirstFoundSong);
    }

    private void playFirstFoundSong() {
        getPage(YouTubeHomepage.class).playTheFirstSong();
    }
}
