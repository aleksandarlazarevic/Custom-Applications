package base;

public class ScreenFactory {
    private static ScreenFactory instance = null;
    public BaseScreen CurrentPage;

    public static ScreenFactory getInstance() {
        if (instance == null){
            instance = new ScreenFactory();
        }

        return instance;
    }

    private ScreenFactory() { }

}
