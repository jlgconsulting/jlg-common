package domain;

import java.lang.String;
import java.util.UUID;

public class NameValuePair<TName, TValue> {

    public String Id;
    public TName Name;
    public TValue Value;
    public String OwnerId;

    public NameValuePair() {
        UUID uuid = UUID.randomUUID();
        Id = uuid.toString();
    }

    public NameValuePair(TName name, TValue value) {
        UUID uuid = UUID.randomUUID();
        Id = uuid.toString();

        Name = name;
        Value = value;
    }

    public NameValuePair(TName name, TValue value, String ownerId) {
        UUID uuid = UUID.randomUUID();
        Id = uuid.toString();

        Name = name;
        Value = value;
        OwnerId = ownerId;
    }
}