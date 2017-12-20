package aspects.protect;

import java.lang.annotation.Retention;
import java.lang.annotation.RetentionPolicy;

@Retention(RetentionPolicy.RUNTIME)
public @interface Protected {
	
	public enum accessRight { VIEW, MODIFY};
	String getUsername();
	accessRight getAccessRight();
}
