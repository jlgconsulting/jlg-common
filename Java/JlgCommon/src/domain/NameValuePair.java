package domain;

import java.lang.String;
import java.util.UUID;

public class NameValuePair<TName, TValue> {

    public String id;
    public TName name;
    public TValue value;
    public String ownerId;

    public NameValuePair() {
        UUID uuid = UUID.randomUUID();
        id = uuid.toString();
    }

    public NameValuePair(TName name, TValue value) {
        this();
        this.name = name;
        this.value = value;
    }

    public NameValuePair(TName name, TValue value, String ownerId) {
        this(name,value);
        this.ownerId = ownerId;
    }
}