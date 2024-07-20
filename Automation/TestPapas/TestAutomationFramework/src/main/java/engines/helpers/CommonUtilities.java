package engines.helpers;

import org.springframework.lang.NonNull;
import org.springframework.lang.Nullable;

public class CommonUtilities {
    public static <T> T defaultWhenNull(@Nullable T object, @NonNull T def) {
        return (object == null) ? def : object;
    }

    public static <T> int defaultWhenZero(@Nullable int object, @NonNull T def) {
        return (object == 0) ? (int) def : object;
    }
}
