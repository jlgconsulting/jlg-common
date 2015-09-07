package extensions;

/**
 * Created by Dan on 07/09/2015.
 */
public final class StringExtensions {

    public static String toSqlValidTableOrColumnName(String str)
    {
        while (str.indexOf("  ") >= 0)
        {
            str = str.replace("  ", " ");
        }
        str = str.trim();
        str = str.replaceAll("[^a-zA-Z0-9_.]+","_");
        if (str.length() > 110)
        {
            str = str.substring(0, 110);
        }
        return str.toLowerCase();
    }

    public static boolean isEmail(String email) {
        String validEmailPattern = "^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@"
                + "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$";
        return email.matches(validEmailPattern);
    }

    private StringExtensions(){
    }
}
