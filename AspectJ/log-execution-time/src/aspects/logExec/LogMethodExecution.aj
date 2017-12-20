package aspects.logExec;
import org.apache.log4j.Logger;
import org.aspectj.lang.JoinPoint;
import org.aspectj.lang.Signature;

public aspect LogMethodExecution {
	
	Logger _logger = Logger.getLogger(LogMethodExecution.class);
	protected int indentationlevel = 0;
	 
    pointcut loggingOperations():
        (execution(* *.*(..)) || execution(*.new(..)))
    		&& !within(LogMethodExecution+);
    
    Object around(): loggingOperations() {
        StringBuffer sb = new StringBuffer();
        if (_logger.isInfoEnabled()) {
            indentationlevel++;
            for (int i = 0, spaces = indentationlevel * 2; i <= spaces; i++) {
                sb.append(" ");
            }
            beforeLog(sb.toString(), thisJoinPoint);
        }
        Object result;
        try {
            result = proceed();
        } finally {
            if (_logger.isInfoEnabled()) {
                afterLog(sb.toString(), thisJoinPoint);
                indentationlevel--;
            }
        }
        return result;
    }
    
    void beforeLog(String indent, JoinPoint jp) {
    	Signature sig = jp.getSignature();
        String line =""+ jp.getSourceLocation().getLine();
        String sourceName = jp.getSourceLocation().getWithinType().getCanonicalName();
        _logger.debug(indent + "Calling "
	                    +  sourceName
	                    +" line " +
	                    line
	                    +" to " +sig.getDeclaringTypeName() + "." + sig.getName()
        			 );
    }
 
    void afterLog(String indent, JoinPoint jp){
    	Signature sig = jp.getSignature();
        String line =""+ jp.getSourceLocation().getLine();
        String sourceName = jp.getSourceLocation().getWithinType().getCanonicalName();
        _logger.debug(indent + "Leaving "
	                    +  sourceName
	                    +" line " +
	                    line
	                    +" to " +sig.getDeclaringTypeName() + "." + sig.getName()
        			 );
    }
}