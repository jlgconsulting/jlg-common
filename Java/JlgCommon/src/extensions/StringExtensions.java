package extensions;

import domain.Constants;

import java.util.Arrays;
import java.util.List;

public final class StringExtensions {

    public static String toSqlValidTableOrColumnName(String str)
    {
        str = replaceMultipleSpacesWithSingleSpace(str);
        str = str.trim();
        str = str.replaceAll("[^a-zA-Z0-9_.]+", "_");
        if (str.length() > 110) {
            str = str.substring(0, 110);
        }
        return str.toLowerCase();
    }

    public static boolean isEmail(String email) {
        String validEmailPattern = "^[_A-Za-z0-9-\\+]+(\\.[_A-Za-z0-9-]+)*@"
                + "[A-Za-z0-9-]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$";
        return email.matches(validEmailPattern);
    }

    public static String lowerCaseAndIgnoreSpaces(String value) {
        String comparableString = value.replaceAll(Constants.SPACE_STRING, Constants.EMPTY_STRING);
        return comparableString.toLowerCase();
    }

    public static String replaceMultipleSpacesWithSingleSpace(String value)
    {
        while (value.contains("  ")) {
            value = value.replaceAll("  ", Constants.SPACE_STRING);
        }
        return value;
    }

    public static String capitalize(String str) {
        if (str.isEmpty()) {
            return str;
        }

        List<String> specialCharacters = Arrays.asList("/", "\\", ".", ";", ",");
        str = str.toLowerCase().trim();
        for (String specialCharacter : specialCharacters) {
            str = str.replace(specialCharacter, String.format("%s ", specialCharacter));
        }

        String[] words = str.split(Constants.SPACE_STRING);
        StringBuilder capitalezedPhrase = new StringBuilder();

        for (String word : words) {
            if (word.length() == 0) {
                continue;
            }
            StringBuilder wordForEdit = new StringBuilder(word);
            String firstLeterCapitalized = ((Character)wordForEdit.charAt(0)).toString().toUpperCase();

            wordForEdit = wordForEdit.delete(0, 1);
            wordForEdit = wordForEdit.insert(0, firstLeterCapitalized);

            capitalezedPhrase.append(wordForEdit);
            capitalezedPhrase.append(Constants.SPACE_STRING);
        }
        capitalezedPhrase = capitalezedPhrase.delete(capitalezedPhrase.length() - 1, capitalezedPhrase.length());
        String capitalized = capitalezedPhrase.toString();
        for (String specialCharacter : specialCharacters) {
            capitalized = capitalized.replace(String.format("%s ", specialCharacter), specialCharacter);
        }
        capitalized = replaceMultipleSpacesWithSingleSpace(capitalized);

        return capitalized;
    }

    private StringExtensions(){
    }
}
