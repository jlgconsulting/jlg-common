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
        assertEquals("dan_misailescu", StringExtensions.toSqlValidTableOrColumnName("Dan Misailescu"));
        assertEquals("name_and_1_surename", StringExtensions.toSqlValidTableOrColumnName(" name - And   1 $& surename "));
    }

    @Test
    public void testIsEmail() {
        assertTrue(StringExtensions.isEmail("some.mail@provider.ro"));
        assertFalse(StringExtensions.isEmail("some.mail@.com"));
    }
}