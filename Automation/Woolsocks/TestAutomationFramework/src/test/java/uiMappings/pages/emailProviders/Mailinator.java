package uiMappings.pages.emailProviders;

import engines.selenium.base.BasePageActions;
import engines.selenium.driverInitialization.WebDriverFactory;
import org.junit.jupiter.api.Assertions;
import org.openqa.selenium.By;
import org.openqa.selenium.NotFoundException;
import org.openqa.selenium.TimeoutException;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;

import java.util.List;

public class Mailinator extends BasePageActions {
    // region Fields and Properties
    @FindBy(xpath = "//input[@id='search'] | //input[@id='inbox_field']")
    public WebElement email;
    @FindBy(xpath = "//button[normalize-space(text())='GO']")
    public WebElement go;
    @FindBy(xpath = "//*[@class='table-striped jambo_table']")
    public WebElement emailListContainer;
    @FindBy(tagName = "body")
    public WebElement emailContent;

    @FindBy(xpath = "//a[contains(@class, 'inbox-link')]")
    public WebElement publicInbox;

    @FindBy(xpath = "//*[@id='inbox_field']")
    public WebElement emailName;
    @FindBy(xpath = "//button[@aria-label='Delete Button']")
    public WebElement deleteButton;
    @FindBy(xpath = "//*[@id='secureLinkSalutationId']//..//..//p[1]")
    public WebElement notificationEmailContent;
    // endregion

    // region Methods
    public Mailinator() {
        PageFactory.initElements(WebDriverFactory.getThreadSafeInstance().driver, this);
    }

    public Mailinator enterEmail(String emailAddress) {
        sendKeysEx(this.email, emailAddress, "EmailAddress", false, false);
        return this;
    }

    public Mailinator clickOnGo() {
        clickEx(this.go, "Go", false);
        return this;
    }

    public Mailinator clickOnEmail(String subject) throws Exception {
        List<WebElement> rows = this.emailListContainer.findElements(By.xpath("./tbody/tr"));
        WebElement emailRow = rows.stream().map(x -> x.findElement(By.xpath("./td[3]"))).findFirst().get();

        String emailRowValue = getValueEx(emailRow);
        if (emailRowValue.contains(subject)) {
            clickEx(emailRow, subject, false);
        } else {
            throw new Exception(String.format("Failed to find the email containing subject: %s", subject));
        }
        return this;
    }

    private boolean isEmailReceived(String subject) {
        WebElement emailElement = this.emailListContainer.findElements(By.xpath("./tbody/tr")).stream().
                map(x -> x.findElement(By.xpath("./td[3]"))).findFirst().get();

        return getValueEx(emailElement).contains(subject);
    }

    public Mailinator waitForEmail(String subject) {
        int timeoutInSeconds = 240;

        try {
            WebDriverFactory.getThreadSafeInstance().wait.until($ -> isEmailReceived(subject));
        } catch (Exception ex) {
            throw new TimeoutException(String.format("Email not received after {0} seconds", timeoutInSeconds), ex);
        }

        return this;
    }

    public Mailinator verifyEmailBodyContent(String text) {
        WebDriverFactory.getThreadSafeInstance().driver.switchTo().frame("html_msg_body");
        String email = getValueEx(this.notificationEmailContent);
        Assertions.assertTrue(email.contains(text), String.format("Email body does not contain expected text! \nExpected: {0} \nActual: {1}", text, email));
        return this;
    }

    public String clickVerificationLink(String text) {
        WebDriverFactory.getThreadSafeInstance().driver.switchTo().frame("html_msg_body");

        WebElement textElement = WebDriverFactory.getThreadSafeInstance().driver.findElement(By.xpath(String.format("//*[contains(text(), '{0}')]", text)));

        if (textElement == null) {
            throw new NotFoundException(String.format("Failed to locate '{0}' in the content", text));
        }

        return textElement.getText();
    }

    public Mailinator clikOnPublicInbox() {
        clickEx(this.publicInbox, "PublicInbox", false);
        return this;
    }
    // endregion
}
