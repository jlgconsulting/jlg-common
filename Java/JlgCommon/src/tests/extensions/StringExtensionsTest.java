package tests.extensions;
import extensions.StringExtensions;
import org.junit.Test;

import static org.junit.Assert.*;

/**
 * Created by Dan on 07/09/2015.
 */
public class StringExtensionsTest {

    @Test
    public void testToSqlValidTableOrColumnName() {
        assertEquals("dan_misailescu", StringExtensions
                .toSqlValidTableOrColumnName("Dan Misailescu"));
        assertEquals("name_and_1_surename", StringExtensions
                .toSqlValidTableOrColumnName(" name - And   1 $& surename "));
    }

    @Test
    public void testIsEmail() {
        assertTrue(StringExtensions.isEmail("some.mail@provider.ro"));
        assertFalse(StringExtensions.isEmail("some.mail@.com"));
    }

    @Test
    public void testLowerCaseAndIgnoreSpaces() {
        assertEquals("thisisaverylongtext", StringExtensions
                .lowerCaseAndIgnoreSpaces(" thIs    is a   vERy  LONG texT  "));
    }

    @Test
    public void ReplaceMultipleSpacesWithSingleSpace() {
        assertEquals(" Dan Misailescu ", StringExtensions
                .replaceMultipleSpacesWithSingleSpace("      Dan     Misailescu    "));
    }

    @Test
    public void testCapitalize() {
        assertEquals("Dan", StringExtensions
                .capitalize("dan"));
        assertEquals("Dan Misailescu", StringExtensions
                .capitalize(" dan misailescu   "));
        assertEquals("This Is A Very Long Text", StringExtensions
                .capitalize(" thIs    is a   vERy  LONG texT  "));
        assertEquals("Dan.Misailescu", StringExtensions
                .capitalize("   dan.misailescu "));
        assertEquals("A/Ba Ap\\Ca A.Ddd A;E A,F", StringExtensions
                .capitalize(" a/bA   ap\\ca  a.ddd a;e     a,f  "));
    }
}