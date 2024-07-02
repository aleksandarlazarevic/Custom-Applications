package common.utilities;

import java.lang.reflect.Field;
import java.sql.*;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public class SQLManager {
    public static ResultSet executeQuery(String connectionString, String sqlCommand) {
        ResultSet resultSet = null;

        try (Connection connection = DriverManager.getConnection(connectionString); Statement statement = connection.createStatement();) {
            resultSet = statement.executeQuery(sqlCommand);
        } catch (SQLException e) {
            e.printStackTrace();
        }

        return resultSet;
    }

    public static <T> List<T> resultSetToObject(ResultSet resultSet, Class<T> objectType) {
        List<Field> fields = Arrays.asList(objectType.getDeclaredFields());
        for(Field field: fields) {
            field.setAccessible(true);
        }

        List<T> list = new ArrayList<>();

        try {
            while(resultSet.next()) {

                T dto = objectType.getConstructor().newInstance();

                for(Field field: fields) {
                    String name = field.getName();

                    try{
                        String value = resultSet.getString(name);
                        field.set(dto, field.getType().getConstructor(String.class).newInstance(value));
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }

                list.add(dto);
            }
        } catch (Exception exception) {
            throw new RuntimeException("Failed casting sql result set to an object");
        }

        return list;
    }
}